using Balivo.DISampleApp.Data;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Balivo.DISampleApp.ViewModels
{
    sealed class HomeViewModel : BaseViewModel
    {
        public HomeViewModel() : base()
        {
            this._EstadoIndex = 0;

            Task.Factory.StartNew(async () => await CarregarCidades());
        }

        private int _EstadoIndex;
        public const string EstadoIndexPropertyName = "EstadoIndex";
        public int EstadoIndex
        {
            get { return this._EstadoIndex; }
            set { SetProperty(ref this._EstadoIndex, value, EstadoIndexPropertyName); }
        }

        private string _Nome;
        public const string NomePropertyName = "Nome";
        public string Nome
        {
            get { return this._Nome; }
            set { SetProperty(ref this._Nome, value, NomePropertyName); }
        }

        private string _Cidades;
        public const string CidadesPropertyName = "Cidades";
        public string Cidades { get { return string.IsNullOrWhiteSpace(this._Cidades) ? "Nenhuma cidade cadastrada" : this._Cidades; } }

        private Command _SalvarCidadeCommand;
        public const string SalvarCidadeCommandPropertyName = "SalvarCidadeCommand";
        public Command SalvarCidadeCommand
        {
            get { return this._SalvarCidadeCommand ?? (this._SalvarCidadeCommand = new Command(async () => await SalvarCidadeCommandExecute(), () => { return this.IsNotBusy; })); }
        }

        async Task SalvarCidadeCommandExecute()
        {
            try
            {
                if (this.IsBusy)
                    return;

                this.IsBusy = true;
                this.SalvarCidadeCommand.ChangeCanExecute();

                if (!string.IsNullOrWhiteSpace(this._Nome))
                {
                    await MobileDatabase.Current.CidadeSave(new Cidade() { CidadeId = Guid.NewGuid(), Nome = this._Nome, UF = this._EstadoIndex == 0 ? "SP" : "RJ" });
                }

                await CarregarCidades();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                this.IsBusy = false;
                this.SalvarCidadeCommand.ChangeCanExecute();
            }
        }

        private async Task CarregarCidades()
        {
            try
            {
                var _cidadesCadastradas = await MobileDatabase.Current.CidadeGetAll();

                this._Cidades = string.Empty;

                foreach (var _cidade in _cidadesCadastradas)
                {
                    this._Cidades += $"{_cidade.Nome} ({_cidade.UF}){(_cidadesCadastradas.IndexOf(_cidade) + 1 == _cidadesCadastradas.Count ? string.Empty : ", ")}";
                }

                OnPropertyChanged(CidadesPropertyName);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

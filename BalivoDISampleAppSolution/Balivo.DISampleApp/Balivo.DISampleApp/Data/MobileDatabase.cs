using Balivo.DISampleApp.Providers;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Balivo.DISampleApp.Data
{
    sealed class MobileDatabase
    {
        private static readonly Lazy<MobileDatabase> _lazy = new Lazy<MobileDatabase>(() => new MobileDatabase());

        public static MobileDatabase Current { get { return _lazy.Value; } }

        private readonly SQLiteConnection _Db;

        public MobileDatabase()
        {
            this._Db = DependencyService.Get<ISQLiteConnectionProvider>().GetConnection();
            this._Db.CreateTable<Cidade>();
        }

        public Task<int> CidadeSave(Cidade pCidade)
        {
            pCidade.UF.ToUpper();

            return Task.Factory.StartNew(() =>
            {
                return this._Db.InsertOrReplace(pCidade);
            });
        }

        public Task<List<Cidade>> CidadeGetAll()
        {
            return Task.Factory.StartNew(() =>
            {
                return this._Db.Table<Cidade>().ToList();
            });
        }

        public Task<List<Cidade>> CidadeGetByUF(string pUF)
        {
            return Task.Factory.StartNew(() =>
            {
                return this._Db.Table<Cidade>().Where(lbda => lbda.UF.Equals(pUF.ToUpper())).ToList();
            });
        }
    }
}

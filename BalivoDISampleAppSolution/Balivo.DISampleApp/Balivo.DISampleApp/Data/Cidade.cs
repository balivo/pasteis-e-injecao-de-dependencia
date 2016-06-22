using SQLite.Net.Attributes;
using System;

namespace Balivo.DISampleApp.Data
{
    public sealed class Cidade
    {
        [PrimaryKey]
        public Guid CidadeId { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
    }
}
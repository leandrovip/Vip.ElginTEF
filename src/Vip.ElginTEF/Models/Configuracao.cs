namespace Vip.ElginTEF.Models
{
    public sealed class Configuracao
    {
        #region Constructor

        internal Configuracao()
        {
            TextoPinpad = "VipERP PDV";
            VersaoAC = "V1.0.0";
            NomeEstabelecimento = "Elgin";
            Loja = "01";
            IdentificadorPontoCaptura = "T0004";
            IpClientTCP = "127.0.0.1";
            PortaClientTCP = 60906;
        }

        #endregion Constructor

        #region Propriedades

        public string TextoPinpad { get; set; }
        public string VersaoAC { get; set; }
        public string NomeEstabelecimento { get; set; }
        public string Loja { get; set; }
        public string IdentificadorPontoCaptura { get; set; }
        public string IpClientTCP { get; set; }
        public int PortaClientTCP { get; set; }

        #endregion Propriedades
    }
}
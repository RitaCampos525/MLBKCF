using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class LM32_PedidosContratoML
    {
        public int Cliente { get; set; }
        public string Nome { get; set; }
        public string idmultilinha { get; set; }
        public string txtidmultilinha_balcao { get; set; }
        public int nBalcao { get; set; }
        public string gBalcao { get; set; }
        public string ProductCode { get; set; }
        public string SubProdutoCode { get; set; }
        public string SubProductDescription { get; set; }
        public string TipoPedido { get; set; }
        public bool btnAccept { get; set; }
        public bool btnReject { get; set; }

        public List<pedidoAprovacao> PedidosAprovacao = new List<pedidoAprovacao> { new pedidoAprovacao() };
        public class pedidoAprovacao
        {
            public int nBalcao { get; set; }
            public string idmultilinha { get; set; }
            public int idcliente { get; set; }
            public string produto { get; set; }
            public string subProduto { get; set; }
            public string descritivo { get; set; }
            public string TipoPedido { get; set; }
            public string utilizador { get; set; }
        }
    }
}

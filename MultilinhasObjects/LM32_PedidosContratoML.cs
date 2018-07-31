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
        public int idmultilinha { get; set; }

        public int nBalcao { get; set; }
        public string TipoPedido { get; set; }

        public List<pedidoAprovacao> PedidosAprovacao;
        public class pedidoAprovacao
        {
            public int nBalcao { get; set; }
            public int idmultilinha { get; set; }
            public int idcliente { get; set; }
            public string produto { get; set; }
            public string subproduto { get; set; }
            public string descritico { get; set; }
            public string TipoPedido { get; set; }
        }
    }
}

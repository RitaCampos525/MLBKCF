using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    [Serializable]
    public class LM34_SublimitesML
    {
        public int Cliente { get; set; }
        public string Nome { get; set; }
        public string idmultilinha { get; set; }
        public string Produtoml { get; set; }
        public string Subprodutoml { get; set; }
        public string Descritivo { get; set; }
        public string EstadoContrato { get; set; }
        public string ncontado { get; set; } //principal
        public decimal limiteglobalmultilinha { get; set; } //é o limite comprometido
        public string idSimulacao { get; set; }
        public decimal sublimiteriscoFinanceiro { get; set; }
        public decimal sublimitriscoComercial { get; set; }
        public decimal sublimiteriscoAssinatura { get; set; }

        public List<ProdutosRisco> ProdutosRiscoAssinatura = new List<ProdutosRisco>();

        public List<ProdutosRisco> produtosRiscoF = new List<ProdutosRisco>();

        public List<ProdutosRisco> produtosRiscoC = new List<ProdutosRisco>();

        [Serializable]
        public class ProdutosRisco
        {
            public string tipologia { get; set; }
            public string familiaproduto { get; set; }
            public int codfamiliaproduto { get; set; }
            public decimal produto { get; set; }
            public string prodsubproduto { get; set; }
            public string descritivo { get; set; }
            public decimal sublimitecomprometido { get; set; } //Aquele q é editavel

            public int zSeq { get; set; }
        }


    }
}

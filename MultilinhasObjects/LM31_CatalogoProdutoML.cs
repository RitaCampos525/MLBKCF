using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilinhasObjects
{
    [Serializable]
    public class LM31_CatalogoProdutoML
    {

        public string ProductCode { get; set; }
        public string SubProdutoCode { get; set; }
        public string SubProductDescription { get; set; }
        public DateTime DataInicioComercializacao { get; set; }
        public DateTime DataFimComercializacao { get; set; }
        public int PrazoMinimo { get; set; }
        public int PrazoMaximo { get; set; }
        public int NumeroMinimoProdutos { get; set; } //2
        public decimal LimiteMinimoCredito { get; set; }
        public decimal LimiteMaximoCredito { get; set; } //13
        public string Estado { get; set; }
        public int NDiasIncumprimento { get; set; }
        public int NDiasPreAviso { get; set; }
        public string IndRenovacao { get; set; }
        public int PrazoRenovacao { get; set; }
        public string tipologiaRiscoF { get; set; } //F ou vazio - length 1
        public string tipologiaRiscoA { get; set; } //A ou vazio - length 1
        public string tipologiaRiscoC { get; set; } //C ou vazio - length 1
        public List<ProdutoRisco> produtosA { get; set; }
        public List<ProdutoRisco> produtosC { get; set; }
        public List<ProdutoRisco> produtosF { get; set; }

        [Serializable]
        public class ProdutoRisco
        {
            public string familia { get; set; }
            public string tipologia { get; set; }
            public string produto { get; set; }
            public string subproduto { get; set; }
            public string descritivo { get; set; }
            public string zSequencial { get; set; }

        }
        public string PeriocidadeCobranca { get; set; }
        public string zSequencial { get; set; }
    }

    
    
}

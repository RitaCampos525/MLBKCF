using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    [Serializable]
    public class LM33_ContratoML
    {
        public int Cliente { get; set; }
        public string Nome { get; set; }
        public string Produtoml { get; set; }
        public string Subprodutoml { get; set; }
        public string Descritivo { get; set; }
        public string idproposta { get; set; }
        public string ncontado { get; set; }
        public int NumeroMinimoProdutos { get; set; } //2
        public DateTime datainiciocontrato { get; set; }
        public int prazocontrato { get; set; }
        public DateTime datafimcontrato { get; set; }
        public bool indicadorrenovacao {get;set;}
        public int prazorenovacao { get; set; }
        public DateTime datarenovacao { get; set; }
        public string EstadoContrato { get; set; }
        public int ndiasincumprimento { get; set; }
        public int NDiasPreAviso { get; set; }
        public int graumorosidade { get; set; }
        public decimal limiteglobalmultilinha { get; set; }
        public decimal sublimiteriscoFinanceiro { get; set; }
        public decimal sublimitriscoComercial { get; set; }
        public decimal sublimiteriscoAssinatura { get; set; }
        public int idmultilinha { get; set; }
        public string tipologiaRiscoF { get; set; } //F ou vazio - length 1
        public string tipologiaRiscoA { get; set; } //A ou vazio - length 1
        public string tipologiaRiscoC { get; set; } //C ou vazio - length 1

       public decimal comissaoabertura { get; set; }
        public decimal valorimpostocomabert { get; set; }
        public decimal baseincidenciacomabert { get; set; }
        public decimal comissaogestaocontrato { get; set; }
        public decimal valorimpostocomgestcontrato { get; set; }
        public decimal baseincidenciacomgestcontrato { get; set; }
        public string PeriocidadeCobrancagestcontrato { get; set; }
        public DateTime dataproximacobrancagestcontrato { get; set; }

        public decimal comissaorenovacao { get; set; }
        public decimal valorimpostocomgestrenovacao { get; set; }
        public decimal baseincidenciacomgestrenovacao { get; set; }
        public string PeriocidadeCobrancagestRenovacao { get; set; }
        public DateTime dataproximacobrancagestrenovacao { get; set; }

        public DateTime dataProcessamento { get; set; }
        public bool indicadorAcaoCancelamento { get; set; }
        public bool indicadorAcaoEnvioCartas { get; set; }
        public bool indicadorAcaoSimulacao { get; set; }

        public int NMinutaContrato { get; set; }

        public List<ProdutosRiscoA> ProdutosRiscoAssinatura = new List<ProdutosRiscoA>();
        [Serializable]
        public class ProdutosRiscoA
        {
            public string tipologia { get; set; }
            public string familiaproduto { get; set; }
            public string prodsubproduto { get; set; }
            public string descritivo { get; set; }

            public int zSeq { get; set; }
        }

        public List<ProdutoRiscoF> produtosRiscoF = new List<ProdutoRiscoF>();
        [Serializable]
        public class ProdutoRiscoF
        {
            public string tipologia { get; set; }
            public string familiaproduto { get; set; }
            public string prodsubproduto { get; set; }
            public string descritivo { get; set; }

            public int zSeq { get; set; }
        }

        public List<ProdutoRiscoC> produtosRiscoC = new List<ProdutoRiscoC>();
        [Serializable]
        public class ProdutoRiscoC
        {
            public string tipologia { get; set; }
            public string familiaproduto { get; set; }
            public string prodsubproduto { get; set; }
            public string descritivo { get; set; }

            public int zSeq { get; set; }
        }

    }
}

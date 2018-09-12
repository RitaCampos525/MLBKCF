using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    [Serializable]
    public class LM37_SimulacaoMl : LM34_SublimitesML
    {
     
        public int Balcao { get; set; }
        public DateTime dataSimulacao { get; set; }
        public string tipoSimulacao { get; set; }

        public decimal limiteglobalmultilinhaNovo { get; set; }
        public decimal limiteglobalmultilinhaTotal { get; set; }
        public decimal sublimiteriscoFinanceiroNovo { get; set; }
        public decimal sublimiteriscoFinanceiroTotal { get; set; }
        public decimal sublimitriscoComercialNovo { get; set; }
        public decimal sublimitriscoComercialTotal { get; set; }
        public decimal sublimiteriscoAssinaturaNovo { get; set; }
        public decimal sublimiteriscoAssinaturaTotal { get; set; }

        // public List<simulacaoSublimites> SimulacaoSublimites { get; set; }
        public List<simulacaoSublimites> SimulacaoSublimites = new List<simulacaoSublimites> { new simulacaoSublimites() };

        [Serializable]
        public class simulacaoSublimites
        {
            public string TipologiaRisco { get; set; }

            public string FamiliaProduto { get; set; }

            public string CodigoTipologia { get; set; }

            public bool preco { get; set; }

            public decimal SublimiteComprometido { get; set; }
            public decimal SublimiteComprometidoNovo { get; set; }

            public decimal SublimiteContratado { get; set; }

            public decimal ExposicaoAtual { get; set; }

            //Para consulta em lista - modo V

            public string cons_Cliente { get; set; }

            public string cons_Balcao { get; set; }

            public string cons_ProdSub { get; set; }

            public DateTime cons_DataSimulacao { get; set; }

            public string cons_idMultilinha { get; set; }

            public string cons_idSimulacao { get; set; }

            public decimal cons_limiteML { get; set; }

            public decimal cons_limiteRF { get; set; }

            public decimal cons_limiteRC { get; set; }

            public decimal cons_limiteRA { get; set; }

            public string cons_utilizador { get; set; }

            public int zSeq { get; set; }

            
        }

    }
}

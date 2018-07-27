using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class LM37_SimulacaoMl : LM34_SublimitesML
    {

        public int IDSimulacaoML { get; set; }

        public int Balcao { get; set; }

        public DateTime dataSimulacao { get; set; }


        public List<simulacaoSublimites> SimulacaoSublimites { get; set; }

        public class simulacaoSublimites
        {
            public string TipologiaRisco { get; set; }

            public string FamiliaProduto { get; set; }

            public string produto { get; set; }

            public bool preco { get; set; }

            public decimal SublimiteComprometido { get; set; }

            public decimal SublimiteContratado { get; set; }

            public decimal ExposicaoAtual { get; set; }

            //Para consulta em lista - modo V

            public string Balcao { get; set; }

            public DateTime DataSimulacao { get; set; }

            public int idMultilinha { get; set; }

            public int idSimulacao { get; set; }

            public decimal limiteRF { get; set; }

            public decimal limiteRC { get; set; }

            public decimal limiteRA { get; set; }

            public string utilizador { get; set; }

            public int zSeq { get; set; }

            
        }

    }
}

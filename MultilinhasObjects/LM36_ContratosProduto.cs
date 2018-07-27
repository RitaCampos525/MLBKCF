﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class LM36_ContratosProduto
    {
        public int Cliente { get; set; }
        public string Nome { get; set; }
        public string Produtoml { get; set; }
        public string Subprodutoml { get; set; }
        public string Descritivo { get; set; }
        public string idmultilinha { get; set; }
        public string EstadoContratoProduto { get; set; }
        public string TipologiaRisco { get; set; }
        public string FamiliaProduto { get; set; }
        public int GrauMorosidade { get; set; }

        public decimal limiteglobalmultilinha { get; set; }
        public decimal sublimiteriscoFinanceiro { get; set; }
        public decimal sublimitriscoComercial { get; set; }
        public decimal sublimiteriscoAssinatura { get; set; }

        public class ContratosProduto
        {
            public string TipoRisco { get; set; }
            public string FamiliaProduto { get; set; }
            public string SubProduto { get; set; }
            public string NContratoProduto { get; set; }
            public string EstadoContratoProduto { get; set; }
            public string GrauMorosidade { get; set; }
            public string ValorComprometido { get; set; }
            public string ValorContratado { get; set; }
            public string ExposicaoAtual { get; set; }

        }



    }
}

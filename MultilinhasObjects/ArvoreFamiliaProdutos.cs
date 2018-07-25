using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class ArvoreFamiliaProdutos
    {
        public string tipologiaRisco { get; set; }
        public string familiaProduto { get; set; }
        public int codfamiliaProduto {get; set;}
        public string produto { get; set; }
        public string subproduto { get; set; }
        public string descricao { get; set; }

        public class FamiliaProduto
        {
            public int codeFamProd { get; set; }
            public string nomeFamProd { get; set; }
            public string descFamProd { get; set; }
        }

        public class FamiliaProdutos
        {
            public static FamiliaProduto F_Descobertos = new FamiliaProduto
            {
                codeFamProd = 1,
                nomeFamProd = "F_Descobertos",
                descFamProd = "Descobertos"
            };

            public static FamiliaProduto F_CCCs = new FamiliaProduto
            {
                codeFamProd = 2,
                nomeFamProd = "F_CCCs",
                descFamProd = "CCC's"
            };

            public static FamiliaProduto F_Livrancas = new FamiliaProduto
            {
                codeFamProd = 3,
                nomeFamProd = "F_Livrancas",
                descFamProd = "Livranças"
            };

            public static FamiliaProduto F_CreditoNegociosEmpresasMTL = new FamiliaProduto
            {
                codeFamProd = 4,
                nomeFamProd = "F_CreditoNegociosEmpresasMTL",
                descFamProd = "Crédito a Negócios e Empresas MTL"
            };

            public static FamiliaProduto F_FinanciamentoPagaImpostos = new FamiliaProduto
            {
                codeFamProd = 5,
                nomeFamProd = "F_FinanciamentoPagaImpostos",
                descFamProd = "Financiamento Paga. Impostos"
            };

            public static FamiliaProduto F_AdiantamentosSobreCheques = new FamiliaProduto
            {
                codeFamProd = 6,
                nomeFamProd = "F_AdiantamentosSobreCheques",
                descFamProd = "Financiamento Paga. Impostos"
            };

            public static FamiliaProduto F_AdiantamentosIVA = new FamiliaProduto
            {
                codeFamProd = 7,
                nomeFamProd = "F_AdiantamentosIVA",
                descFamProd = "Adiantamentos de IVA"
            };
            public static FamiliaProduto F_PagamentoFornecedores = new FamiliaProduto
            {
                codeFamProd = 8,
                nomeFamProd = "F_PagamentoFornecedores",
                descFamProd = "Pagamento a Fornecedores"
            };

            public static FamiliaProduto F_FinanciamentoExportacaoDocumentos = new FamiliaProduto
            {
                codeFamProd = 9,
                nomeFamProd = "F_FinanciamentoExportacaoDocumentos",
                descFamProd = "Financiamento à Exportação c/Documentos"
            };
            public static FamiliaProduto F_FinanciamentoExportacaoSDocumentos = new FamiliaProduto
            {
                codeFamProd = 10,
                nomeFamProd = "F_FinanciamentoExportacaoSDocumentos",
                descFamProd = "Financiamento à Exportação s/Documentos"
            };
            public static FamiliaProduto F_FinanciamentoImportacao = new FamiliaProduto
            {
                codeFamProd = 11,
                nomeFamProd = "F_FinanciamentoImportacao",
                descFamProd = "Financiamento à Importação"
            };
            public static FamiliaProduto A_GarantiasBancTécnicas = new FamiliaProduto
            {
                codeFamProd = 12,
                nomeFamProd = "A_GarantiasBancTécnicas",
                descFamProd = "Garantias Banc. Técnicas"
            };
            public static FamiliaProduto A_GarantiasBancFinanceiras = new FamiliaProduto
            {
                codeFamProd = 13,
                nomeFamProd = "A_GarantiasBancFinanceiras",
                descFamProd = "Garantias Banc. Financeiras"
            };
            public static FamiliaProduto A_GarantiasBancAvalesBancarios = new FamiliaProduto
            {
                codeFamProd = 14,
                nomeFamProd = "A_GarantiasBancAvalesBancarios",
                descFamProd = "Garantias Banc. Avales Bancários"
            };
        
            public static FamiliaProduto A_CreditoDocumentarioImportação = new FamiliaProduto
            {
                codeFamProd = 15,
                nomeFamProd = "A_CreditoDocumentarioImportação",
                descFamProd = "Crédito Documentário Importação"
            };
            public static FamiliaProduto C_FactoringCSRecurso = new FamiliaProduto
            {
                codeFamProd = 16,
                nomeFamProd = "C_FactoringCSRecurso",
                descFamProd = "Factoring c/ e s/ recurso"
            };
            public static FamiliaProduto C_Letras = new FamiliaProduto
            {
                codeFamProd = 17,
                nomeFamProd = "C_Letras",
                descFamProd = "Letras"
            };
         }

        public static List<ArvoreFamiliaProdutos> SearchFamiliaProduto(string risco)
        {
            List<ArvoreFamiliaProdutos> lstarvprd = new List<ArvoreFamiliaProdutos>();

            switch (risco)
            {
                case Constantes.tipologiaRisco.RF:
                    return new List<ArvoreFamiliaProdutos>() {
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Descobertos.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Descobertos.codeFamProd,
                            produto = "",
                            subproduto= "",
                            descricao="NA"
                        },
                         new ArvoreFamiliaProdutos()
                         {
                             tipologiaRisco = Constantes.tipologiaRisco.RF,
                             familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                             codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                             produto = "20",
                             subproduto = "30",
                             descricao = "WAGE ACCOUNT    "
                         },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "32",
                            descricao = "CCC - CHEQUES PRE - DATADOS"
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "33",
                            descricao = "C.C. - CESSAO DE CREDITOS      "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "34",
                            descricao = "CONTA CREDITO BUSINESS ACCOUNT "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "35",
                            descricao = "CONTA CORRENTE CAUCIONADA EURO "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "36",
                            descricao = "CRED. CH.PRE - DATADOS EURO   "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "38",
                            descricao = "CREDITO A CONSTRUCAO - CCC   "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "52",
                            descricao = "CCC POS         "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "53",
                            descricao = "CCC POS P    "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "54",
                            descricao = "CCC PLUS E    "
                        },
                         new ArvoreFamiliaProdutos()
                         {
                             tipologiaRisco = Constantes.tipologiaRisco.RF,
                             familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                             codfamiliaProduto =  ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                             produto = "20",
                             subproduto = "55",
                             descricao = "CCC PLUS E    "
                         },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "60",
                            descricao = "CONTA CORRENTE TOP BROKER   "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto =ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "61",
                            descricao = "CONTA CORRENTE FACTORING    "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "62",
                            descricao = "CONTA CORRENTE FACTORING   "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "63",
                            descricao = "CONTA CREDITO LIQUIDEZ     "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "64",
                            descricao = "CONTA CREDITO NEGOCIOS        "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "65",
                            descricao = "CONTA CORR FACT-EMP SEM RECUR"
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "66",
                            descricao = "CONTA CORR FACT-ENI SEM RECUR"
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "70",
                            descricao = "CONTA CR. C/CAUCAO EURO   "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "71",
                            descricao = "CC- CESSAO DE FACTURAS ENIS  "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "72",
                            descricao = "CCL PRE APROVADA PREMIER     "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "80",
                            descricao = "CONTA CORRENTE RECOV PART  "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "81",
                            descricao = "CONTA CORRENTE RECOV EMP   "
                        },
                         new ArvoreFamiliaProdutos()
                         {
                             tipologiaRisco = Constantes.tipologiaRisco.RF,
                             familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                             codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                             produto = "20",
                             subproduto = "85",
                             descricao = "CONTA CORRENTE PARTICULAR GF"
                         },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "86",
                            descricao = "CONTA CORRENTE PARTICULAR GH  "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "88",
                            descricao = "CC P GF APROV   "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "B1",
                            descricao = "CCN        "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "B2",
                            descricao = "CONTA CORRENTE       "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "B3",
                            descricao = "CONTA CORRENTE       "
                        },
                         new ArvoreFamiliaProdutos()
                         {
                             tipologiaRisco = Constantes.tipologiaRisco.RF,
                             familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                             codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                             produto = "20",
                             subproduto = "B6",
                             descricao = "CONTA CORRENTE       "
                         },
                          new ArvoreFamiliaProdutos()
                          {
                              tipologiaRisco = Constantes.tipologiaRisco.RF,
                              familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                              codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                              produto = "20",
                              subproduto = "B7",
                              descricao = "CONTA CORRENTE       "
                          },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "C6",
                            descricao = "ADIANT IVA-EMP    "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RF,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                            produto = "20",
                            subproduto = "C7",
                            descricao = "ADIANT IVA-ENI   "
                        },
                        //new ArvoreFamiliaProdutos()
                        //{
                        //    tipologiaRisco = Constantes.tipologiaRisco.RF,
                        //    familiaProduto = "CCC's",
                        //    produto = "20",
                        //    subproduto = "C8",
                        //    descricao = "CCC BOAS VINDAS   "
                        //},
                         new ArvoreFamiliaProdutos()
                         {
                             tipologiaRisco = Constantes.tipologiaRisco.RF,
                             familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                             codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                             produto = "24",
                             subproduto = "12",
                             descricao = "CONTA CORRENTE EMPRESAS ME   "
                         },
                          new ArvoreFamiliaProdutos()
                          {
                              tipologiaRisco = Constantes.tipologiaRisco.RF,
                              familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                              codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                              produto = "24",
                              subproduto = "35",
                              descricao = "CONTA CORRENTE M/E    "
                          },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                                produto = "24",
                                subproduto = "61",
                                descricao = "CONTA CORRENTE FACTORING "
                            },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                                produto = "24",
                                subproduto = "62",
                                descricao = "CONTA CORRENTE FACTORING  "
                            },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                                produto = "24",
                                subproduto = "65",
                                descricao = "CONTA CORR FACT-EMP SM RECUR   "
                            },
                             new ArvoreFamiliaProdutos()
                             {
                                 tipologiaRisco = Constantes.tipologiaRisco.RF,
                                 familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                                 codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                                 produto = "24",
                                 subproduto = "66",
                                 descricao = "CONTA CORR FACT-ENI SEM RECUR  "
                             },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                                produto = "24",
                                subproduto = "76",
                                descricao = "CONTA CORRENTE EXPORTACAO ME   "
                            },
                             new ArvoreFamiliaProdutos()
                             {
                                 tipologiaRisco = Constantes.tipologiaRisco.RF,
                                 familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                                 codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                                 produto = "24",
                                 subproduto = "80",
                                 descricao = "CONTA CORRENTE RECOVERIES PART "
                             },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CCCs.codeFamProd,
                                produto = "24",
                                subproduto = "81",
                                descricao = "CONTA CORRENTE RECOVERIES EMP  "
                            },
                             new ArvoreFamiliaProdutos()
                             {
                                 tipologiaRisco = Constantes.tipologiaRisco.RF,
                                 familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                                 codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                                 produto = "34",
                                 subproduto = "01",
                                 descricao = "LIVRANCA SEM AVAL"
                             },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                                produto = "34",
                                subproduto = "02",
                                descricao = "LIVRANCA COM AVAL"
                            },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                                produto = "34",
                                subproduto = "03",
                                descricao = "LIVRANCA (FORCA MORAL)"
                            },
                             new ArvoreFamiliaProdutos()
                             {
                                 tipologiaRisco = Constantes.tipologiaRisco.RF,
                                 familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                                 codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                                 produto = "92",
                                 subproduto = "00",
                                 descricao = "DESCONTO DE LIVRANCA - CURTO PRAZO"
                             },
                            //new ArvoreFamiliaProdutos()
                            //{
                            //    tipologiaRisco = Constantes.tipologiaRisco.RF,
                            //    familiaProduto = "Livranças",
                            //    produto = "95",
                            //    subproduto = "02",
                            //    descricao = "DESCONTO LIVRANCA - CURTO PRAZO"
                            //},
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                                produto = "96",
                                subproduto = "01",
                                descricao = "LIVRANCAS PARTICULARES"
                            },
                             new ArvoreFamiliaProdutos()
                             {
                                 tipologiaRisco = Constantes.tipologiaRisco.RF,
                                 familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                                 codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                                 produto = "96",
                                 subproduto = "02",
                                 descricao = "LIVRANCAS EMPRESAS"
                             },
                            new ArvoreFamiliaProdutos()
                            {
                                tipologiaRisco = Constantes.tipologiaRisco.RF,
                                familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.descFamProd,
                                codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_Livrancas.codeFamProd,
                                produto = "98",
                                subproduto = "04",
                                descricao = "LIVRANCA"
                            },
                             new ArvoreFamiliaProdutos()
                             {
                                 tipologiaRisco = Constantes.tipologiaRisco.RF,
                                 familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                 codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                 produto = "28",
                                 subproduto = "21",
                                 descricao = "MTL COM GAR. HIPOTECARIA"
                             },
                             new ArvoreFamiliaProdutos()
                             {
                                 tipologiaRisco = Constantes.tipologiaRisco.RF,
                                 familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                 codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                 produto = "86",
                                 subproduto = "04",
                                 descricao = "MTL COM CARENCIA"
                             },
                              new ArvoreFamiliaProdutos()
                              {
                                  tipologiaRisco = Constantes.tipologiaRisco.RF,
                                  familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                  codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                  produto = "86",
                                  subproduto = "05",
                                  descricao = "MTL COM CARENCIA E COM VALOR RESIDUAL"
                              },
                               new ArvoreFamiliaProdutos()
                               {
                                   tipologiaRisco = Constantes.tipologiaRisco.RF,
                                   familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                   codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                   produto = "86",
                                   subproduto = "06",
                                   descricao = "MTL - PME INVEST"
                               },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                    produto = "86",
                                    subproduto = "07",
                                    descricao = "MTL C / PENHOR MERCANTIL"
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                    produto = "86",
                                    subproduto = "10",
                                    descricao = "MTL - QREN"
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                    produto = "86",
                                    subproduto = "12",
                                    descricao = "MTL - PME INVEST MADEIRA"
                                },
                                 new ArvoreFamiliaProdutos()
                                 {
                                     tipologiaRisco = Constantes.tipologiaRisco.RF,
                                     familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.descFamProd,
                                     codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_CreditoNegociosEmpresasMTL.codeFamProd,
                                     produto = "86",
                                     subproduto = "12",
                                     descricao = "MTL PROTECAO VIDA"
                                 },
                                 new ArvoreFamiliaProdutos()
                                 {
                                     tipologiaRisco = Constantes.tipologiaRisco.RF,
                                     familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoPagaImpostos.descFamProd,
                                     codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoPagaImpostos.codeFamProd,
                                     produto = "",
                                     subproduto = "",
                                     descricao = "NA"
                                 },
                                  new ArvoreFamiliaProdutos()
                                  {
                                      tipologiaRisco = Constantes.tipologiaRisco.RF,
                                      familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosSobreCheques.descFamProd,
                                      codfamiliaProduto =ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosSobreCheques.codeFamProd,
                                      produto = "",
                                      subproduto = "",
                                      descricao = "NA"
                                  },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.codeFamProd,
                                    produto = "20",
                                    subproduto = "C2",
                                    descricao = "CONTA ADT IVA     "
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_AdiantamentosIVA.codeFamProd,
                                    produto = "20",
                                    subproduto = "C3",
                                    descricao = "CONTA ADT IVA     "
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_PagamentoFornecedores.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_PagamentoFornecedores.codeFamProd,
                                    produto = "82",
                                    subproduto = "01",
                                    descricao = "PAGAMENTO A FORNECEDORES"
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_PagamentoFornecedores.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_PagamentoFornecedores.codeFamProd,
                                    produto = "82",
                                    subproduto = "11",
                                    descricao = "PAGAMENTO A FORNECEDORES INTERNACIONAL"
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoDocumentos.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoDocumentos.codeFamProd,
                                    produto = "85",
                                    subproduto = "00",
                                    descricao = "EXPORTACAO - DESCONTOS C/ENVIO DE DOC."
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoDocumentos.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoDocumentos.codeFamProd,
                                    produto = "85",
                                    subproduto = "03",
                                    descricao = "FINANC EXPORT C/ ENVIO DOCUM - CONTRATO"
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoDocumentos.codeFamProd,
                                    produto = "85",
                                    subproduto = "01",
                                    descricao = "EXPORTACAO - DESCONTOS S/ENVIO DE DOC."
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoSDocumentos.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoDocumentos.codeFamProd,
                                    produto = "85",
                                    subproduto = "04",
                                    descricao = "FINANC EXPORT S/ ENVIO DOCUM - CONTRATO"
                                },
                                new ArvoreFamiliaProdutos()
                                {
                                    tipologiaRisco = Constantes.tipologiaRisco.RF,
                                    familiaProduto =ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoImportacao.descFamProd,
                                    codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.F_FinanciamentoExportacaoDocumentos.codeFamProd,
                                    produto = "",
                                    subproduto = "",
                                    descricao = "NA"
                                }
                   };

                case Constantes.tipologiaRisco.RA:
                    return new List<ArvoreFamiliaProdutos>() {
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RA,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancTécnicas.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancTécnicas.codeFamProd,
                            produto = "97",
                            subproduto = "15",
                            descricao= "GARANTIAS TECNICAS C/ PRAZO"
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RA,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancFinanceiras.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancTécnicas.codeFamProd,
                            produto = "97",
                            subproduto = "16",
                            descricao= "GARANTIAS FINANCEIRAS C/PRAZO"
                        },
                         new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RA,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancFinanceiras.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancFinanceiras.codeFamProd,
                            produto = "97",
                            subproduto = "17",
                            descricao= "GARANTIAS FINANCEIRAS OU TECNICAS  S/PRAZO"
                        },
                          new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RA,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancFinanceiras.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancFinanceiras.codeFamProd,
                            produto = "97",
                            subproduto = "20",
                            descricao= "GARANTIAS FINANCEIRAS SEM PRAZO SEM PRE-AVISO"
                        },
                           new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RA,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancAvalesBancarios.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancFinanceiras.codeFamProd,
                            produto = "",
                            subproduto = "",
                            descricao= "NA"
                        },
                            new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco = Constantes.tipologiaRisco.RA,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_CreditoDocumentarioImportação.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.A_GarantiasBancFinanceiras.codeFamProd,
                            produto = "97",
                            subproduto = "25",
                            descricao= "CREDITO DOCUMENTARIO A IMPORTACAO"
                        }
                    };
                case Constantes.tipologiaRisco.RC:
                    return new List<ArvoreFamiliaProdutos>() {
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto =  ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "20",
                            subproduto = "60",
                            descricao = "CONTA CORRENTE FACTORING    "
                        },
                        new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto =  ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "20",
                            subproduto = "62",
                            descricao = "CONTA CORRENTE FACTORING"
                        },
                         new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto =  ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "20",
                            subproduto = "65",
                            descricao = "CONTA CORR FACT-EMP SEM RECUR "
                        },
                          new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto =  ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "20",
                            subproduto = "66",
                            descricao = "CONTA CORR FACT-ENI SEM RECUR "
                        },
                          new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto =  ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "24",
                            subproduto = "61",
                            descricao = "CONTA CORRENTE FACTORING       "
                        },
                           new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "24",
                            subproduto = "62",
                            descricao = "CONTA CORRENTE FACTORING       "
                        },
                            new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "24",
                            subproduto = "65",
                            descricao = "CONTA CORR FACT-EMP SM RECUR  "
                        },
                              new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "24",
                            subproduto = "66",
                            descricao = "CONTA CORR FACT-ENI SEM RECUR  "
                        },
                               new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_Letras.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "96",
                            subproduto = "03",
                            descricao = "LETRAS"
                        },
                                new ArvoreFamiliaProdutos()
                        {
                            tipologiaRisco= Constantes.tipologiaRisco.RC,
                            familiaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_Letras.descFamProd,
                            codfamiliaProduto = ArvoreFamiliaProdutos.FamiliaProdutos.C_FactoringCSRecurso.codeFamProd,
                            produto = "96",
                            subproduto = "04",
                            descricao = "LETRAS A COBRANCA"
                        }

                };
                default:
                    return lstarvprd;
            }
        }
    }
}

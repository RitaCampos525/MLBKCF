using MultilinhaObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class ML_Objectos
    {
        public static List<MultilinhaObjects.ComboBox> GetTipologias()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "MLB",
                    Description = "ML Base",
                },
                  new ComboBox()
                {
                    Code = "MLA",
                    Description = "ML Avançado",
                },
            };
        }

        public static List<MultilinhaObjects.ComboBox> GetPeriocidade()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "M01",
                    Description = "MENSAL",
                },
                  new ComboBox()
                {
                    Code = "M02",
                    Description = "TRIMESTRAL",
                },
                  new ComboBox()
                {
                    Code = "M03",
                    Description = "SIMESTRAL",
                },
                  new ComboBox()
                {
                    Code = "M12",
                    Description = "ANUAL",
                },
                   new ComboBox()
                {
                    Code = "M24",
                    Description = "BIANUAL",
                },
            };
        }

        public static List<MultilinhaObjects.ComboBox> GetIndicadorRenovacao()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "S",
                    Description = "SIM",
                },
                  new ComboBox()
                {
                    Code = "N",
                    Description = "NÃO",
                },
        };
        }

        public static List<MultilinhaObjects.ComboBox> GetEstadosDoContratoML()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "1",
                    Description = "EM CRIACAO",
                },
                  new ComboBox()
                {
                    Code = "2",
                    Description = "ACTIVO",
                },
                    new ComboBox()
                {
                    Code = "3",
                    Description = "EM MODIFICACAO",
                },
                  new ComboBox()
                {
                    Code = "4",
                    Description = "EM APROVACAO",
                },
                new ComboBox()
                {
                    Code = "5",
                    Description = "APROVADO",
                },
                   
         };
        }

        public static List<MultilinhaObjects.ComboBox> GetTiposPedidoML()
        {
            return new List<ComboBox>(){
                 new ComboBox()
                {
                    Code = "CTR",
                    Description = "Contratação",
                },
                  new ComboBox()
                {
                    Code = "ALT",
                    Description = "Alteração",
                },
               
            };
        }

        public static List<AlteracaoContratoML> GetMotivosAlteracaoContrato()
        {
            return new List<AlteracaoContratoML>(){
                 new AlteracaoContratoML()
                {
                    Code = "A1",
                    Description = "Limite",
                },
                  new AlteracaoContratoML()
                {
                    Code = "A2",
                    Description = "Limite Risco",
                },
                new AlteracaoContratoML()
                {
                    Code = "A3",
                    Description = "Prazo",
                    tipoAlteracaoCodigo = "CG",
                    tipoAlteracaoNome = "Condições Gerais"
                },
                    new AlteracaoContratoML()
                {
                    Code = "A4",
                    Description = "Renovação",
                    tipoAlteracaoCodigo = "CG",
                    tipoAlteracaoNome = "Condições Gerais"
                },
                    new AlteracaoContratoML()
                {
                    Code = "A5",
                    Description = "Denúncia",
                    tipoAlteracaoCodigo = "CG",
                    tipoAlteracaoNome = "Condições Gerais"
                },
                    new AlteracaoContratoML()
                {
                    Code = "A5",
                    Description = "Resolução",
                    tipoAlteracaoCodigo = "CG",
                    tipoAlteracaoNome = "Condições Gerais"
                },
                    new AlteracaoContratoML()
                {
                    Code = "A6",
                    Description = "Envio de Cartas",
                    tipoAlteracaoCodigo = "CG",
                    tipoAlteracaoNome = "Condições Gerais"
                },
                    new AlteracaoContratoML()
                {
                    Code = "A7",
                    Description = "Sublimite Produto",
                    tipoAlteracaoCodigo = "CP",
                    tipoAlteracaoNome = "Condições Particulares"
                },
                    new AlteracaoContratoML()
                {
                    Code = "A8",
                    Description = "Activação CP",
                    tipoAlteracaoCodigo = "CP",
                    tipoAlteracaoNome = "Condições Particulares"
                }
                    
        };
        }

        public class AlteracaoContratoML
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public string tipoAlteracaoNome { get; set; }
            public string tipoAlteracaoCodigo { get; set; } 
        }
    }
}

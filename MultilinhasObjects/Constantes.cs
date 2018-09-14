using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public static class Constantes
    {
       
        public static class tipologiaRisco
        {
            public const string RA = "Risco Assinatura";
            public const string RF = "Risco Financeiro";
            public const string RC = "Risco Comercial";
        }

        public class Mensagens
        {
            #region ML

            public const string LM31CatalogoCriado =  "Produto ML criado em Catalogo.";
            public const string LM31CatalogoModificado = "Produto ML modificado em Catalogo";
            public const string LM32PedidoAprovado = "LM - Pedido Aprovado";
            public const string LM32PedidoRejeitado = "LM - Pedido Rejeitado";
            public const string LM33ContratoCriado = "Contrato Criado. Continue o processo seleccionando 'Seguinte'";
            public const string LM33ContratoModificado = "Contrato ML modificado. Continue o processo seleccionando 'Seguinte'";
            public const string LM34SublimiteCriado = "Sublimite Criado. Continue o processo seleccionando 'Seguinte'";
            public const string LM33PropostaInexistente = "Nº da Proposta da Workflow não encontrado";
            public const string LM37SimulacaoCriado = "LM - Simulação ML criada";
            public const string LM37Formularioinvalido = "LM - Verifique os valores do formulário.";
            public const string ProdutoMLNIdentificado = "Produto não identificado como produto Multilinha. A descrição do produto deverá indicar se o produto é Base ou Avançado";
            public const string NMinimoProdutosCredito = "Selecione no máximo 100 produtos de risco";
            public const string NMinimoProdutosRiscoA = "Selecione no máximo 17 produtos de risco";
            public const string NMinimoProdutosRiscoB = "Selecione no máximo 9 produtos de risco";
            public const string NMinimoProdutosRiscoF = "Selecione no máximo 60 subprodutos de risco financeiro";
            public const string NMinimoProdutosRiscoAs = "Selecione no máximo 20 subprodutos de risco assinatura";
            public const string NMinimoProdutosRiscoC = "Selecione no máximo 20 subprodutos de risco comercial";
            public const string NMinimoProdutosML = "Selecione no mínimo {0} produto(s) de crédito";
            public const string NMinimoProdutosML_CP = "Aceite as condições particulares de {0} subproduto(s) de crédito";
            public const string ValorSublimitesRiscoInvalido = "Verifique os valores dos sublimites de risco. O valor do sublimite não pode exceder o limite global Multilinha";
            public const string ValorSublimiteRiscoInvalido = "Valor de sublimite superior ao limite global Multilinha";
            public const string SomaSublimitesComprometidosAssinaturaInvalida = "Verifique os valores de sublimites comprometidos. O sublimite de risco assinatura foi ultrapassado";
            public const string SomaSublimitesComprometidosComercialInvalida = "Verifique os valores de sublimites comprometidos. O sublimite de risco comercial foi ultrapassado";
            public const string SomaSublimitesComprometidosFinanceiroInvalida = "Verifique os valores de sublimites comprometidos. O sublimite de risco financeiro foi ultrapassado";
            public const string SomaTotalSublimitesComprometidosInvalida = "Verifique os valores de sublimites comprometidos. A soma dos sublimites deverá ser igual ao Limite Global comprometido";

            #endregion

            #region BCDWS
            public const string UtilizadorSessaoInvalida = "Utilizador com sessão inválida";
            public const string UserRequesterInvalido = "UserRequester inválido";
            public const string UtilizadorSemAcesso = "Utilizador não tem acesso à transacção";
            public const string SistemaIndisponivel = "Erro de sistema. Contacte o HelpDesk.";
            public const string BalcaoInativo = "Balcão Inativo";
            #endregion
        }

        public static string NovaMensagem(this string text, string[] args)
        {
            string a = args[0];
            for(int i = 1; i < args.Length; i++)
            {
                a += "," + args[i];
                  
            }
            return string.Format(text, a); ;
        }
        
    }
}

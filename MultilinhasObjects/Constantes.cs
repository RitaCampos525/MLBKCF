using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class Constantes
    {
       
        public static class tipologiaRisco
        {
            public const string RA = "Risco Assinatura";
            public const string RF = "Risco Financeiro";
            public const string RC = "Risco Comercial";
        }

        public class Mensagens
        {
            #region

            public const string LM33ContratoCriado = "Contrato Criado. Continue o processo seleccionando 'Seguinte'";
            public const string LM34SublimiteCriado = "Sublimite Criado. Continue o processo seleccionando 'Seguinte'";
            public const string LM33PropostaInexistente = "Nº da Proposta da Workflow não encontrado";
            public const string ProdutoMLNIdentificado = "Produto não identificado como produto Multilinha. A descrição do produto deverá indicar se o produto é Base ou Avançado";
            public const string NMinimoProdutosRiscoA = "Selecione 17 produtos de risco";
            public const string NMinimoProdutosRiscoB = "Selecione 9 produtos de risco";
            public const string SomaSublimitesComprometidosAssinaturaInvalida = "Verifique os valores de sublimites comprometidos. O sublimite de risco assinatura foi ultrapassado";
            public const string SomaSublimitesComprometidosComercialInvalida = "Verifique os valores de sublimites comprometidos. O sublimite de risco comercial foi ultrapassado";
            public const string SomaSublimitesComprometidosFinanceiroInvalida = "Verifique os valores de sublimites comprometidos. O sublimite de risco financeiro foi ultrapassado";

            #endregion

            #region BCDWS
            public const string UtilizadorSessaoInvalida = "Utilizador com sessão inválida";
            public const string UserRequesterInvalido = "UserRequester inválido";
            public const string UtilizadorSemAcesso = "Utilizador não tem acesso à transacção";
            public const string SistemaIndisponivel = "Erro de sistema. Contacte o HelpDesk.";
            public const string BalcaoInativo = "Balcão Inativo";
            #endregion
        }
        
    }
}

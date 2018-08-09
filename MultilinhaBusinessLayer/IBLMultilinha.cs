using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MultilinhaBusinessLayer
{
    [ServiceContract]
    public interface IBLMultilinha
    {
        [OperationContract]
        MensagemOutput<List<string>> CL55Request(int nCliente, ABUtil.ABCommandArgs abargs);

        [OperationContract]
        MensagemOutput<LM31_CatalogoProdutoML> LM31Request(LM31_CatalogoProdutoML _lm31, ABUtil.ABCommandArgs abargs, string accao);

        [OperationContract]
        MensagemOutput<LM33_ContratoML> LM33Request(LM33_ContratoML _LM33, ABUtil.ABCommandArgs abargs, string accao);
    }
}

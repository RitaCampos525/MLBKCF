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
        MensagemOutput<List<string>> CL55Request();

        [OperationContract]
        MensagemOutput<List<string>> LM31Request();
    }
}

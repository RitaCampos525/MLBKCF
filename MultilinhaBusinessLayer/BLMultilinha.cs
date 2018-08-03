using MultilinhasObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultilinhaBusinessLayer
{
    
    public class BLMultilinha
    {
        MultilinhasDataLayer.DLMultilinha dl = new MultilinhasDataLayer.DLMultilinha();

        public MensagemOutput<List<string>> CL55Request(int nCliente, ABUtil.ABCommandArgs abargs )
        {
            MensagemOutput<List<string>> msgOut = new MensagemOutput<List<string>>();
            MultilinhasDataLayer.BCDWSProxy.CL55Transaction response = dl.CL55Request(abargs, nCliente, "V", "1", "1");

            msgOut.erro = response.Erro.CodigoErro;
            msgOut.mensagem = response.Erro.MensagemErro;

            List<string> lst = new List<string>();
            if (response.Row1 != null)
            {
                foreach (var a in response.Row1)
                {
                    string nConta = string.Format("{0}{1}{2}{3}", a.cbalcao_l, a.cproduto_l, a.cnumecta_l, a.cdigicta_l);
                    lst.Add(nConta);
                }
            }

            msgOut.ResultResult = lst;
            return msgOut;

        }
    }
}

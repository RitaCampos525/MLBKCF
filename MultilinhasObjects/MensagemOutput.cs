using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultilinhasObjects
{
    public class MensagemOutput<T>
    {
        public int erro { get; set; }
        public string mensagem { get; set; }

        public T ResultResult { get; set; }

    }
}

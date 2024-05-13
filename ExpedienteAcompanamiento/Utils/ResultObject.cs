using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpedienteAcompanamiento.Utils
{
    public class ResultObject
    {        
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string uiMessage { get; set; }
        public object Value { get; set; }
    }
}
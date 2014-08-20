using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface
{
    public class StateInterfaceParameterValidationException : ApplicationException
    {
        public StateInterfaceParameterValidationException(string message)
            : base(message)
        {
        }
    }
}
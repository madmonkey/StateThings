using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface
{
    public class ViewModelValidationException : ApplicationException
    {
        public ViewModelValidationException(string message)
            : base(message)
        {
        }
    }
}
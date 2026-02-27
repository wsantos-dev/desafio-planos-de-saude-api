using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PlanosSaude.API.Errors.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message)
            : base(message, (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
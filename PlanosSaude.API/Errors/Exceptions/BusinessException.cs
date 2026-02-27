using System.Net;

namespace PlanosSaude.API.Errors.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message)
            : base(message, (int)HttpStatusCode.Conflict)
        {
        }
    }
}
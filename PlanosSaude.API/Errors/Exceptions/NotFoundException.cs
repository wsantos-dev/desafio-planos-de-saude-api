using System.Net;

namespace PlanosSaude.API.Errors.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message)
          : base(message, (int)HttpStatusCode.NotFound)
        {
        }
    }
}
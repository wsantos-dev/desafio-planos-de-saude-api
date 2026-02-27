using System.Net.Mail;

namespace PlanosSaude.API.Validators
{
    public static class EmailValidator
    {
        public static bool EmailValido(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
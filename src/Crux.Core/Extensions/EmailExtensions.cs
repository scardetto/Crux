using System.IO;
using System.Text.RegularExpressions;

namespace Crux.Core.Extensions
{
    public static class EmailExtensions
    {
        private const string EMAIL_REGEX = @"^(?:[a-zA-Z0-9_^&/+-])+(?:\.(?:[a-zA-Z0-9_^&/+-])+)*@(?:(?:\[?(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))\.){3}(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\]?)|(?:[a-zA-Z0-9-]+\.)+(?:[a-zA-Z]){2,}\.?)$";

        private static readonly Regex EMAIL_VALIDATOR = new Regex(EMAIL_REGEX, RegexOptions.Compiled);

        public static bool IsEmailAddress(this string email)
        {
            return EMAIL_VALIDATOR.IsMatch(email);
        }

        public static void AssertValidEmailAddress(this string email)
        {
            if (!email.IsEmailAddress()) {
                throw new InvalidDataException("{0} is not a valid email address".ToFormat(email));
            }
        }
    }
}

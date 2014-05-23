using System.IO;
using System.Text.RegularExpressions;

namespace Crux.Core.Extensions
{
   public static class TelcoExtension
   {
       private const string USPHONE_REGEX = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
       private static readonly Regex USPHONE_VALIDATOR = new Regex(USPHONE_REGEX, RegexOptions.Compiled);

       public static bool IsUSPhoneNumber(this string phone)
       {
           return USPHONE_VALIDATOR.IsMatch(phone);
       }

       public static void AssertValidUSPhoneNumber(this string phone)
       {
           if (!phone.IsUSPhoneNumber()) {
               throw new InvalidDataException("{0} is not a valid US phone number.".ToFormat(phone));
           }
       }

   }
}

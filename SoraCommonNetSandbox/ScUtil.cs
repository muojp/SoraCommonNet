using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoraCommonNetSandbox
{
    public static class ScUtil
    {
        public static IActionResult GetGenericBadSoracomRequest(this Controller controller)
        {
            return controller.HttpBadRequest(new { code = "COMXXXX", message = "bad request" });
        }

        public static bool IsCompliantForPassword(this string candidate)
        {
            if (candidate.Length < 8 || 100 < candidate.Length || 
                !(Regex.IsMatch(candidate, "[a-z]") && Regex.IsMatch(candidate, "[A-Z]") && Regex.IsMatch(candidate, "[0-9]")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsCompliantForEmail(this string candidate)
        {
            return candidate.Contains("@");
        }

        public static bool IsCompliantForToken(this string candidate)
        {
            return 32 <= candidate.Length;
        }
    }
}

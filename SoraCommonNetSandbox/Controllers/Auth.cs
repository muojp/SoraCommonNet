using Microsoft.AspNet.Mvc;

namespace SoraCommonNetSandbox.Controllers
{
    [Route("/[controller]")]
    public class AuthController : Controller
    {
        public class AuthOperatorRequest
        {
            public string email { get; set; }
            public string password { get; set; }
            public long tokenTimeoutSeconds { get; set; }
        }
        [HttpPost]
        public IActionResult Post([FromBody] AuthOperatorRequest req)
        {
            if (req?.email == null || !req.email.IsCompliantForEmail() || !req.password.IsCompliantForPassword())
            {
                return new HttpUnauthorizedResult();
            }
            return new JsonResult(new { });
        }

        public class PasswordResetTokenRequest
        {
            public string email { get; set; }
        }
        [HttpPost("password_reset_token/issue")]
        public IActionResult Post([FromBody] PasswordResetTokenRequest req)
        {
            if (req?.email == null || !req.email.IsCompliantForEmail())
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new { });
        }

        public class VerifyPasswordResetTokenRequest
        {
            public string password { get; set; }
            public string token { get; set; }
        }
        [HttpPost("password_reset_token/verify")]
        public IActionResult Post([FromBody] VerifyPasswordResetTokenRequest req)
        {
            if (req?.password == null || req?.token == null || !req.password.IsCompliantForPassword() || !req.token.IsCompliantForToken())
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new { });
        }

        public class VerifyOperatorRequest
        {
            public string token { get; set; }
        }
        [HttpPost("verify")]
        public IActionResult Post([FromBody] VerifyOperatorRequest req)
        {
            if (req?.token != null && !req.token.IsCompliantForToken())
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new { });
        }
    }
}

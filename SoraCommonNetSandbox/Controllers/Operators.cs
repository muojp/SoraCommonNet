using Microsoft.AspNet.Mvc;
using System;

namespace SoraCommonNetSandbox.Controllers
{
    [Route("/[controller]")]
    public class OperatorsController : Controller
    {
        public class CreateOperatorRequest
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateOperatorRequest req)
        {
            if (req?.email == null || req?.password == null || !req.email.IsCompliantForEmail() || !req.password.IsCompliantForPassword())
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new CreatedResult("dummy", null);
        }

        public class VerifyOperatorRequest
        {
            public string token { get; set; }
        }
        [HttpPost("verify")]
        public IActionResult Post([FromBody] VerifyOperatorRequest req)
        {
            if (req?.token == null || !req.token.IsCompliantForToken())
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new { });
        }

        public class RefreshTokenRequest
        {
            public int timeout_seconds { get; set; }
        }
        [HttpPost("{operator_id}/token")]
        public IActionResult Post(string operator_id, [FromBody] RefreshTokenRequest req)
        {
            if (String.IsNullOrEmpty(operator_id) || req.timeout_seconds < 0)
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new { apiToken = "newtokenstring_newtokenstring_newtokenstring_newtokenstring_newtokenstring" });
        }

        public class UpdatePasswordRequest
        {
            public string currentPassword { get; set; }
            public string newPassword { get; set; }
        }
        [HttpPost("{operator_id}/password")]
        public IActionResult Post(string operator_id, [FromBody] UpdatePasswordRequest req)
        {
            if (String.IsNullOrEmpty(operator_id) || req?.currentPassword == null || req?.newPassword == null ||
                !req.currentPassword.IsCompliantForPassword() || !req.newPassword.IsCompliantForPassword())
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new { });
        }

        public class CreateSupportAccessRequest
        {
        }
        [HttpPost("{operator_id}/support/token")]
        public IActionResult Post(string operator_id, [FromBody] CreateSupportAccessRequest req)
        {
            if (String.IsNullOrEmpty(operator_id))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new { token = "supporttoken_supporttoken_supporttoken_supporttoken_supporttoken" });
        }

        [HttpGet("{operator_id}")]
        public IActionResult Get(string operator_id)
        {
            if (String.IsNullOrEmpty(operator_id))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(new
            {
                createDate = "createDate",
                description = "description",
                email = "email@example.com",
                operatorId = "FOO0123456789",
                rootOperatorId = "ROOT0123456789",
                updateDate = "updateDate"
            });
        }
    }
}

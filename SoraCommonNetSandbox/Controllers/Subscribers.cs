using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;

namespace SoraCommonNetSandbox.Controllers
{
    [Route("/[controller]")]
    public class SubscribersController : Controller
    {
        static object dummySubscriber = new
        {
            imsi = "imsi",
            msisdn = "msisdn",
            ipAddress = "ipAddress",
            apn = "apn",
            speed_class = "s1.standard",
            createdAt = 0,
            lastModifiedAt = 0,
            expiryTime = 0,
            status = "status",
            tags = new { },
            operatorId = "FOO0123456789"
        };

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new[] { dummySubscriber });
        }

        public class RegisterSubscriberRequest
        {
            public string registrationSecret { get; set; }
            public string groupId { get; set; }
            public string tags { get; set; }
        }
        [HttpPost("{imsi}/register")]
        public IActionResult Post(string imsi, [FromBody] RegisterSubscriberRequest req)
        {
            if (String.IsNullOrEmpty(req?.registrationSecret))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new CreatedResult("dummy", dummySubscriber);
        }

        [HttpGet("{imsi}")]
        public IActionResult GetSubscriber(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(dummySubscriber);
        }

        public class UpdateSpeedClassRequest
        {
            public string speedClass { get; set; }
        }
        [HttpPost("{imsi}/update_speed_class")]
        public IActionResult Post(string imsi, [FromBody] UpdateSpeedClassRequest req)
        {
            if (String.IsNullOrEmpty(imsi) || String.IsNullOrEmpty(req?.speedClass))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpPost("{imsi}/activate")]
        public IActionResult PostActivateSubscriber(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return HttpNotFound();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpPost("{imsi}/deactivate")]
        public IActionResult PostDeactivateSubscriber(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return HttpNotFound();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpPost("{imsi}/terminate")]
        public IActionResult PostTerminateSubscriber(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return HttpNotFound();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpPost("{imsi}/enable_termination")]
        public IActionResult PostEnableTerminationOnSubscriber(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return HttpNotFound();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpPost("{imsi}/disable_termination")]
        public IActionResult PostDisableTerminationOnSubscriber(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return HttpNotFound();
            }
            return new JsonResult(dummySubscriber);
        }

        public class SetExpiryTimeRequest
        {
            public long expiryTime { get; set; }
        }
        [HttpPost("{imsi}/set_expiry_time")]
        public IActionResult Post(string imsi, [FromBody] SetExpiryTimeRequest req)
        {
            if (String.IsNullOrEmpty(imsi) || req.expiryTime < 0)
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpPost("{imsi}/unset_expiry_time")]
        public IActionResult PostUnsetExpiryTime(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return HttpNotFound();
            }
            return new NoContentResult();
        }

        public class SetGroupRequest
        {
            public List<string> configuration { get; set; }
            public long createdTime { get; set; }
            public string groupId { get; set; }
            public long lastModifiedTime { get; set; }
            public string operatorId { get; set; }
            public Dictionary<string, string> tags{ get; set; }
        }
        [HttpPost("{imsi}/set_group")]
        public IActionResult Post(string imsi, [FromBody] SetGroupRequest req)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpPost("{imsi}/unset_group")]
        public IActionResult PostUnsetGroup(string imsi)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(dummySubscriber);
        }

        public class TagEntry
        {
            public string tagName { get; set; }
            public string tagValue { get; set; }
        }
        public class TagsRequest
        {
            public List<TagEntry> tags { get; set; }
        }
        [HttpPut("{imsi}/tags")]
        public IActionResult Put(string imsi, [FromBody] TagsRequest req)
        {
            if (String.IsNullOrEmpty(imsi))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new JsonResult(dummySubscriber);
        }

        [HttpDelete("{imsi}/tags/{tag_name}")]
        public IActionResult Delete(string imsi, string tag_name)
        {
            if (String.IsNullOrEmpty(imsi) || String.IsNullOrEmpty(tag_name))
            {
                return this.GetGenericBadSoracomRequest();
            }
            return new NoContentResult();
        }
    }
}

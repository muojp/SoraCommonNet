using System;
using System.Collections.Generic;
using static System.Net.Http.HttpMethod;
using System.Threading.Tasks;
using static SoraCommonNet.Communicator;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace SoraCommonNet
{
    public class Operator
    {
        public class OperatorInfo
        {
            [JsonProperty(PropertyName = "createDate")]
            public string CreateDate { get; set; }

            [JsonProperty(PropertyName = "description")]
            public string Description { get; set; }

            [JsonProperty(PropertyName = "email")]
            public string Email { get; set; }

            [JsonProperty(PropertyName = "operatorId")]
            public string OperatorId { get; set; }

            [JsonProperty(PropertyName = "rootOperatorId")]
            public string RootOperatorId { get; set; }

            [JsonProperty(PropertyName = "updateDate")]
            public string UpdateDate { get; set; }
        }

        const int TOKEN_TIMEOUT_SEC = 180;

        Communicator scm = null;

        private Operator(Communicator commu)
        {
            scm = commu;
        }

        public static async Task<bool> Register(string email, string password)
        {
            var msg = new { email = email, password = password };
            try
            {
                await PerformUnsignedRequest("/operators", Post, msg);
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static async Task<bool> VerifyRegistration(string token)
        {
            var msg = new { token = token };
            try
            {
                await PerformUnsignedRequest("/operators/verify", Post, msg);
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static async Task<Operator> Authenticate(string email, string password)
        {
            var commu = new Communicator();
            if (await commu.Authenticate(email, password))
            {
                // succeeded
                return new Operator(commu);
            }
            else
            {
                return null;
            }
        }

        // FIXME: not tested yet
        public static async Task IssueResetPasswordToken(string email)
        {
            var msg = new { email = email };
            await PerformUnsignedRequest("/auth/password_reset_token/issue", Post, msg);
        }

        // FIXME: not tested yet
        public static async Task VerifyResetPasswordToken(string token, string newPassword)
        {
            var msg = new { token = token, password = newPassword };
            await PerformUnsignedRequest("/auth/password_reset_token/verify", Post, msg);
        }

        public async Task<bool> RefreshToken(int? timeoutSec = null)
        {
            var msg = new { timeout_seconds = timeoutSec ?? TOKEN_TIMEOUT_SEC };
            string result = null;
            try {
                result = await scm.PerformSignedRequest("/operators/{operator_id}/token", Post, msg);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
            if (String.IsNullOrEmpty(result))
            {
                return false;
            }
            var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            if (!parsed.ContainsKey("token"))
            {
                return false;
            }
            scm.UpdateToken(parsed["token"]);
            return true;
        }

        // FIXME: not tested yet
        public async Task<bool> ChangePassword(string currentPassword, string newPassword)
        {
            var msg = new { currentPassword = currentPassword, newPassword = newPassword };
            try {
                await scm.PerformSignedRequest("/operators/{operator_id}/password", Post, msg);
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<string> FetchSupportToken()
        {
            string result = null;
            try
            {
                result = await scm.PerformSignedRequest("/operators/{operator_id}/support/token", Post);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            if (String.IsNullOrEmpty(result))
            {
                return null;
            }
            var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            return parsed.ContainsKey("token") ? parsed["token"] : null;
        }

        public async Task<OperatorInfo> FetchInfo()
        {
            string result = null;
            try
            {
                result = await scm.PerformSignedRequest("/operators/{operator_id}", Get);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            if (String.IsNullOrEmpty(result))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<OperatorInfo>(result);
        }

        public async Task<List<Subscriber>> ListSubscribers()
        {
            // FIXME: support search by conditions
            var msg = new { };
            string result = null;
            try
            {
                result = await scm.PerformSignedRequest("/subscribers", Get, msg);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            if (String.IsNullOrEmpty(result))
            {
                return null;
            }
            var subscribers = JsonConvert.DeserializeObject<List<Subscriber>>(result);
            foreach (var subscriber in subscribers)
            {
                subscriber.Communicator = scm;
            }
            return subscribers;
        }

        // FIXME: not tested yet
        public async Task<Subscriber> RegisterSubscriber(string imsi, string secret)
        {
            // FIXME: support setting groupId/tags
            var msg = new { registrationSecret = secret };
            string result = null;
            try
            {
                result = await scm.PerformSignedRequest($"/subscribers/{imsi}/register", Post, msg);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            var res = JsonConvert.DeserializeObject<Subscriber>(result);
            res.Communicator = scm;
            return res;
        }

        public async Task<Subscriber> RetrieveSubscriber(string imsi)
        {
            string result = null;
            try
            {
                result = await scm.PerformSignedRequest($"/subscribers/{imsi}", Get);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            if (String.IsNullOrEmpty(result))
            {
                return null;
            }
            var res = JsonConvert.DeserializeObject<Subscriber>(result);
            res.Communicator = scm;
            return res;
        }

    }
}

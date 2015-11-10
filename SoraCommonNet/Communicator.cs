using PortableRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Http.HttpMethod;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SoraCommonNet
{
    internal class Communicator
    {
        const string API_ENDPOINT = "https://api.soracom.io";
        const string API_PREFIX = "/v1";
        const string OPERATOR_ID_PLACEHOLDER = "{operator_id}";

        private string apiKey = null;
        private string apiToken = null;
        private string operatorId = null;

        private RestClient client = null;

        private static string GetRequestUri(string api)
        {
            return $"{API_ENDPOINT}{API_PREFIX}{api}";
        }

        public Communicator()
        {
            client = new RestClient();
        }

        private RestRequest CreateRequest(string api, HttpMethod method, object msgBody = null)
        {
            // replace operator id
            var apiPath = api.Replace(OPERATOR_ID_PLACEHOLDER, operatorId);
            var req = new RestRequest(GetRequestUri(apiPath), method);
            req.ContentType = ContentTypes.Json;
            if (msgBody != null)
            {
                req.AddParameter(msgBody);
            }
            else
            {
                if (method == HttpMethod.Post)
                {
                    req.AddParameter(new object[]{ });
                }
            }
            return req;
        }

        internal async Task<string> PerformRequest(string api, HttpMethod method, object msgBody = null, bool signed = false)
        {
            var req = CreateRequest(api, method, msgBody);
            if (signed && apiKey != null && apiToken != null)
            {
                req.AddHeader("X-Soracom-API-Key", apiKey);
                req.AddHeader("X-Soracom-Token", apiToken);
            }
            return await client.ExecuteAsync<string>(req);
        }

        internal async Task<string> PerformSignedRequest(string api, HttpMethod method, object msgBody = null)
        {
            return await PerformRequest(api, method, msgBody, signed: true);
        }

        static Communicator sharedCommu = new Communicator();
        internal static async Task<string> PerformUnsignedRequest(string api, HttpMethod method, object msgBody = null)
        {
            return await sharedCommu.PerformRequest(api, method, msgBody, signed: false);
        }

        internal async Task<bool> Authenticate(string email, string password)
        {
            var msg = new { email = email, password = password };
            try
            {
                var result = await PerformUnsignedRequest("/auth", Post, msg);
                var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                apiKey = parsed.ContainsKey("apiKey") ? parsed["apiKey"] : null;
                apiToken = parsed.ContainsKey("token") ? parsed["token"] : null;
                operatorId = parsed.ContainsKey("operatorId") ? parsed["operatorId"] : null;
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        internal void UpdateToken(string newToken)
        {
            apiToken = newToken;
        }
    }
}

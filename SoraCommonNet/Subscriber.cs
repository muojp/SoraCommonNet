using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.Http.HttpMethod;

namespace SoraCommonNet
{
    public class Subscriber
    {
        public enum SubscriberSpeedClass
        {
            S1_Minimum,
            S1_Slow,
            S1_Standard,
            S1_Fast
        };

        [JsonProperty(PropertyName = "imsi")]
        public string Imsi { get; set; }

        [JsonProperty(PropertyName = "msisdn")]
        public string Msisdn { get; set; }

        [JsonProperty(PropertyName = "ipAddress")]
        public string IpAddress { get; set; }

        [JsonProperty(PropertyName = "apn")]
        public string Apn { get; set; }

        [JsonProperty(PropertyName = "moduleType")]
        public string ModuleType { get; set; }

        [JsonProperty(PropertyName = "plan")]
        public int Plan { get; set; }

        [JsonProperty(PropertyName = "groupId")]
        public string GroupId { get; set; }

        [JsonProperty(PropertyName = "terminationEnabled")]
        public bool TerminationEnabled { get; set; }

        //FIXME: apply converter
        [JsonProperty(PropertyName = "speed_class")]
        public string SpeedClass { get; set; }
        // public SubscriberSpeedClass SpeedClass { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty(PropertyName = "lastModifiedAt")]
        public long LastModifiedAt { get; set; }

        [JsonProperty(PropertyName = "expiryTime")]
        public long? ExpiryTime { get; set; }

        [JsonProperty(PropertyName = "sessionStatus")]
        public object SessionStatus { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public Dictionary<string, string> Tags { get; set; }

        [JsonProperty(PropertyName = "operatorId")]
        public string OperatorId { get; set; }

        internal Communicator Communicator { get; set; }

        public async Task UpdateSpeedClass()
        {
            // post /subscribers/{imsi}/update_speed_class
            // ['s1.minimum', 's1.slow', 's1.standard', 's1.fast']
            throw new NotImplementedException();
        }

        public async Task<bool> Activate()
        {
            try
            {
                await Communicator.PerformSignedRequest($"/subscribers/{Imsi}/activate", Post);
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> Deactivate()
        {
            try
            {
                await Communicator.PerformSignedRequest($"/subscribers/{Imsi}/deactivate", Post);
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        // FIXME: not tested yet
        public async Task<bool> Terminate()
        {
            try
            {
                await Communicator.PerformSignedRequest($"/subscribers/{Imsi}/terminate", Post);
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> EnableTermination()
        {
            try
            {
                await Communicator.PerformSignedRequest($"/subscribers/{Imsi}/enable_termination", Post);
                TerminationEnabled = true;
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DisableTermination()
        {
            try
            {
                await Communicator.PerformSignedRequest($"/subscribers/{Imsi}/disable_termination", Post);
                TerminationEnabled = false;
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task SetExpiryTime()
        {
            // post /subscribers/{imsi}/set_expiry_time
            throw new NotImplementedException();
        }

        public async Task UnsetExpiryTime()
        {
            // post /subscribers/{imsi}/unset_expiry_time
            throw new NotImplementedException();
        }

        public async Task SetGroup()
        {
            // post /subscribers/{imsi}/set_group
            throw new NotImplementedException();
        }

        public async Task UnsetGroup()
        {
            // post /subscribers/{imsi}/unset_group
            throw new NotImplementedException();
        }

        public async Task PutTag()
        {
            // put /subscribers/{imsi}/tags
            throw new NotImplementedException();
        }

        public async Task PutTags()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTag()
        {
            // delete /subscribers/{imsi}/tags/{tag_name}
            throw new NotImplementedException();
        }

    }
}

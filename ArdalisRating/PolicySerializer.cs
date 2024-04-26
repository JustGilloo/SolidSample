using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating
{
    internal class PolicySerializer
    {
        public Policy GetPolicyFromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<Policy>(json,
                new StringEnumConverter());
        }
    }
}

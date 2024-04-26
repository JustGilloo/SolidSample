using ArdalisRating.Factories;
using ArdalisRating.Logging;
using ArdalisRating.Raters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace ArdalisRating
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        private ConsoleLogger logger = new ConsoleLogger();
        private FilePolicySource policySource = new FilePolicySource();
        private PolicySerializer policySerializer = new PolicySerializer();
        public decimal Rating { get; set; }
        public void DefineRating()
        {
            PolicyRaterFactory factory = new PolicyRaterFactory(this, logger);

            logger.LogMessage("Starting rate.");
            logger.LogMessage("Loading policy.");

            string policyJson = policySource.GetPolicyFromSource();
            Policy policy = policySerializer.GetPolicyFromJsonString(policyJson);

            IPolicyRater policyRater = factory.Create(policy.Type);
            policyRater.Rate(policy);

            logger.LogMessage("Rating completed.");
        }
    }
}

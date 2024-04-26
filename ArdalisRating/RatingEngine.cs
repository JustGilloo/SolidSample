using ArdalisRating.Logging;
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
        public void Rate()
        {
            logger.LogMessage("Starting rate.");
            logger.LogMessage("Loading policy.");

            string policyJson = policySource.GetPolicyFromSource();
            Policy policy = policySerializer.GetPolicyFromJsonString(policyJson);

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    logger.LogMessage("Rating AUTO policy...");
                    logger.LogMessage("Validating policy.");
                    if (String.IsNullOrEmpty(policy.Make))
                    {
                        logger.LogMessage("Auto policy must specify Make");
                        return;
                    }
                    if (policy.Make == "BMW")
                    {
                        if (policy.Deductible < 500)
                        {
                            Rating = 1000m;
                        }
                        Rating = 900m;
                    }
                    break;

                case PolicyType.Land:
                    logger.LogMessage("Rating LAND policy...");
                    logger.LogMessage("Validating policy.");
                    if (policy.BondAmount == 0 || policy.Valuation == 0)
                    {
                        logger.LogMessage("Land policy must specify Bond Amount and Valuation.");
                        return;
                    }
                    if (policy.BondAmount < 0.8m * policy.Valuation)
                    {
                        logger.LogMessage("Insufficient bond amount.");
                        return;
                    }
                    Rating = policy.BondAmount * 0.05m;
                    break;

                case PolicyType.Life:
                    logger.LogMessage("Rating LIFE policy...");
                    logger.LogMessage("Validating policy.");
                    if (policy.DateOfBirth == DateTime.MinValue)
                    {
                        logger.LogMessage("Life policy must include Date of Birth.");
                        return;
                    }
                    if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
                    {
                        logger.LogMessage("Centenarians are not eligible for coverage.");
                        return;
                    }
                    if (policy.Amount == 0)
                    {
                        logger.LogMessage("Life policy must include an Amount.");
                        return;
                    }
                    int age = DateTime.Today.Year - policy.DateOfBirth.Year;
                    if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                        DateTime.Today.Day < policy.DateOfBirth.Day ||
                        DateTime.Today.Month < policy.DateOfBirth.Month)
                    {
                        age--;
                    }
                    decimal baseRate = policy.Amount * age / 200;
                    if (policy.IsSmoker)
                    {
                        Rating = baseRate * 2;
                        break;
                    }
                    Rating = baseRate;
                    break;

                default:
                    logger.LogMessage("Unknown policy type");
                    break;
            }

            logger.LogMessage("Rating completed.");
        }
    }
}

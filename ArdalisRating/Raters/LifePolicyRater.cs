using ArdalisRating.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating.Raters
{
    internal class LifePolicyRater : IPolicyRater
    {
        private ConsoleLogger logger;
        private RatingEngine engine;

        public LifePolicyRater(RatingEngine engine, ConsoleLogger logger)
        {
            this.logger = logger;
            this.engine = engine;
        }

        public void Rate(Policy policy)
        {
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
                engine.Rating = baseRate * 2;
                return;
            }
            engine.Rating = baseRate;
        }
    }
}

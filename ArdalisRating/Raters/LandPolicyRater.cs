using ArdalisRating.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating.Raters
{
    internal class LandPolicyRater : IPolicyRater
    {
        private ConsoleLogger logger;
        private RatingEngine engine;

        public LandPolicyRater(RatingEngine engine, ConsoleLogger logger)
        {
            this.logger = logger;
            this.engine = engine;
        }

        public void Rate(Policy policy)
        {
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
            engine.Rating = policy.BondAmount * 0.05m;
        }
    }
}

using ArdalisRating.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating.Raters
{
    internal class AutoPolicyRater : IPolicyRater
    {
        private ConsoleLogger logger;
        private RatingEngine engine;

        public AutoPolicyRater(RatingEngine engine, ConsoleLogger logger)
        {
            this.logger = logger;
            this.engine = engine;
        }

        public void Rate(Policy policy)
        {
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
                    engine.Rating = 1000m;
                }
                engine.Rating = 900m;
            }
        }
    }
}

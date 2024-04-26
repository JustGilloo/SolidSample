using ArdalisRating.Logging;
using ArdalisRating.Raters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisRating.Factories
{
    internal class PolicyRaterFactory
    {
        private RatingEngine engine;
        private ConsoleLogger logger;

        public PolicyRaterFactory(RatingEngine engine, ConsoleLogger logger)
        {
            this.engine = engine;
            this.logger = logger;
        }

        public IPolicyRater Create(PolicyType policyType)
        {
            switch(policyType)
            {
                case PolicyType.Auto:
                    return new AutoPolicyRater(engine, logger);
                case PolicyType.Land:
                    return new LandPolicyRater(engine, logger);
                case PolicyType.Life:
                    return new LifePolicyRater(engine, logger);
                default:
                    throw new ArgumentException();
            }
        }
    }
}

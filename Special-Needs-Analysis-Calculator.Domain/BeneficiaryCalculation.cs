using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Domain
{
    public class BeneficiaryCalculation
    {
        public double RemainingDependency { get; set; }
        public double CostMonthly { get; set; }
        public double OverallMonetaryCost { get; set; }
        public double ExtraMonthlyCostSpecialNeedsDependent { get; set; }
        public bool IsUnder65 { get; set; }
        public bool SpecialNeedsTrustEligible { get; set; }
        public bool SupplementalSecurityIncomeEligible { get; set; }
        public double NetSupplementalSecurityIncome { get; set; }
        public bool SocialSecurityDisabilityInsuranceEligible { get; set; }
        public double NetSocialSecurityDisabilityInsurance { get; set; }
        public double MaxABLEContribution { get; set; }
        public double RecommendedABLEContribution { get; set; }
        public double ABLELifetimeValue { get; set; }
        public bool MedicadeEligibile { get; set; }
        public List<double> AbleAccountValues { get; set; }
        public List<double> SavingsAccountValues { get; set; }
        public List<double> PostTaxCapitalValues { get; set; }
    }
}

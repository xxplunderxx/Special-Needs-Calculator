using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Domain.SpecialNeedsCalculator
{
    public abstract class TemplateSpecialNeedsCalculator
    {
        public abstract int GetRemainingDependency();
        public abstract double GetCostMonthly();
        public abstract double OverallMonetaryCost();
        public abstract double ExtraMonetaryCost();
        public abstract bool IsUnder65();
        public abstract bool SpecialNeedsTrustEligible();
        public abstract bool SupplementalSecurityIncomeEligible();
        public abstract bool SocialSecurityDisabilityInsuranceEligible();
        public abstract double NetSocialSecurityDisabilityInsurance();
        public abstract double NetSupplementalSecurityIncome();
        public abstract double MaxABLEContribution();
        public abstract double RecommendedABLEContribution();
        public abstract double ABLELifetimeValue();
        public abstract bool MedicadeEligibile();
        public abstract List<double> AbleAccountValues();
        public abstract List<double> SavingsAccountValues();
        public abstract List<double> PostTaxCapitalValues();


        public BeneficiaryCalculation TemplateResults()
        {
            return new BeneficiaryCalculation
            {
                RemainingDependency = GetRemainingDependency(),
                CostMonthly = GetCostMonthly(),
                OverallMonetaryCost = OverallMonetaryCost(),
                ExtraMonthlyCostSpecialNeedsDependent = ExtraMonetaryCost(),
                IsUnder65 = IsUnder65(),
                SpecialNeedsTrustEligible = SpecialNeedsTrustEligible(),
                SupplementalSecurityIncomeEligible = SupplementalSecurityIncomeEligible(),
                NetSupplementalSecurityIncome = NetSupplementalSecurityIncome(),
                SocialSecurityDisabilityInsuranceEligible = SocialSecurityDisabilityInsuranceEligible(),
                NetSocialSecurityDisabilityInsurance = NetSocialSecurityDisabilityInsurance(),
                MaxABLEContribution = MaxABLEContribution(),
                RecommendedABLEContribution = RecommendedABLEContribution(),
                ABLELifetimeValue = ABLELifetimeValue(),
                MedicadeEligibile = MedicadeEligibile(),
                AbleAccountValues = AbleAccountValues(),
                SavingsAccountValues = SavingsAccountValues(),
                PostTaxCapitalValues = PostTaxCapitalValues()
            };
        }
    }
}

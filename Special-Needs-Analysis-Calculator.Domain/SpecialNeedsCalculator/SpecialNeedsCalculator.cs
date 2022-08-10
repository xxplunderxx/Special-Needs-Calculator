using Special_Needs_Analysis_Calculator.Data.Models;
using Special_Needs_Analysis_Calculator.Data.Models.People;
using Special_Needs_Analysis_Calculator.Data.Models.Person;
using static Special_Needs_Analysis_Calculator.Domain.FilingStatus;

namespace Special_Needs_Analysis_Calculator.Domain.SpecialNeedsCalculator
{
    public class SpecialNeedsCalculator : TemplateSpecialNeedsCalculator
    {
        public UserModel User { get; set; }
        public BeneficiaryModel BM { get; set; }

        // Fields which we only need to calculate once
        public double RemainingDependency { get; set; }
        public double CostMonthly { get; set; }

        // constructor
        public SpecialNeedsCalculator(UserModel user, BeneficiaryModel beneficiaryModel)
        {
            User = user;
            BM = beneficiaryModel;
            RemainingDependency = GetRemainingDependency();
            CostMonthly = GetCostMonthly();
        }

        /// <summary>
        /// Finds the remaining amount of years the benificary is
        /// expected to be dependent on the user
        /// </summary>
        /// <returns>The amount of years they will be dependent</returns>
        public override int GetRemainingDependency()
        {
            if (BM.ConditionStatus == null) return -1;    // make sure condition exists

            if (BM.ConditionStatus.IsConditionPermanent)
                return BM.ExpectedLifespan - BM.Age;
            else if (BM.IsStudent)
                return Math.Max(24 - BM.Age, 0);
            return Math.Max(19 - BM.Age, 0);
        }

        /// <summary>
        /// Finds the monthly finacial cost of a dependent
        /// </summary>
        /// <returns>the monthly cost</returns>
        public override double GetCostMonthly()
        {
            if (BM.Expenses == null) return -1;    // make sure expenses

            double costMonthly =
                BM.Expenses.Housing +
                BM.Expenses.Food +
                BM.Expenses.Utilities +
                BM.Expenses.Transportation +
                BM.Expenses.MedicalCoPay +
                BM.Expenses.Entertainment +
                BM.Expenses.ConditionCare +
                BM.Expenses.Other;

            return costMonthly;
        }

        /// <summary>
        /// The overall cost of a beneficiary throughout their expected lifespan
        /// </summary>
        /// <returns>cost of beneficiary</returns>
        public override double OverallMonetaryCost()
        {
            double costTotal = CostMonthly * 12 * RemainingDependency;
            return costTotal;
        }

        /// <summary>
        /// The additional cost of a beneficiary thorughout their life
        /// compared to a beneficiary without the additional cost
        /// </summary>
        /// <returns></returns>
        public override double ExtraMonetaryCost()
        {
            return CostMonthly - (412 + 243 + 819 + 161 + 480); // temp averages might change later
        }

        /// <summary>
        /// Determines if someone is under 65
        /// </summary>
        /// <returns>true / false</returns>
        public override bool IsUnder65()
        {
            return BM.Age < 65;
        }

        /// <summary>
        /// Determines if someone is eligable for the Special
        /// Needs Trust
        /// </summary>
        /// <returns>true/false</returns>
        public override bool SpecialNeedsTrustEligible()
        {
            return IsUnder65();
        }

        /// <summary>
        /// Determines if someone is eligable for Supplimental Security Income
        /// </summary>
        /// <returns>true/false</returns>
        public override bool SupplementalSecurityIncomeEligible()
        {
            // Individual income, not household
            double monthlyIncome = (double)BM.YearlyIncome / 12;

            if (BM.ConditionStatus == null) return false;    // make sure condition exists
            return (IsUnder65() || BM.ConditionStatus.IsLegallyBlind || BM.ConditionStatus.IsLegallyDisabled) && monthlyIncome < 2000;
        }

        /// <summary>
        /// Calculates total income from Suppimental Security
        /// </summary>
        /// <returns>income total</returns>
        public override double NetSupplementalSecurityIncome()
        {
            return BM.SupplementalSecurityIncomeMonthly * 12 * RemainingDependency;
        }

        /// <summary>
        /// Determines if someone is eligable for Social Security Disability Insurance
        /// </summary>
        /// <returns>true/false</returns>
        public override bool SocialSecurityDisabilityInsuranceEligible()
        {
            if (!BM.IsEmployed) return false;
            else if (BM.ConditionStatus == null || !BM.ConditionStatus.IsLegallyDisabled) return false;
            else if (!BM.ConditionStatus.IsLegallyBlind && BM.YearlyIncome >= 1350) return false;  // not blind income cutoff
            else if (BM.ConditionStatus.IsLegallyBlind && BM.YearlyIncome >= 2260) return false;   // blind income cutoff
            else return true;
        }

        /// <summary>
        /// Calculates the total Social Security Disability Insurance Income
        /// </summary>
        /// <returns>Insurance Income</returns>
        public override double NetSocialSecurityDisabilityInsurance()
        {
            return BM.SocialSecurityDisabilityInsuranceMonthly * 12 * RemainingDependency;
        }

        /// <summary>
        /// The maximum amount of money someone can contribute to their ABLE account
        /// without missing out on other money
        /// </summary>
        /// <returns>max amount contribution</returns>
        public override double MaxABLEContribution()
        {
            int max = 16000;
            var Numerator = BM.ABLEMaxHoldings * BM.ABLEFundRate * Math.Pow(1 + BM.ABLEFundRate, RemainingDependency);
            var Denomenator = Math.Pow(1 + BM.ABLEFundRate, RemainingDependency) - 1;
            var result = Numerator / Denomenator;

            if (result > max) return max;
            else return result;
        }

        /// <summary>
        /// The amount of money someone should contribute to their ABLE account
        /// to earn the most amount of money back
        /// </summary>
        /// <returns>reccommended contribution amount</returns>
        public override double RecommendedABLEContribution()
        {
            var Numerator = 100000 * BM.ABLEFundRate * Math.Pow(1 + BM.ABLEFundRate, RemainingDependency);
            var Denomenator = Math.Pow(1 + BM.ABLEFundRate, RemainingDependency) - 1;
            return Numerator / Denomenator;
        }

        /// <summary>
        /// The total amount of money the ABlE account will hold
        /// at the end of their lifespan
        /// </summary>
        /// <returns>amount of money in ABLE</returns>
        public override double ABLELifetimeValue()
        {
            double ableValue = BM.AnnualABLEContributions *
                (Math.Pow(1 + BM.ABLEFundRate, RemainingDependency) - 1) /
                BM.ABLEFundRate;

            return ableValue;
        }

        /// <summary>
        /// Determines whether or not the user is elgiable 
        /// for Midcade
        /// </summary>
        /// <returns>true/false</returns>
        public override bool MedicadeEligibile()
        {
            if (!SupplementalSecurityIncomeEligible()) return false;
            if (BM.YearlyIncome > 2199) return false;
            else return true;
        }

        /// <summary>
        /// Assigns method return balues to object feilds
        /// in order to be returned in a simpler way
        /// </summary>
        /// <returns>Object that holds all of the results</returns>
        public override List<double> AbleAccountValues()
        {
            double annualContribution = BM.AnnualABLEContributions;
            double growthRate = 1 + BM.ABLEFundRate;

            return AccountYearly(annualContribution, growthRate);
        }

        /// <summary>
        /// Determines yearly value of a user's
        /// ABLE contribution inside a savings account
        /// (for comparison purposes)
        /// </summary>
        /// <returns>savings account values by year</returns>
        public override List<double> SavingsAccountValues()
        {
            double annualContribution = BM.AnnualABLEContributions;
            double growthRate = 1.01;

            return AccountYearly(annualContribution, growthRate);
        }

        /// <summary>
        /// Accounts for the yearly growth in any account
        /// </summary>
        /// <param name="annualContribution">contribution to the account</param>
        /// <param name="growthRate">growth rate of the account</param>
        /// <returns>list of yearly values</returns>
        public List<double> AccountYearly(double annualContribution, double growthRate)
        {
            List<double> values = new List<double>();
            double total = 0;

            for (int i = 1; i < RemainingDependency; i++)
            {
                total = (total + annualContribution) * (growthRate);
                values.Add(Math.Round(total, 2, MidpointRounding.AwayFromZero));
            }

            return values;
        }

        /// <summary>
        /// Figures out the filing status of a user then
        /// this method determines the tax based on a set 
        /// of income brackets. Finally it subtracts the tax
        /// from the pretax version.
        /// </summary>
        /// <returns>list of post tax able values</returns>
        public override List<double> PostTaxCapitalValues()
        {
            List<double> preTaxValues = AbleAccountValues();
            List<double> postTaxValues = new List<double>();

            // single filing status
            double SingleRow1 = (double)FilingStatus.Single.Row1Max;
            double SingleRow2 = (double)FilingStatus.Single.Row2Max;

            // married joinyly filing status
            double MarriedJointRow1 = (double)MarriedJointly.Row1Max;
            double MarriedJointRow2 = (double)MarriedJointly.Row2Max;

            // married seperately filing status
            double MarriedSepRow1 = (double)MarriedSeperately.Row1Max;
            double MarriedSepRow2 = (double)MarriedSeperately.Row2Max;

            // head of household filing status
            double HeadRow1 = (double)HeadOfHousehold.Row1Max;
            double HeadRow2 = (double)HeadOfHousehold.Row2Max;

            // Each value at the end of the year
            foreach (double value in preTaxValues)
            {
                // Determine tax rate
                double taxSum = 0;

                
                if (User.TaxFilingSatus == TaxFilingStatus.Single)
                {
                    if (value > SingleRow1 && value <= SingleRow2)
                    {
                        taxSum += (value - SingleRow1) * .15;
                    }
                    else if (value > SingleRow2)
                    {
                        // determine previous range tax
                        var previousRangeTax = (SingleRow2 - SingleRow1) * .15;

                        // new range tax
                        taxSum += previousRangeTax + ((value - SingleRow2) * .20);
                    }
                }

                if (User.TaxFilingSatus == TaxFilingStatus.MarriedJointly)
                {
                    if (value > MarriedJointRow1 && value <= MarriedJointRow2)
                    {
                        taxSum += (value - MarriedJointRow1) * .15;
                    }
                    else if (value > MarriedJointRow2)
                    {
                        // determine previous range tax
                        var previousRangeTax = (MarriedJointRow2 - MarriedJointRow1) * .15;

                        // new range tax
                        taxSum += previousRangeTax + ((value - MarriedJointRow2) * .20);
                    }
                }

                if (User.TaxFilingSatus == TaxFilingStatus.MarriedSeperately)
                {
                    if (value > MarriedSepRow1 && value <= MarriedSepRow2)
                    {
                        taxSum += (value - MarriedSepRow1) * .15;
                    }
                    else if (value > MarriedSepRow2)
                    {
                        // determine previous range tax
                        var previousRangeTax = (MarriedSepRow2 - MarriedSepRow1) * .15;

                        // new range tax
                        taxSum += previousRangeTax + ((value - MarriedSepRow2) * .20);
                    }
                }

                if (User.TaxFilingSatus == TaxFilingStatus.HeadOfHousehold)
                {
                    if (value > HeadRow1 && value <= HeadRow2)
                    {
                        taxSum += (value - HeadRow1) * .15;
                    }
                    else if (value > HeadRow2)
                    {
                        // determine previous range tax
                        var previousRangeTax = (HeadRow2 - HeadRow1) * .15;

                        // new range tax
                        taxSum += previousRangeTax + ((value - HeadRow2) * .20);
                    }
                }
                postTaxValues.Add(value-taxSum);
            }
            return postTaxValues;
        }
    }
}

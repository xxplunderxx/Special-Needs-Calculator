using Special_Needs_Analysis_Calculator.Data.Models.Person;
using Special_Needs_Analysis_Calculator.Data.Models.Person.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Models.People
{
    public class BeneficiaryModel : PersonModel
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string StateOfResidence { get; set; }
        public int ExpectedLifespan { get; set; }
        public bool IsStudent { get; set; }
        public bool IsEmployed { get; set; }
        public int EmploymentYears { get; set; }
        public double SupplementalSecurityIncomeMonthly { get; set; }
        public double SocialSecurityDisabilityInsuranceMonthly { get; set; }
        public double ABLEMaxHoldings { get; set; }
        public double AnnualABLEContributions { get; set; }
        public double ABLEFundRate { get; set; }
        public ConditionStatusModel? ConditionStatus { get; set; }
        public ExpensesModel? Expenses { get; set; }
    }
}

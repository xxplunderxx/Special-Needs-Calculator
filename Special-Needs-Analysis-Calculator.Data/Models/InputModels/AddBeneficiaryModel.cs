using Special_Needs_Analysis_Calculator.Data.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Models.InputModels
{
    public class AddBeneficiaryModel 
    {
        public BeneficiaryModel BeneficiaryModel { get; set; }
        public string SessionToken { get; set; }

        public static string CheckInput(BeneficiaryModel beneficiary)
        {
            if (beneficiary.Age > beneficiary.ExpectedLifespan)
            {
                return "Age cannot be greater than Expected Lifespan";
            }
            return "";
        }
    }

    public class UpdateBeneficiaryModel : AddBeneficiaryModel 
    {
        public UpdateBeneficiaryModel() : base()
        {

        }
    }

}

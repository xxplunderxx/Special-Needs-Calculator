using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Models.Person.Info
{
    public class ConditionStatusModel
    {
        public bool IsConditionPermanent { get; set; }
        public bool? IsConditionExpectedToLast1Year { get; set; }  // only necesary if above is false
        public bool IsLegallyBlind { get; set; }
        public bool IsLegallyDisabled { get; set; }
        public bool IsAbleGroceryShop { get; set; }
        public bool IsAbleDrive { get; set; }
        public bool IsAbleWork { get; set; }
        public int ExpectedIndependentYear { get; set; }

        public ConditionStatusModel () { }

        public ConditionStatusModel (bool isConditionPermanent, bool? IsConditionExpectedToLast1Year, bool isLegallyBlind, bool isAbleGroceryShop, bool isAbleDrive, bool IsAbleWork, int ExpectedIndependnetYear)
        {
            this.IsConditionPermanent = isConditionPermanent;
            this.IsConditionExpectedToLast1Year = IsConditionExpectedToLast1Year;
            this.IsLegallyBlind = isLegallyBlind;
            this.IsAbleGroceryShop = isAbleGroceryShop;
            this.IsAbleDrive = isAbleDrive;
            this.IsAbleWork = IsAbleWork;
            this.ExpectedIndependentYear = ExpectedIndependnetYear;
        }
    }
}

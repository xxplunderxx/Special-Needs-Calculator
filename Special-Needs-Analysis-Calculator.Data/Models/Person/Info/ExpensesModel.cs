using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Models.Person.Info
{
    public class ExpensesModel
    {
        public int Housing { get; set; }
        public int Food { get; set; }
        public int Utilities { get; set; }
        public int Transportation { get; set; }
        public int MedicalCoPay { get; set; }
        public int Entertainment { get; set; }
        public int ConditionCare { get; set; }
        public int Other { get; set; }

        public ExpensesModel()
        {

        }

        public ExpensesModel (int CostOfHousing, int CostOfFood, int CostOfUtilities, int CostOfTransportation, int CostOfMedicalCoPay, int CostOfEntertainment, int CostOfConditionCare, int CostOther)
        {
            this.Housing = CostOfHousing;
            this.Food = CostOfFood;
            this.Utilities = CostOfUtilities;
            this.Transportation = CostOfTransportation;
            this.MedicalCoPay = CostOfMedicalCoPay;
            this.Entertainment = CostOfEntertainment;
            this.ConditionCare = CostOfConditionCare;
            this.Other = CostOther;
        }
    }
}

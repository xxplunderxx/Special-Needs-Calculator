using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Domain
{
    public class FilingStatus
    {
        public enum Single
        {
            Row1Max = 40400,
            Row2Max = 445850,
        }

        public enum MarriedJointly
        {
            Row1Max = 80800,
            Row2Max = 501600,
        }

        public enum MarriedSeperately
        {
            Row1Max = 40400,
            Row2Max = 250800,
        }

        public enum HeadOfHousehold
        {
            Row1Max = 54100,
            Row2Max = 473751,
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Models.InputModels
{
    public class UpdateUserModel
    {
        public UserModel UserModel { get; set; }
        public string SessionToken { get; set; }
    }
}

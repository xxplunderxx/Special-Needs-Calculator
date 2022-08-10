using System.Globalization;
using System.Text.RegularExpressions;

namespace Special_Needs_Analysis_Calculator.Data.Models.InputModels
{
    public class CreateUserModel
    {
        public UserModel UserModel { get; set; }
        public string Password { get; set; }

        public static string CheckInput(CreateUserModel userModel)
        {
            // name validation
            var Fistname = userModel.UserModel.FirstName;
            var Lastname = userModel.UserModel.LastName;
            if (!IsValidName(Fistname) || !IsValidName(Lastname))               
            {
                return "Invalid name format";
            }

            // email validation
            var Email = userModel.UserModel.Email;
            if (!IsValidEmail(Email))
            {
                return "Invalid email format";
            }

            // phone validation
            var PrimaryPhone = userModel.UserModel.PrimaryPhoneNumber;
            var SecondaryPhone = userModel.UserModel.SecondaryPhoneNumber;
            if(!IsValidPhone(PrimaryPhone))
            {
                return "Invalid Primary Phone format";
            }
            else if (SecondaryPhone != null && !IsValidPhone(SecondaryPhone))
            {
                return "Invalid Secondary Phone format";
            }

            return "";  // input has been successfully validated
        }

        public static bool IsValidName(string Name)
        {
            if (Name == null || Name.Length == 0) return false;
            
            if(Name.Length < 25) return true;
            else return false;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Normalise the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                // Examines the domain part of the email and normalises it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();
                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidPhone(string PhoneNumber)
        {
            try
            {
                return Regex.IsMatch(PhoneNumber, @"^[1-9]{3}[-][1-9]{3}[-][1-9]{4}$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}

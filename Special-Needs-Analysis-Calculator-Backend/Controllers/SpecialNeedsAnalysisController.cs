using Microsoft.AspNetCore.Mvc;
using Special_Needs_Analysis_Calculator.Data.Database;
using Special_Needs_Analysis_Calculator.Data.Models;
using Special_Needs_Analysis_Calculator.Data.Models.InputModels;
using Special_Needs_Analysis_Calculator.Data.Models.Login;
using Special_Needs_Analysis_Calculator.Data.Models.People;
using Special_Needs_Analysis_Calculator.Domain;
using Special_Needs_Analysis_Calculator.Domain.SpecialNeedsCalculator;

namespace Special_Needs_Analysis_Calculator_Backend.Controllers
{
    // Facade structure 
    [ApiController]
    [Route("[controller]")]
    public class SpecialNeedsAnalysisController : Controller
    {
        private readonly IDatabaseCRUD context;


        public SpecialNeedsAnalysisController(IDatabaseCRUD context)
        {
            this.context = context;
        }

        [HttpGet]
        public string Index()
        {
            return "From Special Needs Analysis Controller Index.";
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserModel createUserModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var status = CreateUserModel.CheckInput(createUserModel);
            if (status != "") return BadRequest(status);

            bool success = await context.CreateUser(createUserModel);

            if(success) return Ok(createUserModel);
            else return BadRequest();
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserModel updateUserModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool success = await context.UpdateUser(updateUserModel);

            if (success) return Ok(updateUserModel);
            else return BadRequest();
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(SessionTokenModel session)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool success = await context.DeleteUser(session.SessionToken);

            if (success) return Ok();
            else return BadRequest();
        }

        [HttpPost("AddBeneficiary")]
        public async Task<IActionResult> AddBeneficiary(AddBeneficiaryModel addBeneficiaryModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var status = AddBeneficiaryModel.CheckInput(addBeneficiaryModel.BeneficiaryModel);
            if (status != "") return BadRequest(status);

            bool success = await context.AddBeneficiary(addBeneficiaryModel);
            if (success) return Ok(addBeneficiaryModel);
            else return BadRequest();
        }

        [HttpPost("UpdateBeneficiary")]
        public async Task<IActionResult> UpdateBeneficiary(UpdateBeneficiaryModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var status = AddBeneficiaryModel.CheckInput(model.BeneficiaryModel);
            if (status != "") return BadRequest(status);

            bool success = await context.UpdateBeneficiary(model);
            if (success) return Ok(model);
            else return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin loginRequest)
        { // Check if already logged in to not make another session token
            if (!ModelState.IsValid) return BadRequest();

            string? sessionId = await context.Login(loginRequest);

            if (sessionId == null) return Unauthorized();
            else return Ok(sessionId);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(SessionTokenModel session)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool result = await context.Logout(session.SessionToken);

            if (result) return Ok();
            else return Unauthorized();
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard(string sessionId)
        {
            if (!ModelState.IsValid) return BadRequest();
            return NotFound();
        }

        [HttpPost("CalculateBeneficiaries")]
        public async Task<IActionResult> CalculateBeneficiaries(SessionTokenModel session)
        {
            if (!ModelState.IsValid) return BadRequest();

            // Get user
            UserDocument userDocument = await context.FindUserBySessionToken(session.SessionToken);
            UserModel user = userDocument.User;

            // Get beneficiaries from database
            List<BeneficiaryModel>? beneficiaries = await context.FindBeneficiariesBySessionToken(session.SessionToken);
            if (beneficiaries == null) return NotFound();

            // Get calculations based on the beneficiaries
            List<BeneficiaryCalculation> beneficiaryCalculations = new List<BeneficiaryCalculation>();
            foreach (BeneficiaryModel beneficiary in beneficiaries)
            {
                SpecialNeedsCalculator calculator = new SpecialNeedsCalculator(user, beneficiary);
                beneficiaryCalculations.Add(calculator.TemplateResults());
            }

            return Json(beneficiaryCalculations);
        }
    }
}

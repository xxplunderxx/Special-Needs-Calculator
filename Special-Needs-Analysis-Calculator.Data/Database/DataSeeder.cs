using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Special_Needs_Analysis_Calculator.Data.Database;
using Special_Needs_Analysis_Calculator.Data.Models;
using Special_Needs_Analysis_Calculator.Data.Models.InputModels;
using Special_Needs_Analysis_Calculator.Data.Models.Login;
using Special_Needs_Analysis_Calculator.Data.Models.People;
using Special_Needs_Analysis_Calculator.Data.Models.Person.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Database
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<SpecialNeedsAnalysisDbContext>();
            context.Database.EnsureCreated();
            AddUsers(context);
            AddUserLogins(context);
            AddSessions(context);
        }

        private static void AddUsers(SpecialNeedsAnalysisDbContext context)
        {
            UserDocument? user = context.Users.FirstOrDefault();
            if (user != null) return;

            context.Add(new UserDocument(new UserModel
            {
                FirstName = "Iris",
                LastName = "Rowe",
                Email = "Iris@gmail.com",
                PrimaryPhoneNumber = "298-639-9285",
                SecondaryPhoneNumber = "298-798-7578",
                Beneficiaries =
                new List<BeneficiaryModel>
                {
                    new BeneficiaryModel
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Cherry",
                        LastName = "Rowe",
                        StateOfResidence = "Missouri",
                        IsStudent = true,
                        Age = 30,
                        ExpectedLifespan = 98,
                        YearlyIncome = 5000,
                        ConditionStatus = new ConditionStatusModel(true, true, true, true, false, false, 98),
                        Expenses = new ExpensesModel(900, 50, 150, 100, 30, 50, 50, 200),
                        SupplementalSecurityIncomeMonthly = 800,
                        SocialSecurityDisabilityInsuranceMonthly = 1300,
                        ABLEMaxHoldings = 400000,
                        AnnualABLEContributions = 8000,
                        ABLEFundRate = 0.10
                    }
                }
            }));

            context.Add(new UserDocument(new UserModel
            {
                FirstName = "Torren",
                LastName = "Bower",
                Email = "Torren@gmail.com",
                PrimaryPhoneNumber = "366-462-9431",
                SecondaryPhoneNumber = "366-823-9554",
            }));

            context.Add(new UserDocument(new UserModel
            {
                FirstName = "Tree",
                LastName = "Roots",
                Email = "Roots@gmail.com",
                PrimaryPhoneNumber = "465-823-9554"
            }));

            context.SaveChanges();
        }

        private static void AddUserLogins(SpecialNeedsAnalysisDbContext context)
        {
            UserLogin? userLogin = context.UserLogin.FirstOrDefault();
            if (userLogin != null) return;

            var salt1 = Guid.NewGuid().ToString();
            var salt2 = Guid.NewGuid().ToString();
            var salt3 = Guid.NewGuid().ToString();

            context.Add(new UserLogin 
            { 
                Email = "Iris@gmail.com", 
                Password = SHA256Hash.PasswordHash("iris", salt1), Salt = salt1 
            });

            context.Add(new UserLogin 
            { 
                Email = "Torren@gmail.com", 
                Password = SHA256Hash.PasswordHash("torren", salt2), Salt = salt2 
            });

            context.Add(new UserLogin 
            { 
                Email = "Roots@gmail.com", 
                Password = SHA256Hash.PasswordHash("roots", salt3), Salt = salt3 
            });

            context.SaveChanges();
        }

        private static void AddSessions(SpecialNeedsAnalysisDbContext context)
        {
            SessionTokenModel? session = context.Sessions.FirstOrDefault();
            if (session != null) return;

            SessionTokenModel newSession = new SessionTokenModel { Email = "Iris@gmail.com", SessionToken = "sessionToken" };
            context.Sessions.Add(newSession);
            context.SaveChanges();
        }
    }
}
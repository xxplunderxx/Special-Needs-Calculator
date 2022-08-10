using Microsoft.EntityFrameworkCore;
using Special_Needs_Analysis_Calculator.Data.Models;
using Special_Needs_Analysis_Calculator.Data.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_Needs_Analysis_Calculator.Data.Database
{
    public class SpecialNeedsAnalysisDbContext : DbContext
    {
        public SpecialNeedsAnalysisDbContext(DbContextOptions<SpecialNeedsAnalysisDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDocument>()
                .Property(u => u.User)
                .HasColumnType("jsonb");
        }

        public DbSet<UserDocument> Users { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<SessionTokenModel> Sessions { get; set; }
    }
}

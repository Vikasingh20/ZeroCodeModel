using System;
using System.Data.Entity;
using ZeroCodeFramework;

namespace Database
{
    public class ZeroCodeDBContext : DbContext
    {

        public string ConnectionString { get; set; }
      public  ZeroCodeDBContext(string connectionString, string from)  : base(connectionString)
        {
            if(from.Equals("env", StringComparison.OrdinalIgnoreCase))
            {
                ConnectionString = System.Environment.GetEnvironmentVariable(connectionString);
            } else
            {
                ConnectionString = connectionString;
            }
              //Open connection 
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        optionsBuilder.UseSqlServer(ConnectionString);

                


        //    }
        //}

        public DbSet<TestCase> TestCases { get; set; }

        public DbSet<TestStep> TestSteps { get; set; }    

        public DbSet<TestCaseExecutionMapping> TestCaseExecutionMappings { get; set; }

        public DbSet<TestStepData> TestStepDatas { get; set; }

        public DbSet<TestStaticDataStep> TestStaticDataSteps { get; set; }
        //
    }
}

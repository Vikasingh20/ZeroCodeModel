using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZeroCodeFramework
{

    [Table("TestStep")]
    public class TestStep
    {
        [Key]
        public int Id { get; set; }
        public decimal RunNumber { get; set; }
        public string TestCaseNumber { get; set; } = "0";
        public int TestCaseId { get; set; }

        [NotMapped]
        public int TestStepId { get; set; }

        [NotMapped]
        public int TestCaseDataId { get; set; }


        public int Seq { get; set; }

        public int Retry { get; set; }

        public int Wait { get; set; }

        public int OnFailStep { get; set; }

        public int OnFailCase { get; set; }
        public string Steps { get; set; }


        [NotMapped]
        public string ExecutedTime { get; set; }
        public bool IsRun { get; set; }

        [NotMapped]
        public bool IsPassed { get; set; }

        [NotMapped]
        public bool IsSkiped { get; set; }
        public string LocatorType { get; set; }
        public string LocatorTypeValue { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }

        [NotMapped]
        public string LocalValue { get; set; }
        public string Condition { get; set; }


        public string ActualValue { get; set; }


        [NotMapped]
        public string Exception { get; set; }

        [NotMapped]
        public string StackTrace { get; set; }


    }
}

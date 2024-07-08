using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZeroCodeFramework
{

    [Table("TestCase")]
    public class TestCase
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public int TestCaseId { get { return Id; } set { Id = value; } }

        [Column("TestCase")]
        public string Case { get; set; }

        public bool IsRun { get; set; }

        [NotMapped]
        public bool IsPassed { get; set; }


        [NotMapped]
        public string Exception { get; set; }

        [NotMapped]
        public string StackTrace { get; set; }
        [NotMapped]
        public List<TestStep> TestSteps { get; set; }

    }
}

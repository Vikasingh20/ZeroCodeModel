using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZeroCodeFramework
{

    [Table("TestCaseExecutionMapping")]
    public class TestCaseExecutionMapping
    {
        [Key]
        public int Id { get; set; }

         
        public int TestExecutionID { get; set; }

         
        public int TestCaseID { get; set; }


        public int Seq { get; set; }
         

        
        public bool IsRun { get; set; }


        [NotMapped]
        public string Exception { get; set; }

        [NotMapped]
        public string StackTrace { get; set; }
      

    }
}

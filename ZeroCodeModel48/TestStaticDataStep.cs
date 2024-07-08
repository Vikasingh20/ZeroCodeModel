using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZeroCodeFramework
{

    [Table("TestStaticDataStep")]
    public class TestStaticDataStep
    {
        [Key]
        public int Id { get; set; }

        public int TestExecutionID { get; set; }

        public int TestStepID { get; set; }

        public int TestCaseID { get; set;}

        public string DataValue { get; set; }

        public string DataKey { get; set; }

       

    }
}

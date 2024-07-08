using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZeroCodeFramework
{

    [Table("TestStepData")]
    public class TestStepData
    {
        [Key]
        public int Id { get; set; }

        public int TestExecutionID { get; set; }

        public int TestStepID { get; set; }

        public string UniqueUserId { get; set;}

        public string DataKey { get; set; }
        public string DataValue { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime ReadTime { get; set; }

        public byte[] Data { get; set; }

    }
}

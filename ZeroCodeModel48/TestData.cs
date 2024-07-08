using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroCodeFramework
{
    public class TestData
    {
        public Dictionary<string, string> ModuleData { get; set; }

        public Dictionary<string, string> TestCaseData { get; set; }


        public Dictionary<string, string> TestStepData { get; set; }

        public void InitiateModuleData()
        {
            ModuleData = new Dictionary<string, string>();
        }

        public void InitiateTestCaseData()
        {
            TestCaseData = new Dictionary<string, string>();
        }

        public void InitiateTestStepData()
        {
            TestStepData = new Dictionary<string, string>();
        }

        public void SetModuleData(string key, string value)
        {
            if (ModuleData.ContainsKey(key))
            {
                ModuleData[key] = value;
            }
            else
            {
                ModuleData.Add(key, value);
            }
        }

        public void SetTestCaseData(string key, string value)
        {
            if (TestCaseData.ContainsKey(key))
            {
                TestCaseData[key] = value;
            }
            else
            {
                TestCaseData.Add(key, value);
            }
        }

        public void SetTestStepData(string key, string value)
        {
            if (TestStepData.ContainsKey(key))
            {
                TestStepData[key] = value;
            }
            else
            {
                TestStepData.Add(key, value);
            }
        }

        public string GetModuleData(string key)
        {
            if (ModuleData.ContainsKey(key))
            {
                return ModuleData[key];
            }
            return "";
        }
        public string GetTestCaseData(string key)
        {
            if (TestCaseData.ContainsKey(key))
            {
                return TestCaseData[key];
            }
            return "";
        }

        public string GetTestStepData(string key)
        {
            if (TestStepData.ContainsKey(key))
            {
                return TestStepData[key];
            }
            return "";
        }
    }
}

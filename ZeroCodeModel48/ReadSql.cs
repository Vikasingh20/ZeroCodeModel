using Database;
using System;

namespace ZeroCodeFramework
{
    public class ReadSql
    {

        public static void ReadFromFileSingleRow(TestStep testStep, TestData testData)
        {
            var _dbContext = new ZeroCodeDBContext(testStep.LocatorTypeValue.Trim(), testStep.LocatorType.Trim());
            var query = System.IO.File.ReadAllText(testStep.Value);

            foreach (var item in testData.ModuleData)
            {
                var key = "{M-" + item.Key + "}";
                query = query.Replace(key, testData.GetModuleData(item.Key));
            }
            foreach (var item in testData.TestCaseData)
            {
                var key = "{C-" + item.Key + "}";
                query = query.Replace(key, testData.GetModuleData(item.Key));
            }
            foreach (var item in testData.TestStepData)
            {
                var key = "{S-" + item.Key + "}";
                query = query.Replace(key, testData.GetModuleData(item.Key));
            }

            var result = _dbContext.ObjectDictionaryByRawSql(query);
            if(result.Count > 0)
            {
                var dct = result[0];
                if ("module".Equals(testStep.ActualValue, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var item in dct)
                    {
                        testData.SetModuleData(item.Key, item.Value);
                    }
                }
                if ("case".Equals(testStep.ActualValue, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var item in dct)
                    {
                        testData.SetTestCaseData(item.Key, item.Value);
                    }
                }
                if ("step".Equals(testStep.ActualValue, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var item in dct)
                    {
                        testData.SetTestStepData(item.Key, item.Value);
                    }
                }
            }
            
        }
    }
}

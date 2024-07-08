using NLog;
using System.Collections.Generic;
using System.Reflection;
using ZeroCodeDynamic;

namespace ZeroCodeFramework
{
    public class ReadDynamicAssembly
    {
        public static void CallDynamicAssembly<T>(TestStep testStep, TestCase testCase, TestData testData, MyDriver myDriver, Logger _logger, List<T> list, ref int index)
        {

            var DLL = Assembly.LoadFile(testStep.LocatorTypeValue);

            IDynamicInvokeSelenum obj = DLL.CreateInstance(testStep.Value) as IDynamicInvokeSelenum;
            if (obj != null)
            {
                obj.InvokeSelenum(testStep, testCase, testData, myDriver.Driver, list);
            }



        }
    }
}

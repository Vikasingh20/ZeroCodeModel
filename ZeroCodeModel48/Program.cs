using Database;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace ZeroCodeFramework
{
    public class Constants
    {
        public static string ImageFilePath { get; set; }
    }
    public class Program
    {
        private static Logger _logger;
        private static bool isHold = false;
        private static bool isHoldBack = false;
        private static bool isExit = false;
        public static List<TestStep> TestSteps { get; set; } = new List<TestStep>();
        public static void ReadLine(MyDriver myDriver, TestData testData)
        {
            for (int i = 1; true; i++)
            {
                if (isExit)
                {
                    return;
                }
                Console.WriteLine($"loop exectuded {i} times");
                Console.WriteLine($"For Print Current Secreen Press P/p + Enter");
                Console.WriteLine($"For Run Test Step  Press R/r + Enter");
                Console.WriteLine($"For continue press Q/q + Enter");
                var key = Console.ReadLine();
                key = key.ToLower();
                if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("E", StringComparison.OrdinalIgnoreCase))
                {
                    isExit = true;
                    break;
                }
                else if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("r", StringComparison.OrdinalIgnoreCase))
                {
                    key = key.Trim();
                    var strArray = key.Split(' ');
                    strArray = strArray.Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();
                    if (strArray.Length == 2 && strArray[1].Trim().StartsWith("-s", StringComparison.OrdinalIgnoreCase))
                    {
                        var testStep = new TestStep();
                        _logger.Info(JsonConvert.SerializeObject(testStep));
                    }
                    else if (strArray.Length == 3 && strArray[1].Trim().StartsWith("-s", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            var testStep = JsonConvert.DeserializeObject<TestStep>(strArray[2]);
                            myDriver.Exceute(testStep, testData);
                        }
                        catch (Exception ex)
                        {
                            _logger.Info(ex.Message);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("p", StringComparison.OrdinalIgnoreCase))
                {
                    key = key.Trim();
                    var strArray = key.Split(' ');
                    strArray = strArray.Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();
                    if (strArray.Length == 1)
                    {
                        var testStep = new TestStep();
                        testStep.Action = "takingscreenshotappender";
                        testStep.Value = "_runtime{0}.png";
                        myDriver.Exceute(testStep, null);
                    }
                    else if (strArray.Length == 2)
                    {
                        var testStep = new TestStep();
                        testStep.Action = "takingscreenshotappender";
                        testStep.Value = strArray[1];
                        myDriver.Exceute(testStep, null);
                    }
                }
                else if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("cp", StringComparison.OrdinalIgnoreCase))
                {
                    key = key.Trim();
                    var strArray = key.Split(' ');
                    strArray = strArray.Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();
                    if (strArray.Length == 1)
                    {
                        var testStep = new TestStep();
                        testStep.Action = "takingscreenshot";
                        testStep.Value = "_runtime{0}.png";
                        myDriver.Exceute(testStep, null);
                    }
                    else if (strArray.Length == 2)
                    {
                        var testStep = new TestStep();
                        testStep.Action = "takingscreenshot";
                        testStep.Value = strArray[1];
                        myDriver.Exceute(testStep, testData);
                    }
                }

            }
        }


        public static void ReadLineHold(MyDriver myDriver, TestData testData)
        {
            ReadLine(myDriver, testData);

            //for (int i = 1; true; i++)
            //{
            //    Console.WriteLine($"loop exectuded {i} times for hold");
            //    Console.WriteLine($"For Print Current Secreen Press P/p + Enter");
            //    Console.WriteLine($"For continue press Q/q + Enter");
            //    var key = Console.ReadLine();
            //    if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("q", StringComparison.OrdinalIgnoreCase))
            //    {
            //        break;
            //    } else if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("r", StringComparison.OrdinalIgnoreCase))
            //    {
            //        key = key.Trim();
            //        var strArray = key.Split(' ');
            //        strArray = strArray.Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();
            //        if (strArray.Length == 2 && strArray[1].Trim().StartsWith("-s", StringComparison.OrdinalIgnoreCase))
            //        {
            //            var testStep = new TestStep();

            //            Console.WriteLine(JsonConvert.SerializeObject(testStep));
            //        }

            //    }
            //    else if (!string.IsNullOrEmpty(key) && key.Trim().StartsWith("p", StringComparison.OrdinalIgnoreCase))
            //    {
            //        key = key.Trim();
            //        var strArray = key.Split(' ');
            //        strArray = strArray.Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();
            //        if (strArray.Length == 1)
            //        {
            //            var testStep = new TestStep();
            //            testStep.Action = "takingscreenshotappender";
            //            testStep.Value = "_runtime{0}.png";
            //            myDriver.Exceute(testStep, null);
            //        }
            //        else if (strArray.Length == 2)
            //        {
            //            var testStep = new TestStep();
            //            testStep.Action = "takingscreenshotappender";
            //            testStep.Value = strArray[1];
            //            myDriver.Exceute(testStep, null);
            //        }
            //    }

            //}


        }


        static void Main(string[] args)
        {

            var propertyFileName = "./zerocode.property";
            Console.WriteLine("Welocme to Automate Factory.");
            _logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
           
            ReadPropertyFile readProperty = new ReadPropertyFile();
            readProperty.OpenProperty(propertyFileName);
            string licenceKey = "";
            

           var ISInterctive = readProperty.GetValue("ISInterctive");
            var ImageFilePaths = readProperty.GetValue("ImageFilePath");
           var itration = readProperty.GetValue("Itration");
            var connectionString = readProperty.GetValue("ConnectionString");
            int itrationCount = 5;
            if (!string.IsNullOrEmpty(itration))
            {
                int.TryParse(itration, out itrationCount);

            }
           

            Constants.ImageFilePath = @"c:\Images";
            if (!string.IsNullOrEmpty(ImageFilePaths))
            {
                Constants.ImageFilePath = ImageFilePaths;
            }

            if (!Directory.Exists(Constants.ImageFilePath))
            {
                Directory.CreateDirectory(Constants.ImageFilePath);
            }

            var newFolder = DateTime.Now.Ticks.ToString();
            Constants.ImageFilePath = Constants.ImageFilePath + @"\" + newFolder;
            if (!Directory.Exists(Constants.ImageFilePath))
            {
                Directory.CreateDirectory(Constants.ImageFilePath);
            }
            bool isIntractive = false;
            if (ISInterctive != null && ISInterctive.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                isIntractive = true;
            }


            TestData testData = new TestData();
            testData.InitiateModuleData();

            MyDriver myDriver = new MyDriver(licenceKey);
            myDriver.itrationCount = itrationCount;
            myDriver._logger = _logger;
            ZeroCodeDBContext zeroCodeDBContext = new ZeroCodeDBContext(connectionString, "con");

            string uniqueUserId = "1234567";
            if(args.Length> 0)
            {
                uniqueUserId= args[0];
            }
            int testExecutionID = 2;

            if (args.Length > 1)
            {
                var strtestExecutionID = args[1];

               // int.TryParse(strtestExecutionID,);//Usa
            }
            ExecuteDB(licenceKey, testData, isIntractive, readProperty, myDriver,zeroCodeDBContext,uniqueUserId, testExecutionID);
            Console.WriteLine("All Execution has completed.");
        }

      

        static void ExecuteDB(string licenceKey, TestData testData, bool isIntractive, ReadPropertyFile readProperty, MyDriver myDriver, ZeroCodeDBContext zeroCodeDBContext,string uniqueUserId,int testExecutionID)
        {
            var testCaseExecutionMappings = zeroCodeDBContext.TestCaseExecutionMappings.Where(x=>x.TestExecutionID == testExecutionID).OrderBy(x=>x.Seq).ToList();
            var allTestCase = new List<TestCase>();
            var lstTestCases = new List<TestCase>();
            int runNumber = 0;
            try
            {
                int currentTry = 0;
                for (int i = 0; i < testCaseExecutionMappings.Count; i++)
                {

                    var testCaseExecutionMapping = testCaseExecutionMappings[i];
                    if(!testCaseExecutionMapping.IsRun)
                    {
                        continue;
                    }
                    try
                    {
                        int c = testCaseExecutionMapping.TestCaseID;
                        var testCase = zeroCodeDBContext.TestCases.Where(x => x.Id == c).FirstOrDefault();
                        testData.InitiateTestCaseData();
                        if (!testCase.IsRun)
                        {
                            continue;
                        }
                        var testSteps = zeroCodeDBContext.TestSteps
                            .Where(x => x.TestCaseId == testCase.Id).AsNoTracking().OrderBy(x=>x.Seq)
                            .ToList().AsEnumerable().ToList();

                        testData.InitiateTestStepData();
                        testCase.TestSteps = testSteps;
                        for (int j = 0; j < testSteps.Count; j++)
                        {

                            var testStep = testSteps[j];

                            if (String.IsNullOrWhiteSpace(testStep.LocalValue))
                            {
                                testStep.LocalValue = testStep.Value.ToString();
                            }
                            else
                            {
                                testStep.Value = testStep.LocalValue.ToString();
                            }

                            if (!testStep.IsRun)
                            {
                                continue;
                            }

                            if (testStep.Value.StartsWith("!S->", StringComparison.OrdinalIgnoreCase))
                            {
                                
                                testStep.Value= GetStaticValueFromDB(testStep, zeroCodeDBContext, testExecutionID, uniqueUserId);
                            }

                            if (testStep.Value.StartsWith("!D->", StringComparison.OrdinalIgnoreCase))
                            {

                                testStep.Value = GetValueFromDB(testStep, zeroCodeDBContext, testExecutionID, uniqueUserId);
                            }
                            else if (testStep.Action.Equals("DynamicAssembly", StringComparison.OrdinalIgnoreCase) && testStep.IsRun)
                            {
                                ReadDynamicAssembly.CallDynamicAssembly(testStep, testCase, testData, myDriver, _logger, TestSteps, ref i);
                                continue;
                            }

                            if (testStep.Value.StartsWith("!C->", StringComparison.OrdinalIgnoreCase))
                            {
                                //var el = myDriver.GetWebElement(testStep);
                                byte[] bytes = myDriver.TakeScreenshots(testStep);
                                SetStepData(testStep, zeroCodeDBContext, testExecutionID, uniqueUserId,bytes);
                                testStep.RunNumber = ++runNumber;
                                continue;
                            }



                            try
                            {
                                testStep.RunNumber = ++runNumber;
                                if (testStep.IsRun)
                                {
                                    myDriver.Exceute(testStep, testData);
                                }
                                else
                                {
                                    testStep.ExecutedTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",
                              CultureInfo.InvariantCulture);
                                }
                                _logger.Info($" TestStepDataLastText - {testData.GetModuleData("TestStepDataLastText")}");
                                _logger.Info($" TestStepDataIsPassed - {testData.GetModuleData("TestStepDataIsPassed")}");
                                var json = JsonConvert.SerializeObject(testStep);
                                Console.WriteLine($" itemStep - {json}");
                                _logger.Info($" itemStep - {json}");
                            }
                            catch(Exception exp)
                            {
                                if (currentTry < 5)
                                {
                                    if (testStep.OnFailCase > 0)
                                    {
                                        i = testStep.OnFailCase;
                                         
                                    }

                                    if (testStep.OnFailStep > 0)
                                    {
                                        j = testStep.OnFailStep - 1;
                                         
                                    }
                                    //break;
                                }
                                 

                            }




                        }

                    }
                    catch  (Exception ex)
                    {

                    }
                }
                allTestCase.AddRange(lstTestCases);
            }
            catch 
            {

            }
            finally
            {
                try
                {
                    var json = JsonConvert.SerializeObject(allTestCase);
                    var tick = DateTime.Now.ToString("yyyymmddHHMMss");
                    var outputFile = readProperty.GetValue("outputJsonFilePath");
                    var replace = readProperty.GetValue("outputReplace");
                    outputFile = outputFile.Replace(replace, tick);
                    File.WriteAllText(outputFile, "var allOutPutData=" + json);
                }
                catch (Exception ex)
                {

                }

            }

            myDriver.Exit();
        }



        public static string GetStaticValueFromDB(TestStep testStep, ZeroCodeDBContext zeroCodeDBContext, int testExecutionID, string uniqueUserId)
        {


            var value = testStep.Value.Replace("!S->", "");
            var testStaticDataStep = zeroCodeDBContext.TestStaticDataSteps
                                  .Where(x =>
                                       x.TestCaseID == testStep.TestCaseId &&
                                       x.TestStepID == testStep.TestStepId &&
                                       x.TestExecutionID == testExecutionID &&
                                       x.DataKey.Equals(value, StringComparison.OrdinalIgnoreCase)
                                  ).FirstOrDefault();
            if (testStaticDataStep != null)
            {
                return testStaticDataStep.DataValue;// rd.GetValueFromDataSheet(itemStep, keyValuePairs, td - 1);
            }
            return "";

        }


        public static string GetValueFromDB(TestStep testStep,ZeroCodeDBContext zeroCodeDBContext,int testExecutionID, string uniqueUserId)
        {
            var key = testStep.Value.Replace("!D->", "");
            var testStepData = new TestStepData();
            testStepData.TestStepID = testStep.TestStepId;
            testStepData.UniqueUserId = uniqueUserId;
            testStepData.TestExecutionID = testExecutionID;
            testStepData.DataKey = key;
            testStepData.CreatedTime = DateTime.Now;

            zeroCodeDBContext.TestStepDatas.Add(testStepData);
            zeroCodeDBContext.SaveChanges();
             

            for (int i = 0; i <= testStep.Retry; i++)
            {
                zeroCodeDBContext.Entry<TestStepData>(testStepData).Reload();
           


                if(testStepData != null && !string.IsNullOrEmpty(testStepData.DataValue))
                {
                    testStepData.ReadTime=DateTime.Now;
                    zeroCodeDBContext.SaveChanges();
                    return testStepData.DataValue;
                }
                else
                {
                    Thread.Sleep(testStep.Wait * 1000);
                }
                
            }

            return "";
        }

        public static void SetStepData(TestStep testStep, ZeroCodeDBContext zeroCodeDBContext, int testExecutionID, string uniqueUserId, byte[] bytes)
        {
            var key = testStep.Value.Replace("!C->", "");
            var testStepData = new TestStepData();
            testStepData.TestStepID = testStep.TestStepId;
            testStepData.UniqueUserId = uniqueUserId;
            testStepData.TestExecutionID = testExecutionID;
            testStepData.DataKey = key;
            testStepData.CreatedTime = DateTime.Now;
            testStepData.Data = bytes;

            zeroCodeDBContext.TestStepDatas.Add(testStepData);
            zeroCodeDBContext.SaveChanges();
        }

    }


    public static class StringExt
    {
        public static int FindIndex(this IEnumerable<string> strList, string compare)
        {
            int i = 0;
            foreach (string str in strList)
            {
                if (str.Equals(compare, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
                i = i + 1;
            }

            return -1;
        }
    }
}

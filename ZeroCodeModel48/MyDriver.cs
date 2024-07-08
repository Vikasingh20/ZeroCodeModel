using Database;
using Newtonsoft.Json;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace ZeroCodeFramework
{
    public class MyDriver
    {

        public MyDriver(string licenceKey)
        {
            isExit = false;
            if (DateTime.Now > new DateTime(2025, 02, 15))
            {

                try
                {
                    licenceKey = licenceKey.Trim();
                    if (!LicenceCheck.Decrypt(licenceKey))
                    {
                        Console.WriteLine("Please Contact with Abhay Velankar(abhay.velankar@capita.com for licence.");
                        ReadPropertyFile message = new ReadPropertyFile();
                        message.OpenProperty("./zerocode.message.property");
                        Console.WriteLine(message.GetValue("licenceExpired"));
                        throw new Exception("Please Contact with Abhay Velankar(abhay.velankar@capita.com for licence.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Contact with Abhay Velankar(abhay.velankar@capita.com for licence.");
                    ReadPropertyFile message = new ReadPropertyFile();
                    message.OpenProperty("./zerocode.message.property");
                    Console.WriteLine(message.GetValue("licenceExpired"));


                }
            }
        }
        public Logger _logger;
        public WebDriver Driver;
        public int timeinsecond = 3;
        public int timeinsecondlong = 3;
        public bool isExit { get; set; }
        public int itrationCount = 3;
        public void Exit()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Dispose();
            }
        }
        public IWebElement GetWebElementFor5min(TestStep testStep)
        {
            IWebElement webElement = null;
            for (var i = 0; i < itrationCount || i < 5; i++)
            {
                if (isExit)
                {
                    return null;
                }
                try
                {
                    Thread.Sleep(2000);
                    webElement = getWebElement(testStep);
                }
                catch (Exception ex)
                {
                    _logger.Info($"Element not Found {testStep.LocatorTypeValue} {ex.ToString()}");
                    var json = JsonConvert.SerializeObject(testStep);
                    Console.WriteLine($" There is exception :");
                    Console.WriteLine($" itemStep - {json}");

                }
                if (webElement != null)
                {
                    break;
                }
            }

            if (webElement == null)
            {
                Console.WriteLine($"{testStep.LocatorType}  - {testStep.LocatorTypeValue}");
                // Console.WriteLine("Please Enter to continue");
                //  Console.ReadLine();
            }
            return webElement;
        }

        private IWebElement getWebElement(TestStep testStep)
        {
            if (isExit)
            {
                return null;
            }
            IWebElement webElement = null;
            if ("id".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecondlong));
                wait.Until(drv => drv.FindElement(By.Id(testStep.LocatorTypeValue)));
                webElement = Driver.FindElement(By.Id(testStep.LocatorTypeValue));
            }

            if ("xpath".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecondlong));
                wait.Until(drv => drv.FindElement(By.XPath(testStep.LocatorTypeValue)));
                webElement = Driver.FindElement(By.XPath(testStep.LocatorTypeValue));

            }

            if ("name".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecondlong));
                wait.Until(drv => drv.FindElement(By.Name(testStep.LocatorTypeValue)));
                webElement = Driver.FindElement(By.Name(testStep.LocatorTypeValue));

            }

            if ("ClassName".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecondlong));
                wait.Until(drv => drv.FindElement(By.ClassName(testStep.LocatorTypeValue)));
                webElement = Driver.FindElement(By.ClassName(testStep.LocatorTypeValue));


            }

            if ("CssSelector".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecondlong));
                wait.Until(drv => drv.FindElement(By.CssSelector(testStep.LocatorTypeValue)));
                webElement = Driver.FindElement(By.CssSelector(testStep.LocatorTypeValue));

            }
            if ("LinkText".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecondlong));
                wait.Until(drv => drv.FindElement(By.LinkText(testStep.LocatorTypeValue)));

                webElement = Driver.FindElement(By.CssSelector(testStep.LocatorTypeValue));

            }
            if ("PartialLinkText".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecondlong));
                wait.Until(drv => drv.FindElement(By.PartialLinkText(testStep.LocatorTypeValue)));
                webElement = Driver.FindElement(By.CssSelector(testStep.LocatorTypeValue));
            }



            return webElement;
        }


        public IWebElement GetWebElement(TestStep testStep)
        {
            if (isExit)
            {
                return null;
            }


            IWebElement webElement = null;
            try
            {
                if ("id".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecond));
                    wait.Until(drv => drv.FindElement(By.Id(testStep.LocatorTypeValue)));
                    webElement = Driver.FindElement(By.Id(testStep.LocatorTypeValue));
                }

                if ("xpath".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecond));
                    wait.Until(drv => drv.FindElement(By.XPath(testStep.LocatorTypeValue)));
                    webElement = Driver.FindElement(By.XPath(testStep.LocatorTypeValue));

                }

                if ("name".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecond));
                    wait.Until(drv => drv.FindElement(By.Name(testStep.LocatorTypeValue)));
                    webElement = Driver.FindElement(By.Name(testStep.LocatorTypeValue));

                }

                if ("ClassName".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecond));
                    wait.Until(drv => drv.FindElement(By.ClassName(testStep.LocatorTypeValue)));
                    webElement = Driver.FindElement(By.ClassName(testStep.LocatorTypeValue));


                }

                if ("CssSelector".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecond));
                    wait.Until(drv => drv.FindElement(By.CssSelector(testStep.LocatorTypeValue)));
                    webElement = Driver.FindElement(By.CssSelector(testStep.LocatorTypeValue));

                }
                if ("LinkText".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecond));
                    wait.Until(drv => drv.FindElement(By.LinkText(testStep.LocatorTypeValue)));

                    webElement = Driver.FindElement(By.CssSelector(testStep.LocatorTypeValue));

                }
                if ("PartialLinkText".Equals(testStep.LocatorType, StringComparison.OrdinalIgnoreCase))
                {
                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeinsecond));
                    wait.Until(drv => drv.FindElement(By.PartialLinkText(testStep.LocatorTypeValue)));
                    webElement = Driver.FindElement(By.CssSelector(testStep.LocatorTypeValue));
                }
            }
            catch (Exception ex)
            {
                _logger.Info($"Element not Found {testStep.LocatorTypeValue} {ex.ToString()}");
                var json = JsonConvert.SerializeObject(testStep);
                Console.WriteLine($" There is Exception: ");
                Console.WriteLine($" itemStep - {json}");
            }
            if (webElement is null)
            {
                webElement = GetWebElementFor5min(testStep);
            }
            if (webElement is null)
            {
                throw new Exception($"No web Element found for test step ${testStep.Steps}");
            }

            return webElement;
        }

        public string HasClass(IWebElement element, string className)
        {
            string classes = element.GetAttribute("class");
            var classesArray = classes.Split(' ');
            foreach (var item in classesArray)
            {
                if (item.Equals(className, StringComparison.OrdinalIgnoreCase))
                {
                    return "y";
                }
            }
            //for (string c : classes.split(" "))
            //{
            //    if (c.equals(theClassYouAreSearching))
            //    {
            //        return true;
            //    }
            //}

            return "N";
        }

        public string GetDataFromTestData(TestStep testStep, TestData testData)
        {
            if ("ModuleText".EndsWith(testStep.Action, StringComparison.OrdinalIgnoreCase))
            {
                return testData.GetModuleData(testStep.Value);
            }
            if ("TestCaseText".EndsWith(testStep.Action, StringComparison.OrdinalIgnoreCase))
            {
                return testData.GetTestCaseData(testStep.Value);
            }
            if ("TestStepText".EndsWith(testStep.Action, StringComparison.OrdinalIgnoreCase))
            {
                return testData.GetTestStepData(testStep.Value);
            }
            throw new Exception($"{testStep.Action} not matched with any get Data");
        }

        public string GetValue(TestData testData, string type, string key)
        {
            if (type.Equals("d", StringComparison.OrdinalIgnoreCase))
            {
                return key;
            }
            if (type.Equals("m", StringComparison.OrdinalIgnoreCase))
            {
                return testData.GetModuleData(key);
            }
            if (type.Equals("c", StringComparison.OrdinalIgnoreCase))
            {
                return testData.GetTestCaseData(key);
            }
            if (type.Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                return testData.GetTestStepData(key);
            }

            return "";
        }
        public void GetLocatorValue(TestStep testStep, TestData testData, string type)
        {
            if (type.Contains(":"))
            {
                var typeArray = type.Split(':');
                if (typeArray[0].Trim().Equals("m", StringComparison.OrdinalIgnoreCase))
                {
                    var value = GetValue(testData, typeArray[0].Trim(), typeArray[1].Trim());
                    testStep.LocatorTypeValue = testStep.LocatorTypeValue.Replace("|{" + typeArray[1].Trim() + "}|", value.Trim());
                }
                else if (typeArray[0].Trim().Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    var value = GetValue(testData, typeArray[0].Trim(), typeArray[1].Trim());
                    testStep.LocatorTypeValue = testStep.LocatorTypeValue.Replace("|{" + typeArray[1].Trim() + "}|", value.Trim());
                }
                else if (typeArray[0].Trim().Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    var value = GetValue(testData, typeArray[0].Trim(), typeArray[1].Trim());
                    testStep.LocatorTypeValue = testStep.LocatorTypeValue.Replace("|{" + typeArray[1].Trim() + "}|", value.Trim());
                }
                else if (typeArray[0].Trim().Equals("d", StringComparison.OrdinalIgnoreCase))
                {
                    var value = GetValue(testData, typeArray[0].Trim(), typeArray[1].Trim());
                    testStep.LocatorTypeValue = testStep.LocatorTypeValue.Replace("|{" + typeArray[1].Trim() + "}|", testStep.ActualValue.Trim());
                }

            }


        }
        public void SetLocatorType(TestStep testStep, TestData testData)
        {
            var locatorType = testStep.LocatorType;
            var locatorTypeArray = locatorType.Split('-');
            testStep.LocatorType = locatorTypeArray[0].ToString();
            for (int i = 1; i < locatorTypeArray.Length; i++)
            {
                GetLocatorValue(testStep, testData, locatorTypeArray[i]);
            }

        }

        public void SetValue(TestStep testStep, TestData testData, string value)
        {
            if (testStep.ActualValue.Contains("-"))
            {
                var valArray = testStep.ActualValue.Split('-');
                var type = valArray[0];
                var key = valArray[0];
                if (type.Equals("m", StringComparison.OrdinalIgnoreCase))
                {
                    testData.SetModuleData(key, value);
                }
                if (type.Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    testData.SetTestCaseData(key, value);
                }
                if (type.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    testData.SetTestStepData(key, value);
                }
            }
            else
            {
                testData.SetModuleData(testStep.ActualValue, value);
            }
        }


        public void ReplaceValue(TestStep testStep, TestData testData)
        {
            if (string.IsNullOrWhiteSpace(testStep.ActualValue))
            {
                var arr = testStep.ActualValue.Split(',');
                foreach (var item in arr)
                {
                    testStep.Value = testStep.Value.Replace($"m:{{item}}", testData.GetModuleData(item));
                    testStep.Value = testStep.Value.Replace($"c:{{item}}", testData.GetTestCaseData(item));
                }
            }

            testStep.Value = testStep.Value.Substring(4);
        }


        public void Exceute(TestStep testStep, TestData testData)
        {
            if (isExit)
            {
                return;
            }
            testStep.ExecutedTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(testStep.LocatorType) && testStep.LocatorType.Contains("-"))
            {
                SetLocatorType(testStep, testData);
            }

            if (!string.IsNullOrWhiteSpace(testStep.Condition))
            {
                var valArray = testStep.Condition.Split('-');
                if (valArray.Length == 4)
                {
                    if (string.IsNullOrEmpty(valArray[3]))
                    {
                        valArray[3] = string.Empty;
                    }
                    else if (valArray[3].StartsWith("$"))
                    {
                        IWebElement webElement = GetWebElement(testStep);
                        valArray[3] = webElement.Text;
                    }
                    var value = GetValue(testData, valArray[0], valArray[1]);
                    if (valArray[2] == "!")
                    {
                        if (value.Equals(valArray[3], StringComparison.OrdinalIgnoreCase))
                        {
                            testStep.IsPassed = true;
                            testStep.IsSkiped = true;
                            return;
                        }
                    }
                    else if (valArray[2] == "=")
                    {
                        if (!value.Equals(valArray[3], StringComparison.OrdinalIgnoreCase))
                        {
                            testStep.IsPassed = true;
                            testStep.IsSkiped = true;
                            return;
                        }
                    }

                }

            }

            if (!string.IsNullOrWhiteSpace(testStep.Value) && testStep.Value.StartsWith("!M->"))
            {
                ReplaceValue(testStep, testData);
            }

            Console.WriteLine($"Test Case Id : {testStep.TestCaseId} --  Test Steps--: {testStep.Steps} --");
            testStep.IsPassed = true;
            if (testData != null)
            {
                testData.SetModuleData("TestStepDataIsPassed", "true");
            }

            try
            {
                if ("openbrowser".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    if (Driver != null)
                    {
                        try
                        {
                            Exit();
                        }
                        catch
                        {

                        }

                    }
                    //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

                    //ChromeOptions chromeOptions = new ChromeOptions();
                    //chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                    //chromeOptions.AddExcludedArgument("enable-automation");
                    //chromeOptions.AddArguments("disable-extensions");
                    //chromeOptions.AddArguments("ignore-certificate-errors");
                    //chromeOptions.AddArgument("--silent");
                    //chromeOptions.AddArgument("--headless");

                    //Driver = new ChromeDriver(chromeOptions);
                    Driver = new ChromeDriver();
                }
                if ("openbrowserChrome".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    if (Driver != null)
                    {
                        try
                        {
                            Exit();
                        }
                        catch
                        {

                        }

                    }
                    ChromeOptions chromeOptions = new ChromeOptions();
                    
                    chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                    chromeOptions.AddExcludedArgument("enable-automation");
                    chromeOptions.AddArguments("disable-extensions");
                    chromeOptions.AddArguments("ignore-certificate-errors");
                    chromeOptions.AddArgument("--silent");

                    //driver.execute_script("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})")
                    Driver = new ChromeDriver(chromeOptions);
                }
                else if ("openbrowserIE".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    if (Driver != null)
                    {
                        try
                        {
                            Exit();
                        }
                        catch
                        {

                        }

                    }
                    ChromeOptions chromeOptions = new ChromeOptions();
                    Driver = new EdgeDriver();

                }
                else if ("openbrowserFF".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    if (Driver != null)
                    {
                        try
                        {
                            Exit();
                        }
                        catch
                        {

                        }

                    }
                    ChromeOptions chromeOptions = new ChromeOptions();
                    Driver = new FirefoxDriver();
                }
                else if ("enterurl".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    Driver.Navigate().GoToUrl(testStep.Value);
                    testData.SetModuleData("basewindow", Driver.CurrentWindowHandle);
                }
                else if ("sendkeys".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    webElement.Clear();
                    webElement.SendKeys(testStep.Value);
                    testData.SetModuleData("TestStepDataLastText", webElement.GetAttribute("value").ToString());

                }
                else if ("sendkeysEncrypt".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    webElement.Clear();
                    webElement.SendKeys(testStep.Value);
                    testStep.Value = "xxx";
                    // testData.SetModuleData("TestStepDataLastText", webElement.GetAttribute("value").ToString());
                }
                else if ("MoveToElement".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    Actions action = new Actions(Driver);
                    action.MoveToElement(webElement).Perform();
                }
                else if ("MoveToElementBuild".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    Actions action = new Actions(Driver);
                    action.MoveToElement(webElement).Build().Perform();
                }
                else if ("MoveToElement".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    Actions action = new Actions(Driver);
                    action.MoveToElement(webElement).ClickAndHold().Perform();
                }
                else if ("GetText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    testData.SetModuleData("TestStepDataLastText", webElement.Text);
                    if (!string.IsNullOrEmpty(testStep.ActualValue))
                    {
                        testData.SetTestCaseData(testStep.ActualValue, webElement.Text);
                        testData.SetTestStepData(testStep.ActualValue, webElement.Text);
                    }
                }
                else if ("click".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    webElement.Click();

                }
                else if ("hasclass".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    string hasClass = HasClass(webElement, testStep.Value);
                    if (!string.IsNullOrEmpty(testStep.ActualValue))
                    {
                        SetValue(testStep, testData, hasClass);
                    }
                    testData.SetModuleData("TestStepDataLastText", hasClass);
                }
                else if ("selectdropdown".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    webElement.Click();
                }
                else if ("selectdropdownByValue".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    var selectElement = new SelectElement(webElement);
                    selectElement.SelectByValue(testStep.Value);
                    testData.SetModuleData("TestStepDataLastText", webElement.Text);
                }
                else if ("selectdropdownByText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    var selectElement = new SelectElement(webElement);
                    selectElement.SelectByText(testStep.Value);
                    testData.SetModuleData("TestStepDataLastText", webElement.Text);
                }
                else if ("selectdropdownByIndex".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    var selectElement = new SelectElement(webElement);
                    int index = -1;
                    bool isInt = int.TryParse(testStep.Value, out index);
                    if (isInt)
                    {
                        selectElement.SelectByIndex(index);
                        testData.SetModuleData("TestStepDataLastText", webElement.Text);
                    }

                }
                else if ("sleep".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Console.WriteLine("I am in sleep");
                        Thread.Sleep(Convert.ToInt32(testStep.Value));
                    }
                    catch
                    {

                        Thread.Sleep(1000);
                    }

                }
                else if ("validateText".StartsWith(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    if (text != stepValue)
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        return;
                    }

                }
                else if ("validateTextandThrow".StartsWith(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    if (text == stepValue)
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        throw new Exception("rethrow");
                    }

                }
                else if ("validateContainsText".StartsWith(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    if (!text.Contains(stepValue))
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        return;
                    }
                }
                else if ("validateStartsWithText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    if (!text.StartsWith(stepValue))
                    {
                        testStep.IsPassed = false;
                        testStep.ActualValue = text;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        return;
                    }
                }
                else if ("validateEndsWithText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    if (!text.EndsWith(stepValue))
                    {
                        testStep.IsPassed = false;
                        testStep.ActualValue = text;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        return;
                    }
                }
                else if ("validateTitle".StartsWith(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    var text = Driver.Title;
                    if (text != stepValue)
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        return;
                    }
                }
                else if ("SetModuleText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    testData.SetModuleData(testStep.ActualValue, text);
                }
                else if ("SetTestCaseText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    testData.SetTestCaseData(testStep.ActualValue, text);
                }
                else if ("SetTestStepText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    var text = webElement.Text;
                    testData.SetTestStepData(testStep.ActualValue, text);
                }
                else if ("SetModuleTextValue".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    testData.SetModuleData(testStep.ActualValue, testStep.Value);
                }
                else if ("SetTestCaseTextValue".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    testData.SetTestCaseData(testStep.ActualValue, testStep.Value);
                }
                else if ("SetTestStepTextValue".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    testData.SetTestStepData(testStep.ActualValue, testStep.Value);
                }
                else if ("closeBrowser".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    Driver.Close();
                }

                else if ("quitBrowser".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    Driver.Close();
                }
                else if ("maximize".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    Driver.Manage().Window.Maximize();
                }
                else if ("takingscreenshot".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    ITakesScreenshot iscreenshot = Driver as ITakesScreenshot;
                    Screenshot screenshot = iscreenshot.GetScreenshot();
                    testStep.Value = Constants.ImageFilePath + @"\" + testStep.Value;
                    Console.WriteLine($"Taking screenshot with following path {testStep.Value}");
                    SaveScreenShot(screenshot, testStep.Value);

                }
                else if ("takingscreenshotappender".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        //Console.WriteLine($"Following is path ${Constants.ImageFilePath}");
                        //var value = testStep.Value;
                        //testStep.Value = value.Replace("{0}", DateTime.Now.Ticks.ToString());
                        //var scrrenfullValue = value.Replace("{0}", "fullscrren" + DateTime.Now.Ticks.ToString());
                        //scrrenfullValue = Constants.ImageFilePath + @"\" + scrrenfullValue;
                        //testStep.Value = Constants.ImageFilePath + @"\" + testStep.Value;
                        //Console.WriteLine($"Taking screenshot with following path {scrrenfullValue}");
                        //var vcs = new VerticalCombineDecorator(new ScreenshotMaker());
                        //var ImageByte = Driver.TakeScreenshot(vcs);
                        //System.IO.File.WriteAllBytes(scrrenfullValue, ImageByte);

                    }
                    catch (Exception ex)
                    {
                        _logger.Info(ex.Message.ToString());
                        _logger.Info(ex.StackTrace.ToString());
                        var json = JsonConvert.SerializeObject(testStep);
                        Console.WriteLine($" There is Exception");
                        Console.WriteLine($" itemStep - {json}");
                    }
                }
                else if ("fullscreenshotappender".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    //ITakesScreenshot iscreenshot = Driver as ITakesScreenshot;
                    //Screenshot screenshot = iscreenshot.GetScreenshot();
                    //var vcs = new VerticalCombineDecorator(new ScreenshotMaker());
                    //var ImageByte = Driver.TakeScreenshot(vcs);
                    //var value = testStep.Value;
                    //testStep.Value = value.Replace("{0}", DateTime.Now.Ticks.ToString());
                    //var scrrenfullValue = value.Replace("{0}", "fullscrren" + DateTime.Now.Ticks.ToString());
                    //scrrenfullValue = Constants.ImageFilePath + @"\" + scrrenfullValue;
                    //Console.WriteLine($"Taking screenshot with following path {scrrenfullValue}");
                    //System.IO.File.WriteAllBytes(scrrenfullValue, ImageByte);
                }
                else if ("fullscreen".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    Driver.Manage().Window.FullScreen();
                }
                else if ("javascriptexecutor".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)Driver;
                    javaScriptExecutor.ExecuteScript(testStep.Value);
                }
                else if ("removealert".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)Driver;
                    javaScriptExecutor.ExecuteScript("alert=(text)=>{}");
                    javaScriptExecutor.ExecuteScript("confirm=(text)=>{ return true;}");
                }
                else if ("isenabled".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    if (!webElement.Enabled)
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        testStep.ActualValue = "Not Enabled";
                    }
                }
                else if ("isdisabled".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    if (webElement.Enabled)
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        testStep.ActualValue = "Not Disabled";
                    }
                }
                else if ("isselected".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    if (!webElement.Selected)
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        testStep.ActualValue = "Not Selected";
                    }
                }
                else if ("isnotselected".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    if (webElement.Enabled)
                    {
                        testStep.IsPassed = false;
                        testData.SetModuleData("TestStepDataIsPassed", "false");
                        testStep.ActualValue = "Not Disabled";
                    }
                }
                else if ("alertdismiss".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Driver.SwitchTo().Alert().Dismiss();
                    }
                    catch
                    {


                    }
                }
                else if ("alertaccept".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var alert = Driver.SwitchTo().Alert();
                        for (int i = 0; i < 3; i++)
                        {
                            if (alert != null)
                            {
                                alert.Accept();
                            }
                            else
                            {
                                Thread.Sleep(500);
                            }
                        }
                    }
                    catch
                    {

                    }


                }
                else if ("confirmccept".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Driver.SwitchTo().Alert().Accept();
                    }
                    catch
                    {


                    }
                }
                else if ("alertsendKeys".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Driver.SwitchTo().Alert().SendKeys(testStep.Value);
                    }
                    catch
                    {
                    }
                }
                else if ("validatealertText".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    var text = Driver.SwitchTo().Alert().Text;
                    if (text != stepValue)
                    {
                        testStep.IsPassed = false;
                        return;
                    }
                }
                else if ("Print".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    var stepValue = testStep.Value;
                    if (testStep.Action.Contains("-"))
                    {
                        stepValue = GetDataFromTestData(testStep, testData);
                    }
                    Console.WriteLine($"Print value => {stepValue}");
                    var json = JsonConvert.SerializeObject(testStep);
                    Console.WriteLine($" itemStep - {json}");
                }
                else if ("alertSetAuthenticationCredentials".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    //object value = Driver.SwitchTo().Alert().SetAuthenticationCredentials(testStep.Value, testStep.ActualValue);
                }
                else if ("SetBaseWindowHandle".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    // String parent = driver.getWindowHandle();
                    string parent = Driver.CurrentWindowHandle;
                    testData.SetModuleData("basewindow", parent);
                }
                else if ("SwitchToBaseWindow".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    // String parent = driver.getWindowHandle();

                    var basewindow = testData.GetModuleData("basewindow");
                    if (!string.IsNullOrEmpty(basewindow))
                    {
                        Driver.SwitchTo().Window(basewindow);
                    }

                }
                else if ("SetPopUpWindow".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    // String parent = driver.getWindowHandle();
                    string parent = Driver.CurrentWindowHandle;
                    var lst = Driver.WindowHandles;
                    var basewindow = testData.GetModuleData("basewindow");

                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (!lst[i].Equals(basewindow))
                        {
                            Driver.SwitchTo().Window(lst[i]);
                            testData.SetModuleData("Popupwindow", lst[i]);
                        }
                    }
                }
                else if ("SwitchToPopUpWindow".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    // String parent = driver.getWindowHandle();
                    var Popupwindow = testData.GetModuleData("Popupwindow");
                    if (string.IsNullOrEmpty(Popupwindow))
                    {
                        Driver.SwitchTo().Window(Popupwindow);
                    }
                }
                else if ("SetPopUpParentWindow".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    // String parent = driver.getWindowHandle();
                    string parent = Driver.CurrentWindowHandle;
                    var lst = Driver.WindowHandles;
                    var basewindow = testData.GetModuleData("basewindow");
                    var Popupwindow = testData.GetModuleData("Popupwindow");
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (!lst[i].Equals(basewindow) && !lst[i].Equals(Popupwindow))
                        {
                            testData.SetModuleData("PopupParentwindow", lst[i]);
                        }
                    }
                }
                else if ("SwitchToPopUpParentWindow".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    // String parent = driver.getWindowHandle();
                    var PopupParentwindow = testData.GetModuleData("PopupParentwindow");
                    if (string.IsNullOrEmpty(PopupParentwindow))
                    {
                        Driver.SwitchTo().Window(PopupParentwindow);
                    }
                }
                else if ("switchToFrame".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement webElement = GetWebElement(testStep);
                    Driver.SwitchTo().Frame(webElement);
                    //Console.WriteLine(currentFrame);


                }
                else if ("switchToFrameByIndex".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(testStep.Value))
                    {
                        var val = 0;
                        var isParse = int.TryParse(testStep.Value, out val);
                        if (isParse)
                        {
                            Driver.SwitchTo().Frame(val);
                        }
                    }

                }
                else if ("switchToParentFrame".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    Driver.SwitchTo().ParentFrame();
                }
                else if ("switchToLastWindow".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {
                    string parent = Driver.CurrentWindowHandle;
                    var lst = Driver.WindowHandles;
                    var last = lst.Last();
                    Driver.SwitchTo().Window(last);
                }
                else if ("switchToLastWindowAndClose".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {

                    var basewindow = testData.GetModuleData("basewindow");
                    var lst = Driver.WindowHandles;
                    var last = lst.Last();
                    if (!last.Equals(basewindow) && lst.Count > 1)
                    {
                        var dri = Driver.SwitchTo().Window(last);
                        dri.Close();
                        Driver.SwitchTo().Window(basewindow);
                    }

                }
                else if ("closeAllPopUpWindow".Equals(testStep.Action, StringComparison.OrdinalIgnoreCase))
                {

                    var basewindow = testData.GetModuleData("basewindow");
                    var lst = Driver.WindowHandles;
                    foreach (var last in lst)
                    {

                        if (!last.Equals(basewindow) && Driver.WindowHandles.Count > 1)
                        {
                            var dri = Driver.SwitchTo().Window(last);
                            dri.Close();
                            Driver.SwitchTo().Window(basewindow);
                        }
                    }


                }
                else if (string.IsNullOrEmpty(testStep.Action))
                {

                }
                else if (testStep.Action.StartsWith("ExecuteDataBaseQuery", StringComparison.OrdinalIgnoreCase))
                {

                    ZeroCodeDBContext zcdb = new ZeroCodeDBContext(testStep.LocatorType, "");
                    if (testStep.Action.Contains("-"))
                    {
                        var ActionStringArray = testStep.Action.Split('-').Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToArray();
                        if (ActionStringArray[1].Equals("ObjectDictionaryByRawSql", StringComparison.OrdinalIgnoreCase))
                        {
                            var list = zcdb.ObjectDictionaryByRawSql(testStep.Value);

                            for (int i = 0; i < list.Count; i++)
                            {
                                var dict = list[i];
                                foreach (var item in dict)
                                {
                                    var key = $"{testStep.LocatorTypeValue}*{i}*{item.Key}";
                                    if (ActionStringArray[2].Equals("m", StringComparison.OrdinalIgnoreCase))
                                    {
                                        testData.SetModuleData(key, item.Value);
                                    }
                                }

                            }
                        }
                        else if (ActionStringArray[1].Equals("CountByRawSql", StringComparison.OrdinalIgnoreCase))
                        {
                            var count = zcdb.CountByRawSql(testStep.Value);
                            var key = $"{testStep.LocatorTypeValue}*0";

                        }
                        else if (ActionStringArray[1].Equals("CountByRawSql", StringComparison.OrdinalIgnoreCase))
                        {
                            var count = zcdb.CountByRawSql(testStep.Value);


                        }


                    }



                }

            }
            catch (Exception ex)
            {
                testStep.IsPassed = false;
                testData.SetModuleData("TestStepDataLastText", ex.Message);
                testData.SetModuleData("TestStepDataIsPassed", "false");
                testStep.Exception = ex.Message;
                testStep.StackTrace = ex.StackTrace;

                if (ex.Message.Equals("rethrow"))
                {
                    throw ex;
                }

            }
            Console.WriteLine($"Test Case Id : {testStep.TestCaseId} --  Test Steps--: {testStep.Steps} -- Result --{testStep.IsPassed}");

        }

        public void SaveScreenShot(Screenshot screenshot, string fileName)
        {
            if (fileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(fileName);
            }
            else if (fileName.EndsWith("Jpeg", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(fileName);
            }
            else if (fileName.EndsWith("Gif", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(fileName);
            }
            else if (fileName.EndsWith("Bmp", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(fileName);
            }
            else if (fileName.EndsWith("Tiff", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(fileName);
            }
        }

        public byte[] TakeScreenshots(TestStep testStep)
        {
            //try
            //{
            byte[] imageBytes = null;
            try
            {
                var element = GetWebElement(testStep);

                Byte[] byteArray = ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray;
                System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(new System.IO.MemoryStream(byteArray));
                System.Drawing.Rectangle croppedImage = new System.Drawing.Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
                //System.Drawing.Rectangle croppedImage = new System.Drawing.Rectangle(266, 258, element.Size.Width, element.Size.Height);
                screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
                //byte[] data = (byte[])(new ImageConverter()).ConvertTo(screenshot, typeof(byte[]));
                //utils.UpdateImage(mpan, mintgid, data);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Save the Bitmap to the MemoryStream in the desired format
                    screenshot.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageBytes = memoryStream.ToArray();
                }
                screenshot.Dispose();
            }catch(Exception e)
            {

            }
            return imageBytes;
        }
    }
}

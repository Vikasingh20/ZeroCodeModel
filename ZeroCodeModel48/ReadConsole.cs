using Newtonsoft.Json;
using NLog;
using System;

namespace ZeroCodeFramework
{
    public class ReadConsole
    {
        public static void ReadFromConsole(TestStep testStep,TestCase testCase, TestData testData,MyDriver myDriver, Logger _logger,ref int index)
        {

            for (int i = 0; i >= 0; i++)
            {
                Console.WriteLine("");
                Console.WriteLine("Read Json Text - Press :J");
                Console.WriteLine("Quit - Press :Q");
                 var key = Console.ReadKey();
                if (key.KeyChar.ToString().Equals("j", StringComparison.OrdinalIgnoreCase) || key.KeyChar.Equals("j"))
                {
                    try
                    {
                        Console.WriteLine("");
                        var text = Console.ReadLine();
                        var obj = JsonConvert.DeserializeObject<TestStep>(text);
                        testCase.TestSteps.Insert(i, obj);
                        i = i + 1;
                        myDriver.Exceute(obj, testData);
                    }
                    catch (Exception ex)
                    {

                    }
                } else if (key.KeyChar.ToString().Equals("Q", StringComparison.OrdinalIgnoreCase) || key.KeyChar.Equals("q"))
                {
                    try
                    {
                        break;
                    }
                    catch (Exception ex)
                    {

                    }
                }



            }
           
          




        }
    }
}

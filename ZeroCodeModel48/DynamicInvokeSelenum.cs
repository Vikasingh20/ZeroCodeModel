using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using ZeroCodeDynamic;
using ZeroCodeFramework;

namespace DynamicClassLibrary
{
    public class DynamicInvokeSelenum : IDynamicInvokeSelenum
    {
        WebDriver driver;
        Hashtable htParam2;
        DataSet ds2;
       // DataAccessClass dataAccessRecruit = new DataAccessClass();

        public DynamicInvokeSelenum()
        {
           // this.dataAccessRecruit = dataAccessRecruit;
        }

        public void InvokeSelenum<T>(TestStep testStep, TestCase testCase, TestData testData, WebDriver webDriver, List<T> list)
        {
            driver = webDriver;
            try
            {
                bool val = true;
                int i = 2;
                string captcha = null;
                for (; true;)
                {
                    val = FindElementlogic("//*[@id=\"ctl00_ContentPlaceHolder1_divPan\"]");
                    if (val == true)
                    {
                        break;
                    }
                }
                IWebElement e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_divPan\"]"));
                for (; true;)
                {
                    val = FindElementlogic("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + i + "]/td[5]");
                    if (val == false && i > 2)
                    {
                        i = i - 1;
                        break;
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
                for (int j = 2; j <= i; j++)
                {
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[1]"));
                    string panlookup = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[2]"));
                    string aadhar = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[3]"));
                    string name = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[4]"));
                    string dob = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[5]"));
                    string instype = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[6]"));
                    string insr = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[7]"));
                    string agncycd = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[8]"));
                    string doa = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[9]"));
                    string stsagncy = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[10]"));
                    string stschndt = e1.Text;
                    e1 = FindElement(By.XPath("//*[@id=\"ctl00_ContentPlaceHolder1_gdvPan\"]/tbody/tr[" + j + "]/td[11]"));
                    string lic = e1.Text;
                    DateTime date;
                    DateTime date1;
                    try
                    {
                        date = DateTime.ParseExact(doa, "dd MMM yyyy", CultureInfo.InvariantCulture);
                        date1 = DateTime.ParseExact(stschndt, "dd MMM yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            date = DateTime.ParseExact(doa, "dd MMMM yyyy", CultureInfo.InvariantCulture);
                            date1 = DateTime.ParseExact(stschndt, "dd MMMM yyyy", CultureInfo.InvariantCulture);
                        }
                        catch (Exception e2)
                        {
                            try
                            {
                                date = DateTime.ParseExact(doa, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                date1 = DateTime.ParseExact(stschndt, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                            }
                            catch (Exception e3)
                            {
                                date = DateTime.ParseExact(doa, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                date1 = DateTime.ParseExact(stschndt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                    }


                    htParam2.Clear();
                    ds2.Clear();
                    htParam2.Add("@panlookup", panlookup);
                    htParam2.Add("@aadhar", aadhar);
                    htParam2.Add("@name", name);
                    htParam2.Add("@dob", dob);
                    htParam2.Add("@instype", instype);
                    htParam2.Add("@insr", insr);
                    htParam2.Add("@agncycd", agncycd);
                    htParam2.Add("@doa", date);
                    htParam2.Add("@stsagncy", stsagncy);
                    htParam2.Add("@stschndt", date1);
                    htParam2.Add("@lic", lic);
                   // ds2 = dataAccessRecruit.GetDataSetForPrcRecruit("Prc_Ins_Agncy_Lookup", htParam2);
                }
                //string name=e1.Text;
                //UpdateName(id, name);
                //utils.UpdateStatus(mpan, mintgid, "6");
                //utils.UpdateType(pan);
                //Dispose();
            }
            catch (Exception e5)
            {
                //utils.LogError("IRDAIAGP", e5.ToString());
                ////e5.ToString();
                //utils.UpdateStatus(mpan, mintgid, "8");
                //utils.UpdateResult(mpan, mintgid, "8", "Please verify PAN");
                //Dispose();
            }
        }


        public IWebElement FindElement(By by)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    IWebElement e = driver.FindElement(by);
                    if (e != null)
                    {
                        return e;
                    }
                }
                catch (Exception e)
                {

                }
                Thread.Sleep(2000);
            }
            throw new Exception("Element not found");
        }

        public bool FindElementlogic(string loc)
        {
            try
            {
                bool v = FindElement(By.XPath(loc)).Displayed;
                return v;
            }
            catch
            {
                return false;
            }
        }

        //public void InvokeSelenum<T>(TestStep testStep, TestCase testCase, TestData testData, WebDriver webDriver, List<T> list)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
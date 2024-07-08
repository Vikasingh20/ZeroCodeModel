
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using ZeroCodeFramework;

namespace ZeroCodeDynamic
{
    public interface IDynamicInvokeSelenum
    {
        void InvokeSelenum<T>(TestStep testStep, TestCase testCase, TestData testData, WebDriver webDriver, List<T> list);
    }
}

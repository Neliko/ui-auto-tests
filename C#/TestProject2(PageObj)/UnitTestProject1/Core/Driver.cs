//using System;

//using DoubleGis.Erm.UnitTestProject1.Core;

//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.IE;

//public class Driver
//{
//    private static IWebDriver webDriverInstance;

//    private Driver()
//    {
//    }

//    public static IWebDriver WebDriverInstance
//    {
//        get
//        {
//            if (webDriverInstance == null)
//            {
//                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method Start.");
//            }
//            return webDriverInstance;
//        }
//        private set
//        {
//            webDriverInstance = value;
//        }
//    }

//    public static void StartBrowser(BrowserTypes browserType = BrowserTypes.Chrome, int defaultTimeOut = 30)
//    {
//        switch (browserType)
//        {
//            case BrowserTypes.Chrome:
//                {
//                    webDriverInstance = new ChromeDriver();
//                    break;
//                }
//            case BrowserTypes.InternetExplorer:
//                {
//                    webDriverInstance = new InternetExplorerDriver();
//                    break;
//                }
//        }
//      //  BrowserWait = new WebDriverWait(Driver.Browser, TimeSpan.FromSeconds(defaultTimeOut));
//    }

//    public static void Close()
//    {
//        if (webDriverInstance != null)
//        {
//            webDriverInstance.Quit();
//            webDriverInstance = null;
//        }
//    }
//}
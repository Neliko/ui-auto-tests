using System.Collections.Generic;
using System.Linq;

using DoubleGis.Erm.UnitTestProject1.Elements;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DoubleGis.Erm.UnitTestProject1.Pages
{
   public class MainPage
   {
       private string baseUrl = "http://workspace19.test.crm.2gis.ru";
       public IWebDriver WebDriver { get; set; }
       private IWebElement mainTittle;

        public MainPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }

       [FindsBy(How = How.XPath, Using = "//*[contains(@class,'title__text')]")]
       public IWebElement MainTitle { get; set; } 

      public SearchBar SearchBar => new SearchBar(WebDriver.FindElement(By.XPath("//*[contains(@class, 'searchbar')]")));

       public IList<UserDeal> Deals => GetUserDealolletion();

       public IWebDriver Open(string account = null)
       {
           if (account != null)
           {
               baseUrl = $"{baseUrl}?me={account}";
           }

           WebDriver.Navigate().GoToUrl(url: baseUrl);
           return WebDriver;
       }

       private IList<UserDeal> GetUserDealolletion()
       {
           var userdealList = WebDriver.FindElements(By.ClassName("deal"));

           return userdealList.Select(x => new UserDeal(x)).ToList();
       }
   }
}

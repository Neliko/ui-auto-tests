using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DoubleGis.Erm.UnitTestProject1.Pages
{
    public class DealPage
    {
        private string dealPageUrl = "http://workspace19.test.crm.2gis.ru/deal";
        private IWebDriver WebDriver { get; set; }

        public DealPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'deal-title__deal-name')]")]
        public IWebElement DealName { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'deal-toolbar__next-deal')]")]
        public IWebElement GoToNextDealButton { get; set; }

        public IWebDriver Open(long dealId, string account = null)
        {
            dealPageUrl = $"{dealPageUrl}/{dealId}";
            if (account != null)
            {
                dealPageUrl = $"{dealPageUrl}?me={account}";
            }

            WebDriver.Navigate().GoToUrl(url: dealPageUrl);
            return WebDriver;
        }
    }
}

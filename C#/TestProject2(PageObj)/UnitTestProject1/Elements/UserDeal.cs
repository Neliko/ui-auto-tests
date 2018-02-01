using OpenQA.Selenium;

namespace DoubleGis.Erm.UnitTestProject1.Elements
{
    public class UserDeal
    {
        private readonly IWebElement currentElement;

        public UserDeal(IWebElement userDealElement)
        {
            currentElement = userDealElement;
        }

        public IWebElement DealNameElement => currentElement.FindElement(By.XPath(".//*[contains(@class, 'text-with-match')]"));
        public IWebElement CheckDealElement => currentElement.FindElement(By.XPath(".//*[contains(@class, 'checkbox')]"));

		// ЛУчше бы описать как отдельный элемент  dealActivity, который содержит иконку, дату ,описание действия
        //public IWebElement LastClosedActivityElement;
        //public IWebElement FirstOpenActivityElement;

		// надо научиться различать выставленнй и снятый флаг. Внутри меняется class span
        public IWebElement FlagForFollowUpElement => currentElement.FindElement(By.XPath(".//*[contains(@class, 'deal__followUp')]"));
        public IWebElement ColorMarkerElement => currentElement.FindElement(By.XPath(".//*[contains(@class, 'color-marker')]"));

        public void Click()
        {
			currentElement.Click();
        }
    }
}

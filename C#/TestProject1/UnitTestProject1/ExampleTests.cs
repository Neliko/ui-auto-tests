using System;
using System.Linq;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DoubleGis.Erm.UnitTestProject1
{
    [TestFixture]
    public class ExampleTests
    {
        private IWebDriver ChromeDriver { get; set; }
        private const string MainUrl = "http://workspace19.test.crm.2gis.ru/";
        public static readonly string DealUrl = $"http://workspace19.test.crm.2gis.ru/deal";

        [OneTimeSetUp]
        public void SetUp()
        {
            ChromeDriver = new ChromeDriver();
            ChromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void MainTitleDisplayedTest()
        {
            ChromeDriver.Navigate().GoToUrl(MainUrl);
            var tabTitle = ChromeDriver.Title;

            const string ExpextedTitle = "Рабочая область";
            Assert.That(tabTitle, Is.EqualTo(ExpextedTitle));

            //Чтобы не зависеть от автогенерируемых значений, можно делать так. Или попытаться написать расширение - поиск элемента по части названия класса
            var mainTitleElement = ChromeDriver.FindElement(By.XPath("//*[contains(@class,'title__text')]"));
            Assert.That(mainTitleElement.Displayed, Is.True);
            Assert.That(mainTitleElement.Text, Is.EqualTo(ExpextedTitle));
        }

        [Test]
        public void SearchBarCorrectlyDisplayedTest()
        {
            ChromeDriver.Navigate().GoToUrl(MainUrl);

            var searchBarElement = ChromeDriver.FindElement(By.XPath("//*[contains(@class, 'searchbar')]"));
            // проверяем, что он отображен
            Assert.That(searchBarElement.Displayed, Is.True);
            Assert.That(searchBarElement.Enabled, Is.True);
            var text = searchBarElement.FindElement(By.XPath("//*[contains(@class, 'searchbar__field')]")).GetAttribute("placeholder");

            ////проверяем, что текст соответствует ожидаемому
            const string ExpextedSearchText = "Поиск по названию работы или фирмы";
            Assert.That(text, Is.EqualTo(ExpextedSearchText));

            //// Проверяем наличие внутри элемента строки поиска кнопки поиска
            var searchBarButton = searchBarElement.FindElement(By.XPath("//*[contains(@class, 'searchbar__button')]"));
            Assert.That(searchBarButton.Enabled, Is.True);
            Assert.That(searchBarButton.Displayed, Is.True);
        }

        [Test]
        public void DealTitleTest()
        {
            // User with any deal
            const int DealId = 139;
            var url = $"{DealUrl}/{DealId}";
           ChromeDriver.Navigate().GoToUrl(url);

            // Нужно получат ожидаемое название работы.
            var ExpectedDealName = "Русская охота, гостиничный комплекс";
            // "Работа" - вытащить в ресурсники
            var expetedTitle = $"Работа: {ExpectedDealName}";

            var titleElement = ChromeDriver.FindElement(By.XPath("//*[contains(@class, 'deal-title__deal-name')]"));
            Assert.That(titleElement != null, $"Couldn't find header for the deal = {ExpectedDealName}");
            Assert.That(titleElement.Text, Is.EqualTo(expetedTitle));

            Assert.That(ChromeDriver.Title, Is.EqualTo(expetedTitle));
        }

        [Test]
        public void NavigateToDeal_NavigationToNextDeal()
        {
            // User with some deal
            const string UserAccount = "l.sveta";
            ChromeDriver.Navigate().GoToUrl($"{MainUrl}");
            ChromeDriver.Navigate().GoToUrl($"{MainUrl}?me={ UserAccount}");

            var dealElementsCondition = By.XPath("//*[contains(@class, 'deals-list__deal')]");

            // find deal in list
            var dealCount = 2;
            var deals = ChromeDriver.FindElements(dealElementsCondition).Take(dealCount).ToList();
            Assert.That(deals.Count == dealCount, $"Couldn't find {dealCount} deal in deal list for the user = {UserAccount}");

            var firstDeal = deals.FirstOrDefault();
            var dealName = firstDeal.FindElement(By.XPath(".//*[contains(@class, 'link')]")).Text;

            var secondDeal = deals.LastOrDefault();
            var dealNextName = secondDeal.FindElement(By.XPath(".//*[contains(@class, 'link')]")).Text;

            firstDeal.Click();

            var expetedTitle = $"Работа: {dealName}";

            var titleElement = ChromeDriver.FindElement(By.XPath("//*[contains(@class, 'deal-title__deal-name')]"));
            Assert.That(titleElement != null, $"Couldn't find header for the deal = {expetedTitle}");
            Assert.That(titleElement.Text, Is.EqualTo(expetedTitle));
            Assert.That(ChromeDriver.Title, Is.EqualTo(expetedTitle));

            var nextButton = ChromeDriver.FindElement(By.XPath("//*[contains(@class, 'deal-toolbar__next-deal')]"));
            Assert.That(nextButton.Enabled, Is.True);
            Assert.That(nextButton.Displayed, Is.True);

            nextButton.Click();

            var expetedNextTitle = $"Работа: {dealNextName}";

            titleElement = ChromeDriver.FindElement(By.XPath("//*[contains(@class, 'deal-title__deal-name')]"));
            Assert.That(titleElement != null, $"Couldn't find header for the deal = {expetedNextTitle}");
            Assert.That(titleElement.Text, Is.EqualTo(expetedNextTitle));
            Assert.That(ChromeDriver.Title, Is.EqualTo(expetedNextTitle));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            ChromeDriver.Close();
        }
    }
}

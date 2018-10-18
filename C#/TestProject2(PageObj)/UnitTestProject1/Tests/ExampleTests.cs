using System;
using System.Linq;

using DoubleGis.Erm.UnitTestProject1.Pages;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DoubleGis.Erm.UnitTestProject1.Tests
{
    [TestFixture]
    public class ExampleTests
    {
        private IWebDriver chromeDriver; 

        [OneTimeSetUp]
        public void SetUp()
        {
            chromeDriver = new ChromeDriver();
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void MainTitleDisplayedTest()
        {
            var page = new MainPage(chromeDriver);
            page.Open();
            var tabTitle = chromeDriver.Title;

            const string ExpextedTitle = "Рабочая область";
            Assert.That(tabTitle, Is.EqualTo(ExpextedTitle));

            var titleTextElement = page.MainTitle;
            Assert.That(titleTextElement.Displayed, Is.True);
            Assert.That(titleTextElement.Text, Is.EqualTo(ExpextedTitle));
        }

        [Test]
        public void SearchBarCorrectlyDisplayedTest()
        {
            var page = new MainPage(chromeDriver);
            page.Open();

            var searchElement = page.SearchBar;

            // проверяем, что он отображен
            Assert.That(searchElement.IsDisplayed, Is.True);
            Assert.That(searchElement.IsEnabled, Is.True);

            var text = page.SearchBar.GetPlaceholderText();

            //проверяем, что текст соответствует ожидаемому
            const string ExpextedSearchText = "Поиск по названию работы или фирмы";
            Assert.That(text, Is.EqualTo(ExpextedSearchText));

            // Проверяем наличие внутри элемента строки поиска кнопки поиска
            var searchBarButton = page.SearchBar.SearchButton;
            Assert.That(searchBarButton.Enabled, Is.True);
            Assert.That(searchBarButton.Displayed, Is.True);
        }

        [Test]
        public void DealTitleTest()
        {
            // User with any deal
            const int DealId = 139;
            var page = new DealPage(chromeDriver);
            page.Open(DealId);

            // Нужно получат ожидаемое название работы.
            const string ExpectedDealName = "Русская охота, гостиничный комплекс";

            // "Работа" - вытащить в ресурсники
            var expetedTitle = $"Работа: {ExpectedDealName}";
            var titleElement = page.DealName;
            Assert.That(titleElement != null, $"Couldn't find header for the deal = {ExpectedDealName}");
            Assert.That(titleElement.Text, Is.EqualTo(expetedTitle));
            Assert.That(chromeDriver.Title, Is.EqualTo(expetedTitle));
        }

        [Test]
        public void NavigateToDeal_NavigationToNextDeal()
        {
            //User with some deal
            const string UserAccount = "l.sveta";
            var page = new MainPage(chromeDriver);
            page.Open();
            page.Open(UserAccount);

            const int DealCount = 2;
            var deals = page.Deals.Take(DealCount).ToList();
            Assert.That(deals.Count == DealCount, $"Couldn't find {DealCount} deal in deal list for the user = {UserAccount}");

            var a = deals.Select(x => x.DealNameElement.Text).ToList();

            var firstDeal = deals.FirstOrDefault();
            // ReSharper disable once PossibleNullReferenceException

            // было бы круто вытащить это в отдельный класс
            var dealName = firstDeal.DealNameElement.Text;

            var secondDeal = deals.LastOrDefault();
            var dealNextName = secondDeal.DealNameElement.Text;

            firstDeal.Click();

            var dealPage = new DealPage(chromeDriver);

            var expetedTitle = $"Работа: {dealName}";
            var titleElement = dealPage.DealName;
            Assert.That(titleElement != null, $"Couldn't find header for the deal = {expetedTitle}");
            Assert.That(titleElement.Text, Is.EqualTo(expetedTitle));
            Assert.That(chromeDriver.Title, Is.EqualTo(expetedTitle));

            var nextButton = dealPage.GoToNextDealButton;
            Assert.That(nextButton.Enabled, Is.True);
            Assert.That(nextButton.Displayed, Is.True);

            nextButton.Click();

            var expetedNextTitle = $"Работа: {dealNextName}";
            titleElement = dealPage.DealName;
            Assert.That(titleElement != null, $"Couldn't find header for the deal = {expetedNextTitle}");
            Assert.That(titleElement.Text, Is.EqualTo(expetedNextTitle));
            Assert.That(chromeDriver.Title, Is.EqualTo(expetedNextTitle));
        }

        [OneTimeTearDown]
        public void Close()
        {
            chromeDriver.Quit();
        }
    }
}

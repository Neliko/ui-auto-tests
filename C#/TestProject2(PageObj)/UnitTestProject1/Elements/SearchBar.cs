using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DoubleGis.Erm.UnitTestProject1.Elements
{
    public class SearchBar
    {
        private readonly IWebElement currentElement;

        public bool IsDisplayed => currentElement.Displayed;
        public bool IsEnabled => currentElement.Enabled;

        public IWebElement TextField => currentElement.FindElement(By.XPath(".//*[contains(@class, 'search-bar__field')]"));

        public IWebElement SearchButton => currentElement.FindElement(By.XPath(".//*[contains(@class, 'search-bar__button')]"));

        public SearchBar(IWebElement searchBarElement)
        {
            currentElement = searchBarElement;
        }

        public string GetPlaceholderText()
        {
            return TextField.GetAttribute("placeholder");
        }

        public void Search(string searchText) { }
    }
}
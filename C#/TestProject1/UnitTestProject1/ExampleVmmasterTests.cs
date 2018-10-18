using System;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace DoubleGis.Erm.UnitTestProject1
{
    [TestFixture]
    class ExampleVmmasterTests
    {
        [Test]
        public void TestWithVmmaster()
        {
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
            capabilities.SetCapability(CapabilityType.Version, "62");
            capabilities.SetCapability(CapabilityType.Platform, "DOCKER");
            capabilities.SetCapability(CapabilityType.TakesScreenshot, true);

            var command_executor = new Uri("http://vmmaster.test:9001/wd/hub");
            var driver = new RemoteWebDriver(command_executor, capabilities);

            var url = "https://workspace18.test.crm.2gis.ru";
            driver.Navigate().GoToUrl(url);

            var title = driver.Title;

            Assert.That(title, Is.EqualTo("Рабочая область"), "что-то пошло не так");
            driver.Quit();
        }
    }
}

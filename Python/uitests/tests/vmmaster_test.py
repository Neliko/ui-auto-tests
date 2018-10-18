import unittest
from time import sleep

from vmmaster_client import client

from selenium import webdriver

class TempTests(unittest.TestCase):
    """
    selenium==3.8.0
    git+https://github.com/2gis/vmmaster-client.git
    """

# тест для запуска хром с обходом win auth. Раньше работал, сейчас, кажется, нет
    def test_first(self):
        driver = webdriver.Remote(
            command_executor="http://vmmaster.test:9001/wd/hub",
            desired_capabilities={
                "platform": "DOCKER",
                "browserName": "chrome",
                "version": "62",
                "takeScreenshot": True,
                "takeScreencast": True,
            }
        )

        driver.get("https://workspace18.test.crm.2gis.ru/")

        vmc = client.vmmaster(driver)
        auth_string = "xdotool type 'manager1.1'\nxdotool key Tab\nxdotool type '123qwe!'\nxdotool key Return"
        vmc.run_script(auth_string)  # для самого сайта
        vmc.run_script(auth_string)  # для урла телефонии

        sleep(10)  # чтоб видео было не столь коротким
        driver.quit()


if __name__ == '__main__':
    unittest.main()

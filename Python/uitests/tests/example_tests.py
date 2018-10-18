from unittest import TestCase
import unittest
from selenium import webdriver
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.support.wait import WebDriverWait

from pages.main_page import MainPage
from pages.deal_page import DealPage


class SeleniumWD(TestCase):
    def setUp(self):
        # путь до chromedriver.exe
        self.driver = webdriver.Chrome("D:\TestUiProject\Python\Python project\selenium\chromedriver.exe")
        self.driver.implicitly_wait(30)

    def tearDown(self):
        self.driver.close()

    def test_find_main_title(self):
        self.page = MainPage(self.driver)
        self.page.open()

        _a = self.driver.title
        self.assertTrue("Рабочая область" in self.page.main_title.text, "Неверное значение основного зашоловка")

    def test_searchBar_correctly_displayed(self):
        self.page = MainPage(self.driver)
        self.page.open()

        self.assertTrue(self.page.search_bar.element.is_displayed(), "SearchBar не отображен")
        self.assertTrue(self.page.search_bar.element.is_enabled(), "SearchBar недоступен для редактирвания")

        text = "Поиск по названию работы или фирмы"
        print(self.page.search_bar.placeholder)
        self.assertEqual(text, self.page.search_bar.placeholder, "Ожидаемое значение не найдено в placeholder")

        search_btn = self.page.search_bar.search_button
        self.assertTrue(search_btn.is_displayed(), "Кнопка поиска нt отображена не отображен")
        self.assertTrue(search_btn.is_enabled(), "SearchBar недоступен для редактирвания")

    def test_deal_title(self):
        deal_id = 139
        self.page = DealPage(self.driver)
        self.page.open(deal_id)

        expected_deal_name = "Русская охота, гостиничный комплекс"
        expected_title_name = 'Работа: ' + expected_deal_name

        wait = WebDriverWait(self.driver, 10)
        element = wait.until(EC.title_is(expected_title_name))

        self.assertIsNotNone(self.page.deal_name, "Не удалось найти заголовок")
        self.assertEqual(expected_title_name, self.page.deal_name.text, "Ожидаемое значение названия работы не "
                                                                        "совпадает с фактическим")


if __name__ == '__main__':
    unittest.main()

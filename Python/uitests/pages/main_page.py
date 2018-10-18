class MainPage():
    def __init__(self, driver):
        self.driver = driver
        self.driver.implicitly_wait(100)
        self._search_bar = None
        self._main_title = None
        self._expected_result = None

    @property
    def search_bar(self):
        from elements.search_bar import SearchBar

        if self._search_bar is None:
            self._search_bar = SearchBar(self.driver, self.driver.find_element_by_xpath("//*[contains(@class, 'searchbar')]"))
        return self._search_bar

    @property
    def main_title(self):
        from elements.main_title import MainTitle

        if self._main_title is None:
            self._main_title = MainTitle(self.driver, self.driver.find_element_by_xpath("//*[contains(@class, 'text-title')]"))
        return self._main_title

    @property
    def page_source(self):
        return self.driver.page_source

    # @property
    # def expected_result(self):
    #   from expected_result import ExpectedResult
    #   if self._expected_result is None:
    #      self._expected_result = ExpectedResult(self.driver,

    #                                        self.driver.find_elements_by_class_name(ExpectedResult.selectors['expected_text']))
    # return self._expected_result

    def open(self, account=None):
        url = "http://workspace19.test.crm.2gis.ru"
        if account is not None:
            url = url + "?me=" + account
        self.driver.get(url)

    @property
    def title(self):
        return self.driver.title

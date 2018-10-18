class DealPage():
    def __init__(self, driver):
        self.driver = driver
        self.driver.implicitly_wait(100)
        self._deal_name = None
        self._expected_result = None

    @property
    def deal_name(self):
        from elements.deal_name import DealName
        if self._deal_name is None:
            self._deal_name = DealName(self.driver, self.driver.find_element_by_xpath("//*[contains(@class, 'deal-title__deal-name')]"))
        return self._deal_name
        
    def open(self, deal_id, account=None):
        url = 'http://workspace19.test.crm.2gis.ru/deal/' + str(deal_id)
        if account is not None:
            url = url + "?me=" + account
        self.driver.get(url)

    @property
    def title(self):
        return self.driver.title
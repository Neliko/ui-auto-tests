__author__ = 'neliko'
from elements.base_component import BaseComponent


class DealName(BaseComponent):

    @property
    def text(self):
        return self.driver.find_element_by_xpath("//*[contains(@class, 'deal-title__deal-name')]").text
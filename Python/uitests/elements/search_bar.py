__author__ = 'neliko'
from elements.base_component import BaseComponent


class SearchBar(BaseComponent):

    @property
    def text_field(self):
        return self.driver.find_element_by_xpath("//*[contains(@class, 'searchbar__field')]")
    @property
    def search_button(self):
        return self.driver.find_element_by_xpath("//*[contains(@class, 'searchbar__button')]")
    @property
    def placeholder(self):
        return self.text_field.get_attribute("placeholder")

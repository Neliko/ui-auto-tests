__author__ = 'neliko'
from elements.base_component import BaseComponent


class MainTitle(BaseComponent):

    @property
    def text(self):
        return self.element.text
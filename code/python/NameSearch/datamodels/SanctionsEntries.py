__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from CustomXmlElement import *

class SanctionsEntries(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(SanctionsEntries, self).__init__(root, namespace)

    def __load(self):
        self.Sanctions = {};

        for child_node in self._select_children('SanctionsEntry'):
            id = child_node.attrib['ID']
            data_set = child_node.attrib
            data_set['Element'] = child_node
            self.Sanctions[id] = data_set
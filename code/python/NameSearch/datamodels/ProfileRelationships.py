__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from CustomXmlElement import *

class ProfileRelationships(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(ProfileRelationships, self).__init__(root, namespace)

    def _load(self):
        self.Relationships = {};

        for child_node in self._select_children('ProfileRelationship'):
            id = child_node.attrib['ID']
            data_set = child_node.attrib
            data_set['Element'] = child_node
            self.Relationships[id] = data_set

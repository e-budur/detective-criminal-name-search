__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from CustomXmlElement import *

class IDRegDocuments(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(IDRegDocuments, self).__init__(root, namespace)

    def _load(self):
        self.List = {};

        for child_node in self._select_children('IDRegDocument'):
            id = child_node.attrib['ID']
            data_set = child_node.attrib
            data_set['IDRegDocument'] = IDRegDocument(child_node, self.ns)
            self.List[id] = data_set


class IDRegDocument(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(IDRegDocument, self).__init__(root, namespace)

    def _load(self):

        self.ID = self._get_attrib_int('ID')
        self.IDRegDocTypeID = self._get_attrib_int('IDRegDocTypeID')
        self.IssuedByCountryID = self._get_attrib_int('IssuedBy-CountryID')
        self.ValidityID = self._get_attrib_int('ValidityID')
        self.Comment = self._get_node_text('Comment')
        self.IDRegistrationNo = self._get_node_text('IDRegistrationNo')
        self.IssuingAuthority = self._get_node_text('IssuingAuthority')
        self.DocumentedNameID = self._get_node_attrib_int('DocumentedNameReference', 'DocumentedNameID')

__author__ = 'e-budur'
import xml.etree.ElementTree as ET

class IDRegDocuments:

    def __init__(self, root, namespace) :
        self.root = root
        self.ns = namespace
        self.__load()

    def __load(self):
        self.List = {};

        self.node = self.__select_node('IDRegDocuments')
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':IDRegDocument'

        for child_node in self.node.findall(xpath_str, self.ns):
            id = child_node.attrib['ID']
            data_set = child_node.attrib
            data_set['IDRegDocument'] = IDRegDocument(child_node, self.ns)
            self.List[id] = data_set

    def __select_node(self, tag_name):
        namespace_tag = self.ns.keys()[0]
        node = self.root.findall(namespace_tag+':'+tag_name, self.ns)[0]
        return node



class IDRegDocument(IDRegDocuments):
    ID = 0
    IDRegDocTypeID = 0
    IdentityID = 0
    IssuedByCountryID = 0
    ValidityID = 0
    Comment = ''
    IDRegistrationNo = ''
    IssuingAuthority = ''
    DocumentedNameReference = ''
    DocumentedNameID = ''



    def __init__(self, node, namespace) :
        self.node = node
        self.ns = namespace
        self.__load()

    def __load(self):

        print self.node.attrib
        self.ID = self.__get_attrib_int('ID')
        self.IDRegDocTypeID = self.__get_attrib_int('IDRegDocTypeID')
        self.IssuedByCountryID = self.__get_attrib_int('IssuedBy-CountryID')
        self.ValidityID = self.__get_attrib_int('ValidityID')
        self.Comment = self.__get_node_text('Comment')
        self.IDRegistrationNo = self.__get_node_text('IDRegistrationNo')
        self.IssuingAuthority = self.__get_node_text('IssuingAuthority')

        child = self.__get_node('DocumentedNameReference')
        self.DocumentedNameID = int(child.attrib['DocumentedNameID'])


    def __get_node(self, tag):
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+tag
        child = self.node.findall(xpath_str, self.ns)[0]
        return child

    def __get_attrib_int(self, attribName):
        if attribName not in self.node.attrib.keys():
            return 0

        return int(self.node.attrib[attribName])

    def __get_node_text(self, tag):
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+tag
        child = self.node.findall(xpath_str, self.ns)[0]
        if child == None:
            return ""
        return child.text

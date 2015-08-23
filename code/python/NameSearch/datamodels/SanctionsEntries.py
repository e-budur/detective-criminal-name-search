__author__ = 'e-budur'
import xml.etree.ElementTree as ET

class SanctionsEntries:

    def __init__(self, root, namespace) :
        self.root = root
        self.ns = namespace
        self.__load()

    def __load(self):
        self.Sanctions = {};

        self.node = self.__select_node('SanctionsEntries');

        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':SanctionsEntry'
        print xpath_str
        for child_node in self.node.findall(xpath_str, self.ns):
            id = child_node.attrib['ID']
            data_set = child_node.attrib
            data_set['Element'] = child_node
            self.Sanctions[id] = data_set

        print self.Sanctions

    def __select_node(self, tag_name):
        namespace_tag = self.ns.keys()[0]
        node = self.root.findall(namespace_tag+':'+tag_name, self.ns)[0]
        return node
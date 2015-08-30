__author__ = 'admin'

__author__ = 'e-budur'
import xml.etree.ElementTree as ET

class CustomXmlElement(object):

    def __init__(self, root, namespace) :
        self.root = root
        self.ns = namespace
        self._load()

    def _select_node(self, tag_name):
        namespace_tag = self.ns.keys()[0]
        node = self.root.findall(namespace_tag+':'+tag_name, self.ns)[0]
        return node


    def _load(self):
        pass

    def _select_node(self, tag_name):
        return self._get_node(tag_name)

    def _get_text(self):
        if self.root == None:
            return ""

        return self.root.text;

    def _get_node_text(self, tag):
        node = self._get_node(tag)

        if node == None:
            return ""

        return node.text

    def _get_node(self, tag):

        if self.root == None:
            return None

        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+tag

        nodes = self.root.findall(xpath_str, self.ns)
        if nodes == None or len(nodes) == 0:
            return None

        return nodes[0]

    def _get_node_attrib_int(self, tag, attribName):

        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+tag
        nodes = self.root.findall(xpath_str, self.ns)
        if nodes == None or len(nodes) == 0:
            return 0

        if nodes[0] == None:
            return 0;

        if attribName not in nodes[0].attrib.keys():
            return 0

        return int(nodes[0].attrib[attribName])


        return nodes[0]

    def _get_attrib_int(self, attribName):
        if self.root == None:
            return 0;

        if attribName not in self.root.attrib.keys():
            return 0

        return int(self.root.attrib[attribName])

    def _get_attrib_bool(self, attribName):
        if self.root == None:
            return False

        if attribName not in self.root.attrib.keys():
            return False

        return self.root.attrib[attribName].lower() == 'true'

    def _select_children(self, tag_name):
        if self.root == None:
            return []

        namespace_tag = self.ns.keys()[0]
        return self.root.findall(namespace_tag+':'+tag_name, self.ns)

    def _select_children_of_child(self, child_tag_name, grand_children_tag_name):

        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+child_tag_name

        nodes = self.root.findall(xpath_str, self.ns)
        if nodes == None or len(nodes) == 0:
            return None

        node = nodes[0]

        return node.findall(namespace_tag+':'+grand_children_tag_name, self.ns)
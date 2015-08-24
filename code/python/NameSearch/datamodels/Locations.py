__author__ = 'e-budur'
import xml.etree.ElementTree as ET


class Locations(object):

    def __init__(self, root, namespace) :
        self.root = root
        self.ns = namespace
        self.__load()

    def __load(self):

        self.node = self.__select_node('Locations')

        self.List = {};

        for child_node in self.__select_children('Location'):
            id = child_node.attrib['ID']
            data_set = child_node.attrib
            data_set['Location'] = Location(child_node, self.ns)
            self.List[id] = data_set

    def __select_children(self, tag_name):
        namespace_tag = self.ns.keys()[0]
        return self.node.findall(namespace_tag+':'+tag_name, self.ns)

    def __select_node(self, tag_name):
        namespace_tag = self.ns.keys()[0]
        node = self.root.findall(namespace_tag+':'+tag_name, self.ns)[0]
        return node



class Location(Locations):
    ID = 0
    LocationAreaCodeId = 0
    Country = 0

    def __init__(self, node, namespace) :
        self.node = node
        self.ns = namespace
        self.__load()

    def __load(self):

        child = self.__get_node('LocationAreaCode')
        self.LocationAreaCodeId = int(child.attrib['AreaCodeID'])

        child = self.__get_node('LocationCountry')
        self.Country = Country(child, self.ns)

        child = self.__get_node('LocationPart')
        self.LocationPart = LocationPart(child, self.ns)


    def __get_node(self, tag):
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+tag
        child = self.node.findall(xpath_str, self.ns)[0]
        return child

class Country(Location):
    CountryID = 0
    CountryRelevanceID = 0

    def __init__(self, node, namespace) :
        self.node = node
        self.ns = namespace
        self.__load()

    def __load(self):
        self.CountryID = int(self.node.attrib['CountryID'])
        self.CountryRelevanceID = int(self.node.attrib['CountryRelevanceID'])

class LocationPart(Location):
    LocPartTypeID = 0
    LocationPartValues = []

    def __init__(self, node, namespace) :
        self.node = node
        self.ns = namespace
        self.__load()

    def __load(self):

        self.LocPartTypeID = int(self.node.attrib['LocPartTypeID'])
        child = self.__get_node('LocationPartValue')
        self.LocationPartValue  = LocationPartValue(child, self.ns)

    def __get_node(self, tag):
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+tag
        child = self.node.findall(xpath_str, self.ns)[0]
        return child


class LocationPartValue(LocationPart):
    Primary = False
    LocPartValueTypeID = 0
    LocPartValueStatusID = 0
    Comment = ""
    Value = ""

    def __init__(self, node, namespace) :
        self.node = node
        self.ns = namespace
        self.__load()

    def __load(self):

        self.Primary = self.node.attrib['Primary'].lower() == 'true'
        self.LocPartValueTypeID = int(self.node.attrib['LocPartValueTypeID'])
        self.LocPartValueStatusID = int(self.node.attrib['LocPartValueStatusID'])

        self.Comment = self.__get_node_text('Comment')
        self.Value = self.__get_node_text('Value')


    def __get_node_text(self, tag):
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+tag
        child = self.node.findall(xpath_str, self.ns)[0]
        return child.text

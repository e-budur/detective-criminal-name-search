__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from CustomXmlElement import *

class Locations(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(Locations, self).__init__(root, namespace)

    def _load(self):

        self.node = self._select_node('Locations')

        self.List = {};

        for child_node in self._select_children('Location'):
            id = child_node.attrib['ID']
            data_set = child_node.attrib
            data_set['Location'] = Location(child_node, self.ns)
            self.List[id] = data_set


class Location(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(Location, self).__init__(root, namespace)

    def _load(self):

        child = self._get_node('LocationAreaCode')
        self.LocationAreaCodeId = int(child.attrib['AreaCodeID'])

        child = self._get_node('LocationCountry')
        self.Country = Country(child, self.ns)

        child = self._get_node('LocationPart')
        self.LocationPart = LocationPart(child, self.ns)


class Country(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(Country, self).__init__(root, namespace)

    def _load(self):
        self.CountryID = self._get_attrib_int('CountryID')
        self.CountryRelevanceID =  self._get_attrib_int('CountryRelevanceID')

class LocationPart(CustomXmlElement):
    LocationPartValues = []

    def __init__(self, root, namespace) :
        super(LocationPart, self).__init__(root, namespace)

    def _load(self):

        self.LocPartTypeID = self._get_attrib_int('LocPartTypeID')
        for child in self._select_children('LocationPartValue'):
            lpv = LocationPartValue(child, self.ns)
            self.LocationPartValues.append(lpv)


class LocationPartValue(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(LocationPartValue, self).__init__(root, namespace)

    def _load(self):

        self.Primary = self._get_attrib_bool('Primary')
        self.LocPartValueTypeID = self._get_attrib_int('LocPartValueTypeID')
        self.LocPartValueStatusID = self._get_attrib_int('LocPartValueStatusID')

        self.Comment = self._get_node_text('Comment')
        self.Value = self._get_node_text('Value')



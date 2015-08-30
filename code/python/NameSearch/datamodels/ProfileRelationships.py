__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from CustomXmlElement import *

class ProfileRelationships(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(ProfileRelationships, self).__init__(root, namespace)

    def _load(self):
        self.DirectedList = {};
        self.UndirectedList = {};
        self.AllRelationshipDetails = []
        for child_node in self._select_children_of_child('ProfileRelationships', 'ProfileRelationship'):

            relationship = ProfileRelationship(child_node, self.ns)
            self.__appendDirectedRelationship(relationship.From, relationship.To)
            self.__appendUndirectedRelationship(relationship.From, relationship.To)
            self.__appendAllRelationships(relationship)

    def __appendUndirectedRelationship(self, fromID, toID):

        if fromID not in self.UndirectedList.keys():
            self.UndirectedList[fromID] = []

        if toID not in self.UndirectedList.keys():
            self.UndirectedList[toID] = []

        self.UndirectedList[fromID].append(toID)
        self.UndirectedList[toID].append(fromID)

    def __appendDirectedRelationship(self, fromID, toID):

        if fromID not in self.DirectedList.keys():
            self.DirectedList[fromID] = []

        self.DirectedList[fromID].append(toID)

    def __appendAllRelationships(self, relationship):
        self.AllRelationshipDetails.append(relationship)



class ProfileRelationship(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(ProfileRelationship, self).__init__(root, namespace)

    def _load(self):
        self.ID = self._get_attrib_int('ID')
        self.From = self._get_attrib_int('From-ProfileID')
        self.To = self._get_attrib_int('To-ProfileID')
        self.TypeID = self._get_attrib_int('RelationTypeID')
        self.QualityID = self._get_attrib_int('RelationQualityID')
        self.IsFormer = self._get_attrib_bool('Former')
        self.SanctionsEntryID = self._get_attrib_int('SanctionsEntryID')
        self.Comment = self._get_node_text('Comment')
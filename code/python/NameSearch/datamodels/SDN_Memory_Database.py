__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from ReferenceValueSet import *
from Locations import *
from IDRegDocuments import *
from DistinctParties import *
from ProfileRelationships import *
from SanctionsEntries import *

class SDN_Memory_Database:

    def __init__(self) :

        pass

    def load(self, filepath):
        self.ns = {'sdn': 'http://www.un.org/sanctions/1.0'}
        self.filepath = filepath
        self.tree = ET.parse(self.filepath)
        self.root = self.tree.getroot()
        self.__load_data()

    def __load_data(self):

        self.ReferenceValueSet = ReferenceValueSet(self.root, self.ns)
        self.Locations = Locations(self.root, self.ns)
        self.IDRegDocuments = IDRegDocuments(self.root, self.ns)
        self.DistinctParties = DistinctParties(self.root, self.ns)
        self.ProfileRelationships = ProfileRelationships(self.root, self.ns)
        self.SanctionsEntries = SanctionsEntries(self.root, self.ns)

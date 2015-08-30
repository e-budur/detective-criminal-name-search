__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from CustomXmlElement import *

class DistinctParties(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(DistinctParties, self).__init__(root, namespace)

    def _load(self):
        self.Profiles = [];

        self.node = self._select_node('DistinctParties')
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+ ':DistinctParty'

        for child_node in self.node.findall(xpath_str, self.ns):
            distinctParty = DistinctParty(child_node, self.ns)
            self.Profiles.append(distinctParty)


class DistinctParty(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(DistinctParty, self).__init__(root, namespace)

    def _load(self):

        self.FixedRef = self._get_attrib_int('FixedRef')
        self.Comment = self._get_node_text('Comment')

        child = self._get_node('Profile')

        self.Profile = Profile(child, self.ns)

class Profile(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(Profile, self).__init__(root, namespace)

    def _load(self):
        self.ID = self._get_attrib_int('ID')
        self.PartySubTypeID = self._get_attrib_int('PartySubTypeID')

        node = self._get_node('Identity')
        self.Identity = Identity(node, self.ns)

        self.Features = []
        for node in self._select_children('Feature'):
            feature = Feature(node, self.ns)
            self.Features.append(feature)

class Identity(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(Identity, self).__init__(root, namespace)

    def _load(self):
        self.ID = self._get_attrib_int('ID')
        self.FixedRef = self._get_attrib_int('FixedRef')
        self.Primary = self._get_attrib_int('ID')
        self.False = self._get_attrib_bool('False')
        self.Aliases = []

        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':Alias'
        self.Names = [];
        for child_node in self.root.findall(xpath_str, self.ns):
            alias = Alias(child_node, self.ns)
            self.Aliases.append(alias)

        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':NamePartGroups/'+namespace_tag+':MasterNamePartGroup/'+namespace_tag+':NamePartGroup'
        self.NamePartGroups = {};
        for child_node in self.root.findall(xpath_str, self.ns):
            namePartGroup = NamePartGroup(child_node, self.ns)
            self.__appendNamePart(namePartGroup)

    def __appendNamePart(self, namePartGroup):
        nameParts = [];
        if namePartGroup.NamePartTypeID not in self.NamePartGroups.keys():
            self.NamePartGroups[namePartGroup.NamePartTypeID] = nameParts
        self.NamePartGroups[namePartGroup.NamePartTypeID].append(namePartGroup)

class Alias(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(Alias, self).__init__(root, namespace)

    def _load(self):

        self.FixedRef = self._get_attrib_int('FixedRef')
        self.AliasTypeID = self._get_attrib_int('AliasTypeID')
        self.Primary = self._get_attrib_bool('Primary')
        self.LowQuality = self._get_attrib_bool('LowQuality')
        node = self._get_node('DocumentedName')
        self.DocumentedName = DocumentedName(node, self.ns)

class DocumentedName(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(DocumentedName, self).__init__(root, namespace)

    def _load(self):

        self.ID = self._get_attrib_int('ID')
        self.FixedRef = self._get_attrib_int('FixedRef')
        self.Primary = self._get_attrib_bool('Primary')
        self.DocNameStatusID = self._get_attrib_int('DocNameStatusID')

        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':DocumentedNamePart/'+namespace_tag+':NamePartValue'
        self.Names = [];

        for child_node in self.root.findall(xpath_str, self.ns):
            namePart = NamePartValue(child_node, self.ns)
            self.Names.append(namePart)


class NamePartValue(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(NamePartValue, self).__init__(root, namespace)

    def _load(self):

        self.NamePartGroupID = self._get_attrib_int('NamePartGroupID')
        self.ScriptID = self._get_attrib_int('ScriptID')
        self.ScriptStatusID = self._get_attrib_int('ScriptStatusID')
        self.Acronym = self._get_attrib_bool('Acronym')
        self.NameText = self._get_text();

class NamePartGroup(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(NamePartGroup, self).__init__(root, namespace)

    def _load(self):

        self.ID = self._get_attrib_int('ID')
        self.NamePartTypeID = self._get_attrib_int('NamePartTypeID')

class Feature(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(Feature, self).__init__(root, namespace)

    def _load(self):
        self.ID = self._get_attrib_int('ID')
        self.FeatureTypeID = self._get_attrib_int('FeatureTypeID')

        node = self._get_node('FeatureVersion')
        self.FeatureVersion = FeatureVersion(node, self.ns)

class FeatureVersion(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(FeatureVersion, self).__init__(root, namespace)

    def _load(self):

        self.ID = self._get_attrib_int('ID')
        self.ReliabilityID = self._get_attrib_int('ReliabilityID')
        self.Comment = self._get_node_text('Comment')
        self.VersionDetails = self._get_node_text('VersionDetail')
        self.VersionDetailTypeID = self._get_node_attrib_int('VersionDetail', 'DetailTypeID')
        self.VersionLocationID = self._get_node_attrib_int('VersionLocation', 'LocationID')
        self.IdentityReferenceID = self._get_node_attrib_int('IdentityReference', 'IdentityID')
        self.IdentityFeatureLinkTypeID = self._get_node_attrib_int('IdentityReference', 'IdentityFeatureLinkTypeID')
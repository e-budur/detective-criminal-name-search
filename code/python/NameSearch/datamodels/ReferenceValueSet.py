__author__ = 'e-budur'
import xml.etree.ElementTree as ET
from CustomXmlElement import *

class ReferenceValueSet(CustomXmlElement):

    def __init__(self, root, namespace) :
        super(ReferenceValueSet, self).__init__(root, namespace)

    def _load(self):

        self.node = self._select_node('ReferenceValueSets')
        self.AliasTypeValues = self.__loadReferenceEntities('AliasType');
        self.AreaCodeValues = self.__loadReferenceEntities('AreaCode');
        self.AreaCodeTypeValues = self.__loadReferenceEntities('AreaCodeType');
        self.CalendarTypeValues = self.__loadReferenceEntities('CalendarType');
        self.CountryValues = self.__loadReferenceEntities('Country');
        self.CountryRelevanceValues = self.__loadReferenceEntities('CountryRelevance');
        self.DecisionMakingBodyValues = self.__loadReferenceEntities('DecisionMakingBody');
        self.DetailReferenceValues = self.__loadReferenceEntities('DetailReference');
        self.DetailTypeValues = self.__loadReferenceEntities('DetailType');
        self.DocNameStatusValues = self.__loadReferenceEntities('DocNameStatus');
        self.EntryEventTypeValues = self.__loadReferenceEntities('EntryEventType');
        self.EntryLinkTypeValues = self.__loadReferenceEntities('EntryLinkType');
        self.ExRefTypeValues = self.__loadReferenceEntities('ExRefType');
        self.FeatureTypeValues = self.__loadReferenceEntities('FeatureType');
        self.FeatureTypeGroupValues = self.__loadReferenceEntities('FeatureTypeGroup');
        self.IDRegDocDateTypeValues = self.__loadReferenceEntities('IDRegDocDateType');
        self.IdentityFeatureLinkTypeValues = self.__loadReferenceEntities('IdentityFeatureLinkType');
        self.LegalBasisValues = self.__loadReferenceEntities('LegalBasis');
        self.LegalBasisTypeValues = self.__loadReferenceEntities('LegalBasisType');
        self.LocPartTypeValues = self.__loadReferenceEntities('LocPartType');
        self.LocPartValueStatusValues = self.__loadReferenceEntities('LocPartValueStatus');
        self.LocPartValueTypeValues = self.__loadReferenceEntities('LocPartValueType');
        self.NamePartTypeValues = self.__loadReferenceEntities('NamePartType');
        self.OrganisationValues = self.__loadReferenceEntities('Organisation');
        self.PartySubTypeValues = self.__loadReferenceEntities('PartySubType');
        self.PartyTypeValues = self.__loadReferenceEntities('PartyType');
        self.RelationQualityValues = self.__loadReferenceEntities('RelationQuality');
        self.RelationTypeValues = self.__loadReferenceEntities('RelationType');
        self.ReliabilityValues = self.__loadReferenceEntities('Reliability');
        self.SanctionsProgramValues = self.__loadReferenceEntities('SanctionsProgram');
        self.SanctionsTypeValues = self.__loadReferenceEntities('SanctionsType');
        self.ScriptValues = self.__loadReferenceEntities('Script');
        self.ScriptStatusValues = self.__loadReferenceEntities('ScriptStatus');
        self.SubsidiaryBodyValues = self.__loadReferenceEntities('SubsidiaryBody');
        self.SupInfoTypeValues = self.__loadReferenceEntities('SupInfoType');
        self.TargetTypeValues = self.__loadReferenceEntities('TargetType');
        self.ValidityValues = self.__loadReferenceEntities('Validity');

    def __loadReferenceEntities(self, selectedNodeName):

        entity_dict = {}
        namespace_tag = self.ns.keys()[0]
        xpath_str = namespace_tag+':'+selectedNodeName+'Values/'+namespace_tag+':'+selectedNodeName

        for referenceValueNode in self.node.findall(xpath_str, self.ns):
            id = referenceValueNode.attrib['ID']
            data_set = referenceValueNode.attrib
            data_set['Text'] = referenceValueNode.text
            entity_dict[id] = data_set

        return entity_dict
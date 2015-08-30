__author__ = 'e-budur'
from BlacklistEntity import *
import string

class BlacklistEntityParser():


    def __init__(self, db):
        self.db = db

    def parse(self, distinctParty):

        blacklistEntity = BlacklistEntity()
        blacklistEntity.Comment = distinctParty.Comment
        blacklistEntity.UserId = distinctParty.Profile.ID
        blacklistEntity.PartySubType = self.__readPartySubType(distinctParty)
        blacklistEntity.Names = self.__readNames(distinctParty)
        blacklistEntity.Features = self.__readFeatures(distinctParty)

        #self.__print(blacklistEntity)

        return blacklistEntity

    def __readPartySubType(self, distinctParty):
        return self.db.ReferenceValueSet.PartySubTypeValues[str(distinctParty.Profile.PartySubTypeID)]['Text']

    def __readNames(self, distinctParty):
        # we can even distinguish the type of names

        names = []
        for alias in distinctParty.Profile.Identity.Aliases:
            nameParts = []
            for name in alias.DocumentedName.Names:
                nameParts.append(name.NameText)

            name = string.join(nameParts, ' ')
            names.append(name)

        return names

    def __readFeatures(self, distinctParty):
        features = []

        for tmpFeature in distinctParty.Profile.Features:

            feature = {}

            feature['FeatureType'] = self.db.ReferenceValueSet.FeatureTypeValues[str(tmpFeature.FeatureTypeID)]['Text']
            feature['Comment'] = tmpFeature.FeatureVersion.Comment


            feature['Location'] = ""
            if str(tmpFeature.FeatureVersion.VersionLocationID) in self.db.Locations.List.keys():
                location = self.db.Locations.List[str(tmpFeature.FeatureVersion.VersionLocationID)]['Location']
                parsedLocation = self.__parseLocation(location)
                if parsedLocation == None:
                    continue

                feature['Location'] = parsedLocation

            feature['Reliability'] = self.db.ReferenceValueSet.ReliabilityValues[str(tmpFeature.FeatureVersion.ReliabilityID)]['Text']
            feature['Details'] = tmpFeature.FeatureVersion.VersionDetails;

            feature['DetailType'] = ''

            if str(tmpFeature.FeatureVersion.VersionDetailTypeID) in self.db.ReferenceValueSet.DetailTypeValues.keys():
                feature['DetailType'] = self.db.ReferenceValueSet.DetailTypeValues[str(tmpFeature.FeatureVersion.VersionDetailTypeID)]['Text']

            features.append(feature)

        return features


    def __parseLocation(self, inputLocation):

        location = {}

        areaCodes = self.db.ReferenceValueSet.AreaCodeValues

        if str(inputLocation.LocationAreaCodeId) not in areaCodes:
            return None

        areaCode = self.db.ReferenceValueSet.AreaCodeValues[str(inputLocation.LocationAreaCodeId)]
        location['Description'] = areaCode['Description']
        location['AreaCode'] = areaCode['Text']

        countryReferenceInfo = self.db.ReferenceValueSet.CountryValues[areaCode['CountryID']]
        location['CountryName'] = countryReferenceInfo['Text']
        location['CountryISO2'] = ''
        if 'ISO2' in countryReferenceInfo.keys():
            location['CountryISO2'] = countryReferenceInfo['ISO2']

        return location

    def __print(self, blacklistEntity):
        print blacklistEntity.Names, blacklistEntity.Features, blacklistEntity.PartySubType
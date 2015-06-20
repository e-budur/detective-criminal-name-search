#!/usr/bin/env python

#
# Generated Sat Jun 20 15:03:06 2015 by generateDS.py version 2.16a.
#
# Command line options:
#   ('-o', 'sdn.py')
#   ('-s', 'sdnsubs.py')
#
# Command line arguments:
#   SDN.xsd
#
# Command line:
#   generateDS.py -o "sdn.py" -s "sdnsubs.py" SDN.xsd
#
# Current working directory (os.getcwd()):
#   generateDS
#

import sys

from lxml import etree as etree_

from datamodels import sdn as supermod


def parsexml_(infile, parser=None, **kwargs):
    if parser is None:
        # Use the lxml ElementTree compatible parser so that, e.g.,
        #   we ignore comments.
        parser = etree_.ETCompatXMLParser()
    doc = etree_.parse(infile, parser=parser, **kwargs)
    return doc

#
# Globals
#

ExternalEncoding = 'ascii'

#
# Data representation classes
#


class sdnListSub(supermod.sdnList):
    def __init__(self, publshInformation=None, sdnEntry=None):
        super(sdnListSub, self).__init__(publshInformation, sdnEntry, )
supermod.sdnList.subclass = sdnListSub
# end class sdnListSub


class publshInformationTypeSub(supermod.publshInformationType):
    def __init__(self, Publish_Date=None, Record_Count=None):
        super(publshInformationTypeSub, self).__init__(Publish_Date, Record_Count, )
supermod.publshInformationType.subclass = publshInformationTypeSub
# end class publshInformationTypeSub


class sdnEntryTypeSub(supermod.sdnEntryType):
    def __init__(self, uid=None, firstName=None, lastName=None, title=None, sdnType=None, remarks=None, programList=None, idList=None, akaList=None, addressList=None, nationalityList=None, citizenshipList=None, dateOfBirthList=None, placeOfBirthList=None, vesselInfo=None):
        super(sdnEntryTypeSub, self).__init__(uid, firstName, lastName, title, sdnType, remarks, programList, idList, akaList, addressList, nationalityList, citizenshipList, dateOfBirthList, placeOfBirthList, vesselInfo, )
supermod.sdnEntryType.subclass = sdnEntryTypeSub
# end class sdnEntryTypeSub


class programListTypeSub(supermod.programListType):
    def __init__(self, program=None):
        super(programListTypeSub, self).__init__(program, )
supermod.programListType.subclass = programListTypeSub
# end class programListTypeSub


class idListTypeSub(supermod.idListType):
    def __init__(self, id=None):
        super(idListTypeSub, self).__init__(id, )
supermod.idListType.subclass = idListTypeSub
# end class idListTypeSub


class idTypeSub(supermod.idType):
    def __init__(self, uid=None, idType_member=None, idNumber=None, idCountry=None, issueDate=None, expirationDate=None):
        super(idTypeSub, self).__init__(uid, idType_member, idNumber, idCountry, issueDate, expirationDate, )
supermod.idType.subclass = idTypeSub
# end class idTypeSub


class akaListTypeSub(supermod.akaListType):
    def __init__(self, aka=None):
        super(akaListTypeSub, self).__init__(aka, )
supermod.akaListType.subclass = akaListTypeSub
# end class akaListTypeSub


class akaTypeSub(supermod.akaType):
    def __init__(self, uid=None, type_=None, category=None, lastName=None, firstName=None):
        super(akaTypeSub, self).__init__(uid, type_, category, lastName, firstName, )
supermod.akaType.subclass = akaTypeSub
# end class akaTypeSub


class addressListTypeSub(supermod.addressListType):
    def __init__(self, address=None):
        super(addressListTypeSub, self).__init__(address, )
supermod.addressListType.subclass = addressListTypeSub
# end class addressListTypeSub


class addressTypeSub(supermod.addressType):
    def __init__(self, uid=None, address1=None, address2=None, address3=None, city=None, stateOrProvince=None, postalCode=None, country=None):
        super(addressTypeSub, self).__init__(uid, address1, address2, address3, city, stateOrProvince, postalCode, country, )
supermod.addressType.subclass = addressTypeSub
# end class addressTypeSub


class nationalityListTypeSub(supermod.nationalityListType):
    def __init__(self, nationality=None):
        super(nationalityListTypeSub, self).__init__(nationality, )
supermod.nationalityListType.subclass = nationalityListTypeSub
# end class nationalityListTypeSub


class nationalityTypeSub(supermod.nationalityType):
    def __init__(self, uid=None, country=None, mainEntry=None):
        super(nationalityTypeSub, self).__init__(uid, country, mainEntry, )
supermod.nationalityType.subclass = nationalityTypeSub
# end class nationalityTypeSub


class citizenshipListTypeSub(supermod.citizenshipListType):
    def __init__(self, citizenship=None):
        super(citizenshipListTypeSub, self).__init__(citizenship, )
supermod.citizenshipListType.subclass = citizenshipListTypeSub
# end class citizenshipListTypeSub


class citizenshipTypeSub(supermod.citizenshipType):
    def __init__(self, uid=None, country=None, mainEntry=None):
        super(citizenshipTypeSub, self).__init__(uid, country, mainEntry, )
supermod.citizenshipType.subclass = citizenshipTypeSub
# end class citizenshipTypeSub


class dateOfBirthListTypeSub(supermod.dateOfBirthListType):
    def __init__(self, dateOfBirthItem=None):
        super(dateOfBirthListTypeSub, self).__init__(dateOfBirthItem, )
supermod.dateOfBirthListType.subclass = dateOfBirthListTypeSub
# end class dateOfBirthListTypeSub


class dateOfBirthItemTypeSub(supermod.dateOfBirthItemType):
    def __init__(self, uid=None, dateOfBirth=None, mainEntry=None):
        super(dateOfBirthItemTypeSub, self).__init__(uid, dateOfBirth, mainEntry, )
supermod.dateOfBirthItemType.subclass = dateOfBirthItemTypeSub
# end class dateOfBirthItemTypeSub


class placeOfBirthListTypeSub(supermod.placeOfBirthListType):
    def __init__(self, placeOfBirthItem=None):
        super(placeOfBirthListTypeSub, self).__init__(placeOfBirthItem, )
supermod.placeOfBirthListType.subclass = placeOfBirthListTypeSub
# end class placeOfBirthListTypeSub


class placeOfBirthItemTypeSub(supermod.placeOfBirthItemType):
    def __init__(self, uid=None, placeOfBirth=None, mainEntry=None):
        super(placeOfBirthItemTypeSub, self).__init__(uid, placeOfBirth, mainEntry, )
supermod.placeOfBirthItemType.subclass = placeOfBirthItemTypeSub
# end class placeOfBirthItemTypeSub


class vesselInfoTypeSub(supermod.vesselInfoType):
    def __init__(self, callSign=None, vesselType=None, vesselFlag=None, vesselOwner=None, tonnage=None, grossRegisteredTonnage=None):
        super(vesselInfoTypeSub, self).__init__(callSign, vesselType, vesselFlag, vesselOwner, tonnage, grossRegisteredTonnage, )
supermod.vesselInfoType.subclass = vesselInfoTypeSub
# end class vesselInfoTypeSub


def get_root_tag(node):
    tag = supermod.Tag_pattern_.match(node.tag).groups()[-1]
    rootClass = None
    rootClass = supermod.GDSClassesMapping.get(tag)
    if rootClass is None and hasattr(supermod, tag):
        rootClass = getattr(supermod, tag)
    return tag, rootClass


def parse(inFilename, silence=False):
    parser = None
    doc = parsexml_(inFilename, parser)
    rootNode = doc.getroot()
    rootTag, rootClass = get_root_tag(rootNode)
    if rootClass is None:
        rootTag = 'sdnList'
        rootClass = supermod.sdnList
    rootObj = rootClass.factory()
    rootObj.build(rootNode)
    # Enable Python to collect the space used by the DOM.
    doc = None
    if not silence:
        sys.stdout.write('<?xml version="1.0" ?>\n')
        rootObj.export(
            sys.stdout, 0, name_=rootTag,
            namespacedef_='xmlns:mstns="http://tempuri.org/sdnList.xsd"',
            pretty_print=True)
    return rootObj


def parseEtree(inFilename, silence=False):
    parser = None
    doc = parsexml_(inFilename, parser)
    rootNode = doc.getroot()
    rootTag, rootClass = get_root_tag(rootNode)
    if rootClass is None:
        rootTag = 'sdnList'
        rootClass = supermod.sdnList
    rootObj = rootClass.factory()
    rootObj.build(rootNode)
    # Enable Python to collect the space used by the DOM.
    doc = None
    mapping = {}
    rootElement = rootObj.to_etree(None, name_=rootTag, mapping_=mapping)
    reverse_mapping = rootObj.gds_reverse_node_mapping(mapping)
    if not silence:
        content = etree_.tostring(
            rootElement, pretty_print=True,
            xml_declaration=True, encoding="utf-8")
        sys.stdout.write(content)
        sys.stdout.write('\n')
    return rootObj, rootElement, mapping, reverse_mapping


def parseString(inString, silence=False):
    from StringIO import StringIO
    parser = None
    doc = parsexml_(StringIO(inString), parser)
    rootNode = doc.getroot()
    rootTag, rootClass = get_root_tag(rootNode)
    if rootClass is None:
        rootTag = 'sdnList'
        rootClass = supermod.sdnList
    rootObj = rootClass.factory()
    rootObj.build(rootNode)
    # Enable Python to collect the space used by the DOM.
    doc = None
    if not silence:
        sys.stdout.write('<?xml version="1.0" ?>\n')
        rootObj.export(
            sys.stdout, 0, name_=rootTag,
            namespacedef_='xmlns:mstns="http://tempuri.org/sdnList.xsd"')
    return rootObj


def parseLiteral(inFilename, silence=False):
    parser = None
    doc = parsexml_(inFilename, parser)
    rootNode = doc.getroot()
    rootTag, rootClass = get_root_tag(rootNode)
    if rootClass is None:
        rootTag = 'sdnList'
        rootClass = supermod.sdnList
    rootObj = rootClass.factory()
    rootObj.build(rootNode)
    # Enable Python to collect the space used by the DOM.
    doc = None
    if not silence:
        sys.stdout.write('#from ??? import *\n\n')
        sys.stdout.write('import ??? as model_\n\n')
        sys.stdout.write('rootObj = model_.rootClass(\n')
        rootObj.exportLiteral(sys.stdout, 0, name_=rootTag)
        sys.stdout.write(')\n')
    return rootObj


USAGE_TEXT = """
Usage: python ???.py <infilename>
"""


def usage():
    print(USAGE_TEXT)
    sys.exit(1)


def main():
    args = sys.argv[1:]
    if len(args) != 1:
        usage()
    infilename = args[0]
    parse(infilename)


if __name__ == '__main__':
    #import pdb; pdb.set_trace()
    main()

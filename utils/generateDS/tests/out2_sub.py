#!/usr/bin/env python

#
# Generated  by generateDS.py.
#
# Command line options:
#   ('--no-dates', '')
#   ('--no-versions', '')
#   ('-f', '')
#   ('-o', 'tests/out2_sup.py')
#   ('-s', 'tests/out2_sub.py')
#   ('--super', 'out2_sup')
#   ('-u', 'gends_user_methods')
#
# Command line arguments:
#   tests/people.xsd
#
# Command line:
#   generateDS.py --no-dates --no-versions -f -o "tests/out2_sup.py" -s "tests/out2_sub.py" --super="out2_sup" -u "gends_user_methods" tests/people.xsd
#
# Current working directory (os.getcwd()):
#   generateds
#

import sys
from lxml import etree as etree_

import out2_sup as supermod

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


class peopleSub(supermod.people):
    def __init__(self, comments=None, person=None, programmer=None, python_programmer=None, java_programmer=None):
        super(peopleSub, self).__init__(comments, person, programmer, python_programmer, java_programmer, )
supermod.people.subclass = peopleSub
# end class peopleSub


class commentsSub(supermod.comments):
    def __init__(self, emp=None, valueOf_=None, mixedclass_=None, content_=None):
        super(commentsSub, self).__init__(emp, valueOf_, mixedclass_, content_, )
supermod.comments.subclass = commentsSub
# end class commentsSub


class personSub(supermod.person):
    def __init__(self, vegetable=None, fruit=None, ratio=None, id=None, value=None, name=None, interest=None, category=None, agent=None, promoter=None, description=None, extensiontype_=None):
        super(personSub, self).__init__(vegetable, fruit, ratio, id, value, name, interest, category, agent, promoter, description, extensiontype_, )
supermod.person.subclass = personSub
# end class personSub


class programmerSub(supermod.programmer):
    def __init__(self, vegetable=None, fruit=None, ratio=None, id=None, value=None, name=None, interest=None, category=None, agent=None, promoter=None, description=None, language=None, area=None, attrnegint=None, attrposint=None, attrnonnegint=None, attrnonposint=None, email=None, elposint=None, elnonposint=None, elnegint=None, elnonnegint=None, eldate=None, eldatetime=None, eltoken=None, elshort=None, ellong=None, elparam=None, elarraytypes=None, extensiontype_=None):
        super(programmerSub, self).__init__(vegetable, fruit, ratio, id, value, name, interest, category, agent, promoter, description, language, area, attrnegint, attrposint, attrnonnegint, attrnonposint, email, elposint, elnonposint, elnegint, elnonnegint, eldate, eldatetime, eltoken, elshort, ellong, elparam, elarraytypes, extensiontype_, )
supermod.programmer.subclass = programmerSub
# end class programmerSub


class paramSub(supermod.param):
    def __init__(self, semantic=None, name=None, flow=None, sid=None, type_=None, id=None, valueOf_=None):
        super(paramSub, self).__init__(semantic, name, flow, sid, type_, id, valueOf_, )
supermod.param.subclass = paramSub
# end class paramSub


class python_programmerSub(supermod.python_programmer):
    def __init__(self, vegetable=None, fruit=None, ratio=None, id=None, value=None, name=None, interest=None, category=None, agent=None, promoter=None, description=None, language=None, area=None, attrnegint=None, attrposint=None, attrnonnegint=None, attrnonposint=None, email=None, elposint=None, elnonposint=None, elnegint=None, elnonnegint=None, eldate=None, eldatetime=None, eltoken=None, elshort=None, ellong=None, elparam=None, elarraytypes=None, nick_name=None, favorite_editor=None):
        super(python_programmerSub, self).__init__(vegetable, fruit, ratio, id, value, name, interest, category, agent, promoter, description, language, area, attrnegint, attrposint, attrnonnegint, attrnonposint, email, elposint, elnonposint, elnegint, elnonnegint, eldate, eldatetime, eltoken, elshort, ellong, elparam, elarraytypes, nick_name, favorite_editor, )
supermod.python_programmer.subclass = python_programmerSub
# end class python_programmerSub


class java_programmerSub(supermod.java_programmer):
    def __init__(self, vegetable=None, fruit=None, ratio=None, id=None, value=None, name=None, interest=None, category=None, agent=None, promoter=None, description=None, language=None, area=None, attrnegint=None, attrposint=None, attrnonnegint=None, attrnonposint=None, email=None, elposint=None, elnonposint=None, elnegint=None, elnonnegint=None, eldate=None, eldatetime=None, eltoken=None, elshort=None, ellong=None, elparam=None, elarraytypes=None, status=None, nick_name=None, favorite_editor=None):
        super(java_programmerSub, self).__init__(vegetable, fruit, ratio, id, value, name, interest, category, agent, promoter, description, language, area, attrnegint, attrposint, attrnonnegint, attrnonposint, email, elposint, elnonposint, elnegint, elnonnegint, eldate, eldatetime, eltoken, elshort, ellong, elparam, elarraytypes, status, nick_name, favorite_editor, )
supermod.java_programmer.subclass = java_programmerSub
# end class java_programmerSub


class agentSub(supermod.agent):
    def __init__(self, firstname=None, lastname=None, priority=None, info=None):
        super(agentSub, self).__init__(firstname, lastname, priority, info, )
supermod.agent.subclass = agentSub
# end class agentSub


class special_agentSub(supermod.special_agent):
    def __init__(self, firstname=None, lastname=None, priority=None, info=None):
        super(special_agentSub, self).__init__(firstname, lastname, priority, info, )
supermod.special_agent.subclass = special_agentSub
# end class special_agentSub


class boosterSub(supermod.booster):
    def __init__(self, firstname=None, lastname=None, other_name=None, class_=None, other_value=None, type_=None, client_handler=None):
        super(boosterSub, self).__init__(firstname, lastname, other_name, class_, other_value, type_, client_handler, )
supermod.booster.subclass = boosterSub
# end class boosterSub


class infoSub(supermod.info):
    def __init__(self, rating=None, type_=None, name=None):
        super(infoSub, self).__init__(rating, type_, name, )
supermod.info.subclass = infoSub
# end class infoSub


class client_handlerTypeSub(supermod.client_handlerType):
    def __init__(self, fullname=None, refid=None):
        super(client_handlerTypeSub, self).__init__(fullname, refid, )
supermod.client_handlerType.subclass = client_handlerTypeSub
# end class client_handlerTypeSub


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
        rootTag = 'people'
        rootClass = supermod.people
    rootObj = rootClass.factory()
    rootObj.build(rootNode)
    # Enable Python to collect the space used by the DOM.
    doc = None
    if not silence:
        sys.stdout.write('<?xml version="1.0" ?>\n')
        rootObj.export(
            sys.stdout, 0, name_=rootTag,
            namespacedef_='',
            pretty_print=True)
    return rootObj


def parseEtree(inFilename, silence=False):
    parser = None
    doc = parsexml_(inFilename, parser)
    rootNode = doc.getroot()
    rootTag, rootClass = get_root_tag(rootNode)
    if rootClass is None:
        rootTag = 'people'
        rootClass = supermod.people
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
        rootTag = 'people'
        rootClass = supermod.people
    rootObj = rootClass.factory()
    rootObj.build(rootNode)
    # Enable Python to collect the space used by the DOM.
    doc = None
    if not silence:
        sys.stdout.write('<?xml version="1.0" ?>\n')
        rootObj.export(
            sys.stdout, 0, name_=rootTag,
            namespacedef_='')
    return rootObj


def parseLiteral(inFilename, silence=False):
    parser = None
    doc = parsexml_(inFilename, parser)
    rootNode = doc.getroot()
    rootTag, rootClass = get_root_tag(rootNode)
    if rootClass is None:
        rootTag = 'people'
        rootClass = supermod.people
    rootObj = rootClass.factory()
    rootObj.build(rootNode)
    # Enable Python to collect the space used by the DOM.
    doc = None
    if not silence:
        sys.stdout.write('#from out2_sup import *\n\n')
        sys.stdout.write('import out2_sup as model_\n\n')
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

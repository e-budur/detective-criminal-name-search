import sys
import xml.etree.ElementTree as ET

from datamodels.SDN_Memory_Database  import *

#sample command line
#sdnadvanced_dataloader.py path_to_input_file.xml path_to_blacklist_entities_output_file.xml path_to_blacklist_relationships_output_file.txt
def main():
    args = sys.argv[1:]

    db = SDN_Memory_Database()
    db.load(args[0])


    f = open(args[1],'w')
    for blacklistEntity in db.getBlacklistEntities():
        print >>f, 'Names:', blacklistEntity.Names, 'Features:', blacklistEntity.Features, 'PartySubType:', blacklistEntity.PartySubType, 'Relationships:', blacklistEntity.Relationships, '\n'
    f.close()

    relationships = db.getUndirectedRelationships()
    f = open(args[2],'w')
    for userID, relatedUserIDList in relationships.iteritems():
        for relatedUserID in relatedUserIDList:
            print >>f, userID, relatedUserID
    f.close()


if __name__ == '__main__':
    main()

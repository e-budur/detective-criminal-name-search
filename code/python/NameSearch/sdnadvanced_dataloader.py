import sys
import xml.etree.ElementTree as ET

from datamodels.SDN_Memory_Database  import *

#sample command line
#sdnadvanced_dataloader.py path_to_input_file.xml path_to_output_file.xml
def main():
    args = sys.argv[1:]

    db = SDN_Memory_Database()
    db.load(args[0])
    f = open(args[1],'w')
    for blacklistEntity in db.getBlacklistEntities():
        print blacklistEntity.UserId
        __write(blacklistEntity, f)
    f.close()

def __write(blacklistEntity, f):
    print >>f, blacklistEntity.Names, blacklistEntity.Features, blacklistEntity.PartySubType, '\n'

if __name__ == '__main__':
    main()

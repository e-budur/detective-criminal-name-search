import sys
import xml.etree.ElementTree as ET

from datamodels.SDN_Memory_Database  import *

def main():
    args = sys.argv[1:]

    db = SDN_Memory_Database()
    db.load(args[0])


if __name__ == '__main__':
    main()

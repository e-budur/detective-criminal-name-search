import sys
import xml.etree.ElementTree as ET

from datamodels import SDN_Memory_Database as database

def main():
    args = sys.argv[1:]

    db = database.SDN_Memory_Database()
    db.load(args[0])


if __name__ == '__main__':
    main()

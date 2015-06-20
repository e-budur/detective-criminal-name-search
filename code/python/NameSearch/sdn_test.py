import sys

from datamodels import sdn as api

def select(sdnEntries, uids):
    sdnEntries = [i for i in sdnEntries.sdnEntry if i.uid in uids]
    people = api.sdnList()
    for sdnEntry in sdnEntries:
        people.add_sdnEntry(sdnEntry)
    people.export(sys.stdout, 0)

def printSummary(sdnEntries):
    for sdnEntry in sdnEntries.sdnEntry:
        print sdnEntry.summarize()

def writeSumaryToFile(sdnEntries, file):
    with open(file, 'w') as the_file:
        for sdnEntry in sdnEntries.sdnEntry:
            the_file.write(sdnEntry.summarize())
            the_file.write('\n')


def main():
    args = sys.argv[1:]
    print args
    if len(args) == 1:
        print 'arg ', args[0]
        sdnEntries = api.parse(args[0], silence=True)
        select(sdnEntries, [36, 39, 543, 544, 545])
        printSummary(sdnEntries)
        writeSumaryToFile(sdnEntries, 'output/sdn_names_and_aliases_data.txt')


if __name__ == '__main__':
    main()

import sys

from datamodels import sdnadvanced as api


def printProfileSummary(sdnEntries):
    for sdnEntry in sdnEntries.DistinctParties.DistinctParty:
        print sdnEntry.summarize()

def printRelationships(sdnEntries):
    for entry in sdnEntries.ProfileRelationships.ProfileRelationship:
        print entry.summarize()

def writeProfileSumaryToFile(sdnEntries, file):
    with open(file, 'w') as the_file:
        for sdnEntry in sdnEntries.DistinctParties.DistinctParty:
            the_file.write(sdnEntry.summarize())
            the_file.write('\n')

def writeRelationshipsToFile(sdnEntries, file):
    with open(file, 'w') as the_file:
        for entry in sdnEntries.ProfileRelationships.ProfileRelationship:
            the_file.write(entry.summarize())
            the_file.write('\n')

def main():
    args = sys.argv[1:]
    print args
    if len(args) == 1:
        print 'arg ', args[0]
        sdnEntries = api.parse(args[0], silence=True)
        printProfileSummary(sdnEntries)
        printRelationships(sdnEntries)
        writeRelationshipsToFile(sdnEntries, 'output/sdn_edges_data.csv')
        writeProfileSumaryToFile(sdnEntries, 'output/sdn_names_data.csv')

if __name__ == '__main__':
    main()

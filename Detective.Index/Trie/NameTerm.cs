using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective.Index.Trie
{
    public class NameTerm
    {   
        public int Id { get; set; }

        public int Tf { get; set; }

        public double Idf { get; set; }

        public int DocFrequency { get; set; }

        public int Index { get; set; }
        public string Term { get; set; }

        public HashSet<int> RecordIds = new HashSet<int>();

        public List<NameTermPosting> PostingList = new List<NameTermPosting>();

        public void AddPosting(int uid, int nameIndex, int sequenceNum)
        {
            var posting = new NameTermPosting() { Uid = uid, NameIndex = nameIndex, SequenceNum = sequenceNum };
            this.PostingList.Add(posting);
            this.Tf++;

            AddRecordId(uid);
        }

        public void AddRecordId(int recordId)
        {
            if (this.RecordIds.Contains(recordId))
                return;

            this.RecordIds.Add(recordId);

            this.DocFrequency++;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}

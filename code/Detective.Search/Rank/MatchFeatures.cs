using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detective.DataModel;
using Detective.Query.Result;

namespace Detective.Search.Rank
{
    public class MatchFeatures
    {
        public int Uid;
        public int NameIndex;
        
        public int QueryIdf;

        public double TotalInformationOfMatchedPart;
        public double MutualInformation;

        
        public double TotalInformation;

        public double MutualInformationWeight
        {
            get { return MutualInformation/TotalInformation; }
        }
        public double EditDistance;
        public double MaxDistance;

        public double LengthOfTotalMatch;
        public double LengthOfTotalExactMatch;
        public double LengthOfTotalRecord;

        public SDNItem SDNItem;
        
        public string SDNType
        {
            get { return SDNItem.sdn_type; }
        }
        public CandidateTermSet CandidateTermSet;

        public double TfIdfSimilarity
        {
            get { return MutualInformation/TotalInformation; }
        }

        public double WeightOfLengthOfTotalMatch
        {
            get
            {
                return LengthOfTotalMatch * 1.0 / LengthOfTotalRecord;
            }
        }

        public double TfIdfSimilarityOfMatchedPart
        {
            get { return MutualInformation / TotalInformationOfMatchedPart; }
        }

        public int NameTermCount { get; set; }

        public int MatchedNameTermCount
        {
            get { return CandidateTermSet.NameTerms.Count; }
        }
    }
}

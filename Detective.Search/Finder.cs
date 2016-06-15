using Detective.Api;
using Detective.Data;
using Detective.DataModel;
using Detective.Query.Result;
using Detective.Search;
using Detective.Search.Trie;
using System;
using System.Diagnostics;
using Detective.Search.Rank;
using Detective.Query.Utils;
using Detective.Search.Filter;

namespace Detective.Query
{
  public class Finder : IQuery
  {
    public QueryResult Query(string queryString)
    {
      return this.Query(new QueryRequest()
      {
        QueryString = queryString
      });
    }

    public QueryResult Query(QueryRequest request)
    {
      Stopwatch stopwatch = new Stopwatch();
      ISearch search = (ISearch) new TrieSearcher();
      stopwatch.Start();
      CandidateRecordSet candidateRecordSet = search.Search(request.QueryString);

      var filter = new FrequentMatchFilter();
      candidateRecordSet = filter.Filter(candidateRecordSet);
            
      var scorer = new DefaultRanker();
      var scoredMatches = scorer.Rank(candidateRecordSet);
      scoredMatches.ToString();
      stopwatch.Stop();
      SearchItem[] searchItemArray = scoredMatches.ToSearchItem();
      QueryResult queryResult = new QueryResult();
      queryResult.SearchItems = searchItemArray;
      queryResult.SearchItemCount = searchItemArray.Length;
      queryResult.AllNameCount = SQLDB.GetNameCount();
      queryResult.SearchTime = stopwatch.ElapsedMilliseconds.ToString() + " ms";
      double totalDays = new DateTime(2016, 6, 21).Subtract(DateTime.Now).TotalDays;
      queryResult.Message = "Powered by GT Labs ! (Countdown:" + (object) (int) totalDays + " days left)";
      queryResult.ResultCode = "OK";
      queryResult.ResultCodeExplanation = "Success";
      return queryResult;
    }

    public QueryResult GetDetails(int uid)
        {
            Stopwatch stopwatch = new Stopwatch();
           
            stopwatch.Start();
            SDNItem sdnItem = SQLDB.GetSDN(uid);
            stopwatch.Stop();
            SearchItem[] searchItemArray = { new SearchItem() { Uid = uid, SDN = sdnItem, Fullname = (sdnItem.first_name+" " +sdnItem.last_name).Trim()} };
            QueryResult queryResult = new QueryResult();
            queryResult.SearchItems = searchItemArray;
            queryResult.SearchItemCount = searchItemArray.Length;
            queryResult.AllNameCount = SQLDB.GetNameCount();
            queryResult.SearchTime = stopwatch.ElapsedMilliseconds.ToString() + " ms";
            double totalDays = new DateTime(2016, 6, 21).Subtract(DateTime.Now).TotalDays;
            queryResult.Message = "Powered by GT Labs ! (Countdown:" + (object)(int)totalDays + " days left)";
            queryResult.ResultCode = "OK";
            queryResult.ResultCodeExplanation = "Success";
            return queryResult;
        }

  }
}

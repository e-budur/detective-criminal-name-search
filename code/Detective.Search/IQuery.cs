using Detective.Api;

namespace Detective.Query
{
  public interface IQuery
  {
    QueryResult Query(string request);

    QueryResult Query(QueryRequest request);
  }
}

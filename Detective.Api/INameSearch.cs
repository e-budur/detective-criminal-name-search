using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Detective.Api
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface INameSearch
    {
        //[OperationContract]
        //QueryResult Query(QueryRequest query);

        [OperationContract]
        [WebGet(UriTemplate="search/{query}", ResponseFormat = WebMessageFormat.Json, BodyStyle =WebMessageBodyStyle.Bare)]
        QueryResult Query(string query);

        [OperationContract]
        [WebGet(UriTemplate = "details/{id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        QueryResult GetDetails(string id);
        // TODO: Add your service operations here
    }
}

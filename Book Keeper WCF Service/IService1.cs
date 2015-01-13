using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Book_Keeper_WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Book> GetBooks();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Author> getAuthors();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetBooksWithAuthors();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetBookById(int id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetAuthorById(int id);

        [OperationContract]
        string AddBook(string book);

        [OperationContract]
        string AddAuthor(string author);

        // TODO: Add your service operations here
    }
}

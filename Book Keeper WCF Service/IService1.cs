using Book_Keeper_WCF_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Book_Keeper_WCF_Service
{
    [ServiceContract]
    public interface IService1
    {
        /**
         * Combines books and authors from the database into a model object list that is returned
         *
         * @return IEnumerable<BookModel>
         */
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBooks")]
        IEnumerable<BookModel> GetBooks();

        /**
         * Selects Authors from the database into a model that is returned
         *
         * @return AuthorModel
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<AuthorModel> getAuthors();

        /**
         * Selects a single book by ID from the database combines it with authors into a model object that is returned
         *
         * @return BookModel
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        BookModel GetBookById(int id);

        /**
         * Selects a single author by ID from the database into a model object that is returned
         *
         * @return AuthorModel
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        AuthorModel GetAuthorById(int id);

        /**
         * Adds an book to the database
         *
         * @return bool
         */
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        bool AddBook(BookModel book);

        /**
         * Deletes books & authors from the database
         *
         * @return bool
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        bool DeleteBookByID(int id);

        /**
         * Edits a book and saves it to the database
         *
         * @return bool
         */
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        bool EditBook(BookModel bookin);
    }
}

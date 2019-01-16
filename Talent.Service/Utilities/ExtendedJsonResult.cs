using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Talent.Service.Utilities
{
    /// <summary>
    /// Type of operation the ExtendedJsonResult is notifying the browser about.
    /// </summary>
    public enum OperationType
    {
        View,
        Create,
        Edit,
        Delete
    }

    public class ExtendedJsonResult<T> : JsonResult
    {
        private const string _jsonRequestGetNotAllowed = "This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.";

        public bool IsSuccess { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public string Operation { get; set; }

        public int EntityId { get; set; }

        public string[] Views { get; set; }

        public new T Data { get; set; }

        public ExtendedJsonResult()
        {
            Operation = OperationType.View.ToString();
            IsSuccess = true;
            IsValid = true;
            Message = string.Empty;
            Views = null;
            Data = default(T);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //if( JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals( context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase ) )
            //{
            //	throw new InvalidOperationException( _jsonRequestGetNotAllowed );
            //}

            //var response = context.HttpContext.Response;

            //response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            //if (ContentEncoding != null)
            //{
            //    response.ContentEncoding = ContentEncoding;
            //}

            //response.Write(JsonConvert.SerializeObject(new
            //{
            //    IsSuccess,
            //    IsValid,
            //    Operation,
            //    EntityId,
            //    Message,
            //    Html = Views,
            //    Data
            //}, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss" }));

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LightData.Auth.Helper
{
    public class ExternalActionResult : JsonResult
    {
        const string JsonRequest_GetNotAllowed = "This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.";
        public ExternalActionResult(object data)
        {
            this.Data = data;
            MaxJsonLength = Int32.MaxValue;
            RecursionLimit = 100;
        }

        public int MaxJsonLength { get; set; }
        public int RecursionLimit { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(JsonRequest_GetNotAllowed);
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
               
                var camelCaseFormatter = new JsonSerializerSettings();
                camelCaseFormatter.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
                response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, camelCaseFormatter));

            }
        }
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Structure.Models.HttpResponse
{
    public class HttpResponseModel
    {
        [JsonProperty("responseCode")]
        public HttpStatusCode ResponseCode { get; set; }

        [JsonProperty("resultObject")]
        public dynamic ResultObject { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }


    }

}

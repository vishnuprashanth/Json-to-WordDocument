using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Structure.Models.HttpResponse
{
    public class GetResultObjectFromHttpResponse
    {
        //Only call this function if the errorMessage in the httpResponseModel == string.empty, otherwise something went wrong in the api call.
        //and the httpResponseModel will be null
        public static T GetResultObjectFromHttpResponseMessage<T>(dynamic responseObject)
        {
            try
            {
                if (responseObject != null)
                {
                    return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(responseObject));
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception)
            {
                return default(T);
            }

        }
    }
}

using System.Net;
using Newtonsoft.Json;

namespace PublicAPI.Services.Contracts.Entities
{
    public class ApiResponse<T>
    {
        public string Error { get; set; }
        public T ResponseContent { get; private set; }
        public HttpStatusCode Status { get; private set; }

        public ApiResponse(HttpStatusCode statusCode, string responseContent = null)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Created:
                    SetCreatedResponse(responseContent);
                    break;

                case HttpStatusCode.OK:
                    SetOkResponse(responseContent);
                    break;

                case HttpStatusCode.NotFound:
                    SetNotFoundResponse();
                    break;

                case HttpStatusCode.BadRequest:
                    SetErrorResponse(responseContent);
                    break;

                case HttpStatusCode.Unauthorized:
                    SetUnauthorisedResponse();
                    break;

                default:
                    SetUnhandledResponse();
                    break;
            }
        }

        public static ApiResponse<T> CreateErrorResponse(string jsonResult)
        {
            return new ApiResponse<T>(HttpStatusCode.BadRequest, jsonResult);
        }

        public static ApiResponse<T> CreateOKResponse(string jsonResult)
        {
            return new ApiResponse<T>(HttpStatusCode.OK, jsonResult);
        }

        public static ApiResponse<T> CreateCreatedResponse(string jsonResult)
        {
            return new ApiResponse<T>(HttpStatusCode.Created, jsonResult);
        }

        public static ApiResponse<T> CreateNotFoundResponse()
        {
            return new ApiResponse<T>(HttpStatusCode.NotFound);
        }

        private void SetErrorResponse(string jsonResult)
        {
            Error = JsonConvert.DeserializeObject<string>(jsonResult);
            Status = HttpStatusCode.BadRequest;
        }

        private void SetOkResponse(string jsonResult)
        {
            ResponseContent = JsonConvert.DeserializeObject<T>(jsonResult);
            Status = HttpStatusCode.OK;
        }

        private void SetCreatedResponse(string jsonResult)
        {
            ResponseContent = JsonConvert.DeserializeObject<T>(jsonResult);
            Status = HttpStatusCode.Created;
        }

        private void SetNotFoundResponse()
        {
            Status = HttpStatusCode.NotFound;
        }

        private void SetUnauthorisedResponse()
        {
            Status = HttpStatusCode.Unauthorized;
        }

        private void SetUnhandledResponse()
        {
            Status = HttpStatusCode.InternalServerError;
        }
    }
}
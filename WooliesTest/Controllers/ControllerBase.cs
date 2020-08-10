using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
namespace PublicAPI.Controllers
{
    public abstract class ControllerBase : ApiController
    {
        protected ResponseMessageResult GetHttpResponse<T>(Services.Contracts.Entities.ApiResponse<T> apiResponse)
        {
            switch (apiResponse.Status)
            {
                case HttpStatusCode.Created:
                    return Created(apiResponse.ResponseContent);
                case HttpStatusCode.OK:
                    return Ok(apiResponse.ResponseContent);
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.BadRequest:
                    return BadRequest(apiResponse.Error);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                default:
                    return InternalServerError();
            }
        }

        protected ResponseMessageResult BadRequest(string error)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, error));
        }

        protected ResponseMessageResult Created<T>(T data)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, data));
        }

        protected new ResponseMessageResult Ok<T>(T data)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, data));
        }

        protected new ResponseMessageResult NotFound()
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        protected new ResponseMessageResult InternalServerError()
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
        }

        protected new ResponseMessageResult Unauthorized()
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized));
        }
        protected ResponseMessageResult Forbidden(string error)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
        }
    }
}
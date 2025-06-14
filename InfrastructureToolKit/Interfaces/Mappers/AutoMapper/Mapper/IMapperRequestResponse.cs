using InfrastructureToolKit.Bases.Dtos;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper
{
    public interface IMapperRequestResponse<Request, Response>
        where Request : BaseRequest
        where Response : BaseResponse
    {
        Task<Request> ResponseToRequestAsync(Response item);
        Task<List<Request>> ResponseToRequestAsync(List<Response> item);
        Task<Response> RequestToResponseAsync(Request item);
        Task<List<Response>> RequestToResponseAsync(List<Request> item);
        Task<string> RequestToJsonAsync(Request item);
        Task<string> RequestToJsonAsync(List<Request> item);
        Task<Request> JsonToRequestAsync(string item);
        Task<List<Request>> JsonToRequestListAsync(string item);
        Task<string> ResponseToJsonAsync(Response item);
        Task<string> ResponseToJsonAsync(List<Response> item);
        Task<Response> JsonToResponseAsync(string item);
        Task<List<Response>> JsonToResponseListAsync(string item);
    }
}

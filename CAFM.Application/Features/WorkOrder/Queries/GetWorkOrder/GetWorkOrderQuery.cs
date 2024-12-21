using CAFM.Application.Responses;
using MediatR;

namespace CAFM.Application.Features.WorkOrder.Queries.GetWorkOrder
{
    public class GetWorkOrderQuery : IRequest<ApiResponse<GetWorkOrderQueryResponse>>
    {
        public GetWorkOrderQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}

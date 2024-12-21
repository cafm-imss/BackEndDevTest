using CAFM.Application.Contracts.Repos;
using CAFM.Application.Responses;
using MediatR;

namespace CAFM.Application.Features.WorkOrder.Queries.GetWorkOrder
{
    internal class GetWorkOrderQueryHandler : IRequestHandler<GetWorkOrderQuery, ApiResponse<GetWorkOrderQueryResponse>>
    {
        private readonly IWorkOrderRepo _workOrderRepo;

        public GetWorkOrderQueryHandler(IWorkOrderRepo workOrderRepo)
        {
            _workOrderRepo = workOrderRepo;
        }

        public async Task<ApiResponse<GetWorkOrderQueryResponse>> Handle(GetWorkOrderQuery request, CancellationToken cancellationToken)
        {
            var workOrder = await _workOrderRepo.GetWorkOrder(request.Id);
            if (workOrder is null)
                return ApiResponse<GetWorkOrderQueryResponse>.GetNotFoundApiResponse("Work order not found.");

            return ApiResponse<GetWorkOrderQueryResponse>.GetSuccessApiResponse(MapWorkOrderData(workOrder));
        }

        private static GetWorkOrderQueryResponse MapWorkOrderData(Domain.Entities.WorkOrder workOrder)
            => new()
            {
                Id = workOrder.Id,
                AssetId = workOrder.AssetId,
                AssetDownTime = workOrder.AssetDownTime,
                CompanyId = workOrder.CompanyId,
                CompletionDate = workOrder.CompletionDate,
                CompletionNote = workOrder.CompletionNote,
                CompletionRatio = workOrder.CompletionRatio,
                DueDate = workOrder.DueDate,
                EstimatedTime = workOrder.EstimatedTime,
                InternalNumber = workOrder.InternalNumber,
                LocationId = workOrder.LocationId,
                PriorityId = workOrder.PriorityId,
                StartDate = workOrder.StartDate,
                TaskAssignmentId = workOrder.TaskAssignmentId,
                TaskDescription = workOrder.TaskDescription,
                TaskName = workOrder.TaskName,
                TaskTypeId = workOrder.TaskTypeId
            };
    }
}

using FluentValidation;

namespace CAFM.Application.Features.WorkOrder.Commands.UpdateWorkOrderStatus
{
    public class UpdateWorkOrderStatusCommandValidator : AbstractValidator<UpdateWorkOrderStatusCommand>
    {
        public UpdateWorkOrderStatusCommandValidator()
        {
            RuleFor(a => a.WorkOrderId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(a => a.TaskStatusId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}

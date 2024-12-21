using FluentValidation;

namespace CAFM.Application.Features.WorkOrder.Queries.GetWorkOrder
{
    public class GetWorkOrderQueryValidator : AbstractValidator<GetWorkOrderQuery>
    {
        public GetWorkOrderQueryValidator()
        {
            RuleFor(a => a.Id)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Invalid Id");
        }
    }
}

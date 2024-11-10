using FluentValidation;
using PayPoint.Core.DTOs.Rooms;

namespace PayPoint.Api.Validators;

public class RoomUpdateDtoValidator : BaseValidator<RoomUpdateDto>
{
    public RoomUpdateDtoValidator()
    {
        RulesDescription(x => x.Description);
        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0.");
    }
}

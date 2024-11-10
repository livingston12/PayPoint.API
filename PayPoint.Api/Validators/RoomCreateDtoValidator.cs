using FluentValidation;
using PayPoint.Core.DTOs.Rooms;

namespace PayPoint.Api.Validators;

public class RoomCreateDtoValidator : BaseValidator<RoomCreateDto>
{
    public RoomCreateDtoValidator()
    {
        RulesName(x => x.Name);
        RulesDescription(x => x.Description);
        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0.");
    }
}

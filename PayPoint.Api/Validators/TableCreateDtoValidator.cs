using FluentValidation;
using PayPoint.Core.DTOs.Tables;

namespace PayPoint.Api.Validators;

public class TableCreateDtoValidator : BaseValidator<TableCreateDto>
{
    public TableCreateDtoValidator()
    {
        RulesName(x => x.Name);
        RulesDescription(x => x.Description);
        RuleFor(t => t.TableNumber)
            .GreaterThan(0).WithMessage("El numero de mesa debe ser mayor a 0.");
        RuleFor(t => t.Capacity)
            .NotEmpty().WithMessage("La capacidad de personas es obligatoria.")
            .GreaterThan(0).WithMessage("La capacidad de personas debe ser mayor a 0.");
        RuleFor(t => t.RoomId)
            .NotEmpty().WithMessage("La ubicacion es obligatoria.")
            .GreaterThan(0).WithMessage("La ubicacion debe ser mayor a 0.");
        RuleFor(t => t.Status)
            .IsInEnum().WithMessage("El status debe ser un valor valido.");
    }

}

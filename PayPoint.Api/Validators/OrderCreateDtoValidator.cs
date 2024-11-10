using FluentValidation;
using PayPoint.Core.DTOs.Orders;

namespace PayPoint.Api.Validators;

public class OrderCreateDtoValidator : BaseValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        GreaterThan(x => x.CustomerId, 0)
            .WithMessage("El cliente debe ser mayor a 0.")
            .NotNull().WithMessage("El cliente es obligatorio.");

        GreaterThan(x => x.TableId, 0)
            .WithMessage("La mesa debe ser mayor a 0.")
            .NotNull().WithMessage("La mesa es obligatoria.");

        GreaterThan(x => x.UserId, 0)
            .WithMessage("El usuario debe ser mayor a 0.")
            .NotNull().WithMessage("El usario es obligatorio.");

        GreaterThan(x => x.OrderStatusId, 0)
            .WithMessage("El status de la orden debe ser mayor a 0.");
    }
}

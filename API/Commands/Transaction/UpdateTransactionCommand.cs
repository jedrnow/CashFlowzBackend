using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.Infrastructure.Constants;

namespace CashFlowzBackend.API.Commands
{
    public record UpdateTransactionCommand : IRequest<bool>
    {
        public int TransactionId { get; }
        public UpdateTransactionInput UpdateTransactionInput { get; }

        public UpdateTransactionCommand(int transactionId, UpdateTransactionInput updateTransactionInput)
        {
            TransactionId = transactionId;
            UpdateTransactionInput = updateTransactionInput;
        }
    }

    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(x => x.UpdateTransactionInput)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .ChildRules(x => x.RuleFor(input => input.Date)
                        .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)))
                .ChildRules(x => x.RuleFor(input => input.Description)
                        .Length(ValidationSettings.MinDescriptionLength, ValidationSettings.MaxDescriptionLength)
                        .When(input => input.Description != null));
        }
    }
}

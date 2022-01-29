﻿namespace BlazorShop.Application.Validators.ReceiptValidator
{
    public class CreateReceiptCommandValidator : AbstractValidator<CreateReceiptCommand>
    {
        public CreateReceiptCommandValidator()
        {
            RuleFor(v => v.UserEmail)
                .MaximumLength(100).WithMessage("Maximum length exceeded")
                .NotEmpty().WithMessage("UserEmail must not be empty")
                .NotNull().WithMessage("UserEmail must not be null");

            RuleFor(v => v.ReceiptDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Date must be greater or equal than Current Date")
                .NotEmpty().WithMessage("ReceiptDate must not be empty")
                .NotNull().WithMessage("ReceiptDate must not be null");

            RuleFor(v => v.ReceiptName)
                .MaximumLength(200).WithMessage("Maximum length exceeded")
                .NotEmpty().WithMessage("ReceiptName must not be empty")
                .NotNull().WithMessage("ReceiptName must not be null");

            RuleFor(v => v.ReceiptUrl)
                .MaximumLength(500).WithMessage("Maximum length exceeded")
                .NotEmpty().WithMessage("ReceiptUrl must not be empty")
                .NotNull().WithMessage("ReceiptUrl must not be null");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Kaizen.CaseStudy.Consumer.Core.Models;

namespace Kaizen.CaseStudy.Consumer.WebAPI.Validators
{
    public class ConsumerBValidator : AbstractValidator<ConsumerB>
    {
        /// <summary>
        /// Check Validation Of Entity. We Can Develop This Area With Resource Strings To Return Error Messages In Different Languages
        /// </summary>
        public ConsumerBValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name field can not be empty")
                .MaximumLength(50)
                .WithMessage("Name field must have maximum 50 characters");

            RuleFor(c => c.Surname)
                .NotEmpty()
                .WithMessage("Surname field can not be empty")
                .MaximumLength(50)
                .WithMessage("Surname field must have maximum 50 characters");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone Number can not be empty");
        }
    }
}

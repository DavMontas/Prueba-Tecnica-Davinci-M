using core.Application.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SaveBalanceRequestValidator : AbstractValidator<SaveBalanceRequest>
{
    public SaveBalanceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MinimumLength(1);

        RuleFor(x => x.Delta)
            .Must(_ => true);
    }
}
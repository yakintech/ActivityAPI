﻿using Activity.Dto.Activity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity.Validations.Validators
{
    public class UpdateActivityRequestValidation : AbstractValidator<UpdateActivityRequestDto>
    {
        public UpdateActivityRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
        }
    }
}

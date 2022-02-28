using FluentValidation;
using SocialMedia.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastrucuture.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            // DEFINIR REGLAS DE VALIDACION

            RuleFor(post => post.Description)
                .NotNull()
                .NotEmpty()
                .Length(10, 15);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now.Date);
        }
    }
}

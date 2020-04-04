using FluentValidation;
using SimpulBlog.Core.Extensions;
using SimpulBlog.Infrastructure.Queries.UserQueries;

namespace SimpulBlog.Infrastructure.Validators.UserValidators
{
    public class AddUserQueryValidator :  AbstractValidator<AddUserQuery>
    {
        public AddUserQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty().WithMessage("Email can not be null")
                .EmailAddress().WithMessage("Email must be valid email address");

            RuleFor(u => u.Firstname)
                .NotNull()
                .NotEmpty().WithMessage("Firstname can not be empty")
                .Must(u => u.IsOnlyLetters()).WithMessage("First name must be only letters");

            RuleFor(u => u.Lastname)
                .NotNull()
                .NotEmpty().WithMessage("Firstname can not be empty")
                .Must(u => u.IsOnlyLetters()).WithMessage("First name must be only letters");
        }

    }
}

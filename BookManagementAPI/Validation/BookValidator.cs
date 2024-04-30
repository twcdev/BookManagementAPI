using BookManagementAPI.Models;
using FluentValidation;

namespace BookManagementAPI.Validation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required")
                .Length(1, 100).WithMessage("Title must be between 1 and 100 characters");

            RuleFor(book => book.Author)
                .NotEmpty().WithMessage("Author is required");

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description is required")
                .Matches(@"^<p>.*</p>$").WithMessage("Description must be in HTML format");

            RuleFor(book => book.Year)
                .InclusiveBetween(1450, 2050).WithMessage("Year must be between 1450 and 2050");

            When(book => book.BookType == BookType.Hardcopy, () =>
            {
                RuleFor(book => book.ISBN)
                    .NotEmpty().WithMessage("ISBN is required for hardcopy books")
                    .Matches(@"^[0-9]{13}$").WithMessage("ISBN must be a 13-digit number");
            });

            When(book => book.BookType == BookType.Audiobook, () =>
            {
                RuleFor(book => book.AudioURL)
                    .NotEmpty().WithMessage("Audio URL is required for audiobooks")
                    .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Invalid URL format");
            });
        }
    }
}
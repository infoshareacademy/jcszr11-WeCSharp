using FluentValidation;

namespace Schedulist.DAL.Models.Validators
{
    public class CalendarEventQueryValidator : AbstractValidator<CalendarEventQuery>
    {
        private int[] allowedPagesSizes = new[] { 5, 10, 15 };
        public CalendarEventQueryValidator() 
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPagesSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPagesSizes)}]");
                }
            });
        }
    }
}

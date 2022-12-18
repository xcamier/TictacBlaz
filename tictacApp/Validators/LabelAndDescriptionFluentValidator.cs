using FluentValidation;
using tictacApp.Helpers;
using tictacApp.Interfaces;

namespace tictacApp.Validators;

    public class LabelAndDescriptionFluentValidator<T> : AbstractValidator<T> where T: class, IIdLabel, IDescription
    {
        private int _labelMin = Constants.LabelMinLength;
        private int _labelMax = Constants.LabelShortLength;
        private int _descriptionMax = Constants.DescriptionStandardLength;

        public LabelAndDescriptionFluentValidator(int labelMin, int labelMax, int descriptionMax)
        {
            RuleFor(p => p.Label).
                            NotNull().
                            MaximumLength(labelMax).
                            MinimumLength(labelMin);

            RuleFor(p =>p.Description).MaximumLength( descriptionMax);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
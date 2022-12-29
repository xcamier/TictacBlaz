using FluentValidation;
using tictacApp.Helpers;
using tictacApp.Interfaces;

namespace tictacApp.Validators;

public class LabelFluentValidator<T> : AbstractValidator<T> where T: class, IIdLabel
{
    private int _labelMin = Constants.Label1CharLength;
    private int _labelMax = Constants.LabelShortLength;

    public LabelFluentValidator(int labelMin, int labelMax)
    {
        RuleFor(p => p.Label).
                        NotEmpty().WithMessage("Please specify a label").
                        Length(labelMin, labelMax);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
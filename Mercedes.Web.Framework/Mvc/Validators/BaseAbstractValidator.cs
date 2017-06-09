using System.ComponentModel.DataAnnotations;
using FluentValidation;



namespace Mercedes.Framework.Mvc.Validators
{
    public class BaseAbstractValidator<T> : AbstractValidator<T> where T : class
    {
    }
}

using FluentValidation;

namespace UpStart.Application.Service.Services
{
    public class BaseService
    {
        internal void Validate<T>(T obj, AbstractValidator<T> validator)
        {
            validator.ValidateAndThrow(obj);
        }
    }
}

using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Validation
{
    public class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            ValidationContext<object> context = new(entity);
            ValidationResult result = validator.Validate(context);
            if (!result.IsValid) throw new ValidationException(result.Errors);
        }
    }
}

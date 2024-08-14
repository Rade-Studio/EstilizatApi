using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.ResponseModels;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace WebApi.Helpers.Validators;

public class ValidationResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context,
        ValidationProblemDetails validationProblemDetails)
    {
        return new BadRequestObjectResult(
            new BaseResponse<IDictionary<string, string[]>>(validationProblemDetails!.Errors, "One or more validation errors occurred.", false));
    }
}
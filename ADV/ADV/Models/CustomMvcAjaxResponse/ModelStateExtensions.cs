using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADV.Models
{
    public static class ModelStateExtensions
    {
        public static MvcAjaxResponse ToYCJFMvcAjaxResponse(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return new MvcAjaxResponse();
            }

            var validationErrors = new List<ValidationErrorInfo>();

            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    validationErrors.Add(new ValidationErrorInfo(error.ErrorMessage, state.Key));
                }
            }

            var errorInfo = new ErrorInfo("ValidationError")
            {
                ValidationErrors = validationErrors.ToArray(),
                Code = 1001,
                Message = validationErrors[0].Message,
            };


            return new MvcAjaxResponse(errorInfo,false);
        }
    }
}
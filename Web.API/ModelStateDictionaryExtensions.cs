using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.API.Models;

namespace Web.API;

public static class ModelStateDictionaryExtensions
{
    /// <summary>
    /// Converts a <see cref="ModelStateDictionary"/> object to a <see cref="RequestError"/> object.
    /// </summary>
    /// <param name="modelStateDictionary">The <see cref="ModelStateDictionary"/> object to convert.</param>
    /// <returns>A <see cref="RequestError"/> object representing the converted <paramref name="modelStateDictionary"/>.</returns>
    public static RequestError ToRequestError(this ModelStateDictionary modelStateDictionary)
    {
        RequestError requestError = new();

        foreach (var keyValuePair in modelStateDictionary)
        {
            foreach (ModelError modelError in keyValuePair.Value.Errors)
            {
                requestError.AddError(new RequestErrorItem
                {
                    Key = keyValuePair.Key,
                    ErrorMessage = modelError.ErrorMessage
                });
            }
        }

        return requestError;
    }
}
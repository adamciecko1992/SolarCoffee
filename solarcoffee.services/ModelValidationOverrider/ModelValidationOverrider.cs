using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace solarcoffee.services.ModelValidationOverrider
{
    public class ModelStateOverrider
    {
        private readonly IServiceCollection services;

        public ModelStateOverrider(IServiceCollection services)
        {
            this.services = services;
        }

        public void Register()
        {
            this.services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                var errors = context.ModelState.ToDictionary(
                           //kvp => string.Join("", kvp.Key.Split('.').Select(x => x.ToCamelCase())),
                           kvp => string.Join("", kvp.Key.Split('.')),
                           kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).Select(RemoveNameFromErrorMessage).ToArray());

                    var result = new
                    {
                        Message = "Data validation error",
                        Errors = errors
                    };
                    return new UnprocessableEntityObjectResult(result);
                };
            });
        }

        private string RemoveNameFromErrorMessage(string input)
        {
            var firstQuotePosition = input.IndexOf('\'');
            var secondQuotePosition = input.IndexOf('\'', firstQuotePosition + 1);
            if (firstQuotePosition == -1 || secondQuotePosition == -1) return input;

            var msg = input.Remove(firstQuotePosition, secondQuotePosition + 1 - firstQuotePosition).Replace("  ", " ").Trim(' ', '.');
            msg = char.ToUpper(msg[0]).ToString() + msg.Substring(1);

            return msg;
        }
    }
}

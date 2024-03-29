﻿using Battle.API.Options;
using Microsoft.AspNetCore.Builder;
using System;

namespace Battle.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwaggerForBattleApplication(this IApplicationBuilder app,
            Action<SwaggerOptions> optionsBuilder)
        {
            var swaggerOptions = new SwaggerOptions();
            optionsBuilder(swaggerOptions);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });
        }
    }
}

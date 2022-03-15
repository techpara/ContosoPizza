using ContosoPizza.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
})
      //.ConfigureApiBehaviorOptions(options =>
      //{
      //    options.InvalidModelStateResponseFactory = context =>
      //        new BadRequestObjectResult(context.ModelState)
      //        {
      //            ContentTypes =
      //            {
      //               Application.Json,
      //               Application.Xml
      //            }
      //        };
      //})
      //.ConfigureApiBehaviorOptions(options =>
      //{
      //    options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
      //        "https://httpstatuses.com/404";
      //})
    .AddXmlSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger1/v1/swagger.json", "WebAPI v1"));

    //app.UseExceptionHandler("/error-development");

    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // using static System.Net.Mime.MediaTypeNames;
            context.Response.ContentType = Text.Plain;

            await context.Response.WriteAsync("An exception was thrown.");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                await context.Response.WriteAsync(" The file was not found.");
            }

            if (exceptionHandlerPathFeature?.Path == "/")
            {
                await context.Response.WriteAsync(" Page: Home.");
            }
        });
    });

    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseStatusCodePages();

//app.UseStatusCodePagesWithRedirects("/Error/Index");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

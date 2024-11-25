using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        .Where(e => e.Value != null && e.Value.Errors.Count > 0)
        .Select(e => new
        {
            Errors = e.Value != null
        ? e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
        : new string[0]
        }).ToList();

        return new BadRequestObjectResult(new
        {
            Message = "Validation failed",
            Errors = errors,
        });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Api is working");

app.MapControllers();
app.Run();


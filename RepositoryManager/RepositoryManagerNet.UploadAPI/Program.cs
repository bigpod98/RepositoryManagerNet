using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "RepositoryManagerNet.UploadAPI", Version = "v1" });
});

var app = builder.Build();
app.UsePathBase(new PathString($"/{Environment.GetEnvironmentVariable("REPONAME")}"));
// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RepositoryManagerNet.UploadAPI v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();

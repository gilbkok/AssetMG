using Microsoft.EntityFrameworkCore;
using AssetMG.Data;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Hosting;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AssetMGDbContext>(options =>
{
    options.UseSqlServer("Data Source=localhost,1433; Initial Catalog=assetmg-sqlserver-1 Integrated Security=False;User Id=sa;Password=Security89");
    //TODO: add using, trycatch
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AssetMGDbContext>();
        AssetMGInitializer.Initialize1(context);
        AssetMGInitializer.Initialize2(context);
        AssetMGInitializer.Initialize3(context);
        AssetMGInitializer.Initialize4(context);
        AssetMGInitializer.Initialize5(context); 
        AssetMGInitializer.Initialize6(context);
    }
}

app.UseHttpsRedirection();
app.UseCors("Policy1");

app.UseAuthorization();

app.MapControllers();

app.Run();

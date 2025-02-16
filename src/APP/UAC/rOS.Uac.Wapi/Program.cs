using Microsoft.AspNetCore.Builder;
using Oql.Api.Runtime;
using Oql.Runtime;
using rOS.Security.Api.Accounts;
using rOS.Security.Entity.Accounts;
using rOS.Uac.Core;
using rOS.Uac.InMemory;
using rOS.Uac.Wapi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddUserAccountService();
builder.Services.AddUserAccountStorageInMemory();

builder.Services.AddSingleton<IEntityModelProvider<IUserAccount, UserAccountModel>, EntityModelProvider<IUserAccount, UserAccountModel>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


global using Lowawa_finances_api.Models;
global using Lowawa_finances_api.Dto.TransationDto;
global using Lowawa_finances_api.Dto.UserDto;
global using AutoMapper;
global using Lowawa_finances_api.Services.TransactionServices;
global using Microsoft.EntityFrameworkCore;
global using Lowawa_finances_api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DataContext>(options
=> options.UseSqlServer(builder.Configuration.GetConnectionString("lowawa-finances-Connection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
AddJwtBearer(options=>{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

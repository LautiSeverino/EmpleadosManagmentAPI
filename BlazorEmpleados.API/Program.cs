using BlazorEmpleados.BLL;
using BlazorEmpleados.BLL.AutoMapper;
using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DAL.Data;
using BlazorEmpleados.DAL.Repositories;
using BlazorEmpleados.DAL.Repositories.Interface;
using BlazorEmpleados.DAL.UnitOfWork.UnitOfWorkFolder;
using BlazorEmpleados.DAL.UnitOfWorkFolder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configure DbContext
builder.Services.AddDbContext<EmpleadosDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});


var misReglasCors = "ReglasCors";
// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: misReglasCors, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



// Register services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(x => new UnitOfWork(x.GetRequiredService<EmpleadosDbContext>(),
    x.GetRequiredService<IEmpleadoRepository>(), x.GetRequiredService<IDepartamentoRepository>(), x.GetRequiredService<IUserRepository>()));

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Configure AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//--------------Serilog------------------------ 
var configuration = new
ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Log.Logger = new
LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction()) //cambiado a development
{
    // Use development-specific features
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();  // Uncomment this if you want to enforce HTTPS
app.UseCors(misReglasCors);
app.UseAuthentication();  // Ensure authentication is before authorization
app.UseAuthorization();

app.MapControllers();

app.Run();

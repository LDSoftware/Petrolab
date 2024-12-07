using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetroLabWebAPI.ApiConfig;
using PetroLabWebAPI.Data.DataConnection;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.Security.Claims;
using PetroLabWebAPI.Security.Config;
using PetroLabWebAPI.Security.Entity;
using PetroLabWebAPI.Services;
using PetroLabWebAPI.Services.Security.Jwt;
using PetroLabWebAPI.Services.Security.Login;
using PetroLabWebAPI.Services.Security.RoleManagment;
using PetroLabWebAPI.Services.Security.UserManagment;
using PetroLabWebAPI.ValidationRules.Commands;
using PetroLabWebAPI.ValidationRules.Rules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PetroLab API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
builder.Services.AddDbContext<SecurityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection")!));

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value!);

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IBranchService, BranchService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<ILabStudioService, LabStudioService>();
builder.Services.AddTransient<IUserLoginService, UserLoginService>();
builder.Services.AddTransient<IRoleManagmentService, RoleManagmentService>();
builder.Services.AddTransient<IUserManagmentService, UserManagmentService>();
builder.Services.AddTransient<ISecurityTokenService, SecurityTokenService>();
builder.Services.AddTransient<IIdentityClaimService, IdentityClaimService>();
builder.Services.AddTransient<ValidateNoDuplicateBranchOnUser>();
builder.Services.AddTransient<InsertNewBrachCommand>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.ConfigureApi();
app.Run();


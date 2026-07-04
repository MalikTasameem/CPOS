using CPOS.PosApi.Data;
using CPOS.PosApi.Security;
using CPOS.PosApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
const string PosCorsPolicy = "PosWebPolicy";

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(PosCorsPolicy, policy =>
    {
        string[] origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        if (origins.Length > 0)
        {
            policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
        else
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "HMAC",
        In = ParameterLocation.Header,
        Description = "أدخل التوكن الذي يرجع من api/auth/login"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddSingleton<ILegacyPasswordHasher, LegacyPasswordHasher>();
builder.Services.AddSingleton<IApiTokenService, ApiTokenService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TablesService>();
builder.Services.AddScoped<BillsService>();
builder.Services.AddScoped<PosCatalogService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(PosCorsPolicy);
app.UseMiddleware<ApiTokenMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

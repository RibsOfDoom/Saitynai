using L1_Zvejyba.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using L1_Zvejyba.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using L1_Zvejyba.Data.Auth.Model;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using L1_Zvejyba.Data.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICitiesRepository, CitiesRepository>();

builder.Services.AddTransient<IBodyRepository, BodyRepository>();

builder.Services.AddTransient<IFishRepository, FishRepository>();

//builder.Services.AddDbContext<DemoRestContext>();

builder.Services.AddDbContext<DemoRestContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

builder.Services.AddTransient<JwtTokenService>();

builder.Services.AddTransient<SessionService>();

builder.Services.AddScoped<AuthSeeder>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DemoRestContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.MapInboundClaims = false; //false
    options.TokenValidationParameters.ValidAudience = builder.Configuration["Jwt:ValidAudience"];
    options.TokenValidationParameters.ValidIssuer = builder.Configuration["Jwt:ValidIssuer"];
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
});

builder.Services.AddAuthorization();

var app = builder.Build();

using var scope = app.Services.CreateScope();

//sync
var dbContext = scope.ServiceProvider.GetRequiredService<DemoRestContext>();
dbContext.Database.Migrate();

var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthSeeder>();

await dbSeeder.SeedAsync();

app._AddAuthApi();

app.MapGet("/", () => "Hello World!");


app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
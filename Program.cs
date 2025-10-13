using L1_Zvejyba.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using L1_Zvejyba.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICitiesRepository, CitiesRepository>();

builder.Services.AddTransient<IBodyRepository, BodyRepository>();

builder.Services.AddTransient<IFishRepository, FishRepository>();

builder.Services.AddDbContext<DemoRestContext>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
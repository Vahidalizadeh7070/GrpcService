using GrpcService.Models;
using GrpcService.Services.CategoryGrpcServices;
using GrpcService.Services.CategoryService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Get ConnectionString
var conn = builder.Configuration.GetConnectionString("DbConnection");

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(db=>db.UseSqlServer(conn));

// Register Category Service
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Grpc and json transcoding
builder.Services.AddGrpc().AddJsonTranscoding();
var app = builder.Build();

app.MapGrpcService<CategoryGrpcService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

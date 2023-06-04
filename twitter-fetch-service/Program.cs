using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using twitter_fetch_service.Rabbitmq;
using twitter_fetch_service.Data;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
builder.Services.AddDbContext<AppDbContext>(opt =>
//opt.UseSqlServer("Server=localhost,1433;Initial Catalog=postsdb;User ID=sa;Password=pa55word;TrustServerCertificate=true", optionsBuilder => optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
opt.UseSqlServer("Server=mssql-clusterip-srv,1433;Initial Catalog=postsdb;User ID=sa;Password=pa55word;TrustServerCertificate=true", optionsBuilder => optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

//opt.UseInMemoryDatabase("InMemory"));


builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddHostedService<UpdateAccountConsumer>();

builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

PrepDb.PrepPopulation(app);

app.Run();

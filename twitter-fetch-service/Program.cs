using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using twitter_fetch_service.Rabbitmq;
using twitter_fetch_service.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using twitter_fetch_service.Authentication;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostWebApi", Version = "v1" });
    //First we define the security scheme
    c.AddSecurityDefinition("Bearer", //Name the security scheme
        new OpenApiSecurityScheme
        {

            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
            Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
});

builder.Services.AddDbContext<AppDbContext>(opt =>
//opt.UseSqlServer("Server=localhost,1433;Initial Catalog=postsdb;User ID=sa;Password=pa55word;TrustServerCertificate=true", optionsBuilder => optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
opt.UseSqlServer("Server=mssql-clusterip-srv,1433;Initial Catalog=postsdb;User ID=sa;Password=AAAaaa11;TrustServerCertificate=true", optionsBuilder => optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

//opt.UseInMemoryDatabase("InMemory"));


builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddHostedService<UpdateAccountConsumer>();
builder.Services.AddHostedService<DeleteAccountConsumer>();

builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ConfigureJWT(builder.Environment.IsDevelopment(), "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnjINZIWnISK2+fYGaWZswZBFzy9oPviov10sYZ+ogzFQVrjPsC+xzaZk/x1xITpILbhiHozQrMJJwpk237FE4oFnEXoIp/2f9hIb0yoeHsmTQ5lD3VmPCy8eTtzEhqZPptstcvlNg8wNtCkEIrb6qEQp/jPyegcHr5NjOXJlrzPOGCL8f8z5y6g6/BfWZP8iWLW2f2SMAl6HWTxUqdx7xPFWG71hWo6EjW3UxN+oaLYKIFKLCa3+BOygfAn7Pb5NTia70W4W5n4thSyNqbyZKa3NZgDqFUWDYRBeZdXV3clWSgIinbvmp6+bOVIWYv1fKu3bEEkeCUdd7kJMA8c7BwIDAQAB");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Console.WriteLine("NEW BUILD WOOOOOOO -----------------------");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

PrepDb.PrepPopulation(app);

app.Run();
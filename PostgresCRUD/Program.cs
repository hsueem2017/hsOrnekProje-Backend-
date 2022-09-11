using Microsoft.EntityFrameworkCore;
using PostgresCRUD.DataAccess;
using PostgresCRUD.Interfaces;
using PostgresCRUD.Services;
using PostgresCRUD.GraphQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:3000")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

// Add services to the container.

//builder.Services.AddControllers();

builder.Services
    .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

builder.Services.AddPooledDbContextFactory<postgresContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlContext")));
//builder.Services.AddScoped<IDataAccessProvider, DataAccessProvider>();
builder.Services.AddScoped<ITokenService, TokenService>();
//builder.Services.AddScoped<IMutationService, MutationService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenKey").Value)),
                       ValidateIssuer = false,

                       ValidateAudience = false,

                   };
               });

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<ListType>()
    .AddMutationType<MutationService>()
    .AddType<UploadType>()
    .AddAuthorization()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyAllowSpecificOrigins");

/*app.Use(async (context, next) =>
{
    var JWToken = context.Session.GetString("TokenKey");
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
    }
    await next();
});*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();      

app.Run();
using GraphQL.Types;
using GraphQLDemo;
using Microsoft.EntityFrameworkCore;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var optionsBuilder = new DbContextOptionsBuilder<DemoContext>()
    .UseInMemoryDatabase("test");
var _options = optionsBuilder.Options;
builder.Services.AddSingleton<DbContextOptions<DemoContext>>(_options);

//only for seeding
builder.Services.AddSingleton<DemoContext>(new DemoContext(_options));

//Add graphql schema, SelfActivatingServiceProvider->registers types
builder.Services.AddSingleton<ISchema, DemoSchema>(services 
    => new DemoSchema(new SelfActivatingServiceProvider(services)));
// register graphQL
builder.Services.AddGraphQL(options =>
{
    options.EnableMetrics = true;
})
.AddSystemTextJson();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var ctx = app.Services.GetService<DemoContext>();
ctx.Seed();

// make sure all our schemas registered to route
app.UseGraphQL<ISchema>();
app.UseGraphQLAltair();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

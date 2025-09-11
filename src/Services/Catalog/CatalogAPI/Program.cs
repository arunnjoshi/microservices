var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");
//Add Services to the DI container
builder.Services.AddCarter();
builder.Services.AddMediatR(c =>
{
	c.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(o =>
{
	o.Connection(connectionString!);
}).UseLightweightSessions();
var app = builder.Build();
//Configure the http request pipeline
app.MapCarter();

app.Run();

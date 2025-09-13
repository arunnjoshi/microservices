var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database");
var programAssembly = typeof(Program).Assembly;
//Add Services to the DI container
builder.Services.AddCarter();
builder.Services.AddMediatR(c =>
{
	c.RegisterServicesFromAssemblies(programAssembly);
	c.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(programAssembly);
builder.Services.AddMarten(o =>
{
	o.Connection(connectionString!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
//Configure the http request pipeline
app.MapCarter();
app.UseExceptionHandler(options=>{ });
app.Run();

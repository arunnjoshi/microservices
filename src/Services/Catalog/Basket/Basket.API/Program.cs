using BuildingBlocks.Behavior;
var programAssembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);
//Add Services to DI container
builder.Services.AddCarter();
builder.Services.AddMediatR(c =>
{
	c.RegisterServicesFromAssemblies(programAssembly);
	c.AddOpenBehavior(typeof(ValidationBehavior<,>));
	c.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(programAssembly);
var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();


app.Run();

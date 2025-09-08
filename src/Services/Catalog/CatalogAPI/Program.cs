var builder = WebApplication.CreateBuilder(args);

//Add Services to the DI container
builder.Services.AddCarter();
builder.Services.AddMediatR(c =>
{
	c.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
var app = builder.Build();
//Configure the http request pipeline
app.MapCarter();

app.Run();

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container.

var app = builder.Build();

//Config pipeline

app.Run();

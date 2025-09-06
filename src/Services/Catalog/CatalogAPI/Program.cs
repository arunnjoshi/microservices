var builder = WebApplication.CreateBuilder(args);

//Add Services to the DI container

var app = builder.Build();
//Configure the http request pipeline

app.Run();

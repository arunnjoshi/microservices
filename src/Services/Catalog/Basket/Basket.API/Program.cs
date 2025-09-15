var builder = WebApplication.CreateBuilder(args);
//Add Services to DI container


var app = builder.Build();

//Configure the HTTP request pipeline


app.Run();

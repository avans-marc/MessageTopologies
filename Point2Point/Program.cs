var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Adele>();
//builder.Services.AddHostedService<Lionel>();
builder.Services.AddHostedService<SomeoneSinging>();

var host = builder.Build();
host.Run();

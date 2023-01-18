using System.Net;
using Aggregator.Configurations;
using Aggregator.EventBus;
using Aggregator.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddGrpc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
 options.AddPolicy(name: "AngularPolicy",
 cfg => {
     cfg.AllowAnyHeader();
     cfg.AllowAnyMethod();
     cfg.WithOrigins(builder.Configuration["AllowedCORS"]);
 }));


builder.Services.AddSingleton<EventBusConnection>();
builder.Services.AddSingleton<RedisConnectionService>();

builder.Services.AddScoped<RedisCartRepository>();


builder.Services.AddGrpcClient<UserProto.UserProtoService.UserProtoServiceClient>(options =>
{
    options.Address = new Uri("http://userservice:5000");
});

builder.Services.AddGrpcClient<OrderProto.OrderProtoService.OrderProtoServiceClient>(options =>
{
    options.Address = new Uri("http://orderingservice:5000");
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MediatorModule()));

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 80, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });

    options.Listen(IPAddress.Any, 5000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope())
{
    var connection=scope.ServiceProvider.GetService<RedisConnectionService>();
    var redis=connection.connection.GetDatabase();
    var endpoint = connection.connection.GetEndPoints();
    var server = connection.connection.GetServer(endpoint.First());
/*
    foreach (var key in server.Keys())
    {
        redis.KeyDelete(key);
    }
*/
}

app.UseCors("AngularPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

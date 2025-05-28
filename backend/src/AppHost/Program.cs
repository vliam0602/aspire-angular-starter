using Aspire.Hosting;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Create PostgreSQL container                  ;
var username = builder.AddParameter("username", "admin");
var password = builder.AddParameter("password", "adminP@ssword");

var citusserver = builder
    .AddPostgres(name: "postgresql-usermng-assignment", userName: username, password: password)
    .WithImage("citusdata/citus:12.1")
    .WithVolume("VolumeMount.postgres.data", "/var/lib/postgresql/data")
    .WithEndpoint("tcp", (e) =>
    {
        e.Port = 15432;
        e.IsProxied = false;
    })
    //.WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var postgres = citusserver.AddDatabase("postgres");

// Register WebApi and config connection string
var userApi = builder.AddProject<Projects.UserManagement>("usermanagement-api")
                     .WaitFor(postgres)
                     .WithReference(postgres);

// Register Angular frontend
var frontend = builder.AddNpmApp("fe-user-management",
                        Path.Combine("..", "..", "..", "frontend", "usermanagement"))
                      .WaitFor(userApi)
                      .WithReference(userApi);

builder.Build().Run();

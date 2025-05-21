using Aspire.Hosting;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Create PostgreSQL container                  ;
var username = builder.AddParameter("username", "admin");
var password = builder.AddParameter("password", "adminP@ssword");

var citusserver = builder
    .AddPostgres(name: "postgresql", userName: username, password: password)
    .WithAnnotation(new ContainerImageAnnotation { Image = "citusdata/citus", Tag = "1.0" })
    .WithVolume("VolumeMount.postgres.data", "/var/lib/postgresql/data")
    .WithEndpoint("tcp", (e) =>
    {
        e.Port = 15432;
        e.IsProxied = false;
    })
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var postgres = citusserver.AddDatabase("postgres");


// Register WebApi and config connection string
var userApi = builder.AddProject<Projects.UserManagement>("usermanagement-api")
                     .WaitFor(postgres)
                     .WithReference(postgres);

builder.Build().Run();

using Aspire.Hosting;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Create PostgreSQL container                  ;
var username = builder.AddParameter("username", "admin");
var password = builder.AddParameter("password", "adminP@ssword");

var citusserver = builder
    .AddPostgres(name: "postgresql", userName: username, password: password)
    //.WithAnnotation(new ContainerImageAnnotation { Image = "citusdata/citus", Tag = "12.1" })
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

#region using default postgres image
//var postgres = builder.AddPostgres(name: "postgres", userName: username, password: password)
//                .WithImage("postgres:latest")
//                .WithVolume("pgdata", "/var/lib/postgresql/data")
//                .WithHostPort(15432)
//                //.WithEndpoint("tcp", (e) =>
//                //{
//                //    e.Port = 15432;
//                //    e.IsProxied = false;
//                //})
//                .WithLifetime(ContainerLifetime.Persistent)
//                .WithPgAdmin();
#endregion

// Register WebApi and config connection string
var userApi = builder.AddProject<Projects.UserManagement>("usermanagement-api")
                     .WaitFor(postgres)
                     .WithReference(postgres);

// Register Angular frontend
//var frontend = builder.AddNpmApp("usermanagement-frontend", "../frontend/usermanagement")
//    .WithNpmScript("start")
//    .WithEndpoint(port: 4200, scheme: "http");

builder.Build().Run();

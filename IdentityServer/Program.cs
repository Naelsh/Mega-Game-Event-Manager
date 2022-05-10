using IdentityServer.Config;

var builder = WebApplication.CreateBuilder(args);

var service = builder.Services;

service.AddIdentityServer()
    .AddInMemoryApiResources(IdConfig.apiResources)
    .AddInMemoryApiScopes(IdConfig.apiScopes)
    .AddInMemoryClients(IdConfig.clients)
    .AddInMemoryIdentityResources(IdConfig.identityResources)
    .AddTestUsers(IdConfig.testUsers)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
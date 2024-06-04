using Fina.Api;
using Fina.Api.Commom.Api;
using Fina.Api.Endpoint;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContext();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();

app.Run();

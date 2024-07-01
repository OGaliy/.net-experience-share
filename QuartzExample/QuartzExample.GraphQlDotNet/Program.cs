using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.AspNetCore;
using QuartzExample.Application.Queries;
using QuartzExample.Application.Services;
using QuartzExample.GraphQlDotNet.GraphQl;
using QuartzExample.Infrastructure;
using QuartzExample.Infrastructure.Data;
using QuartzExample.Infrastructure.Data.Repositories;
using QuartzExample.Infrastructure.Jobs;
using QuartzExample.Infrastructure.QueryHandlers;
using QuartzExample.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddResponseCompression();
builder.Services.AddDbContext<TicketDbContext>(opt =>
{
    opt.UseSqlServer("Integrated Security=SSPI;Initial Catalog=TestDb123;Data Source=OHALII-N2;TrustServerCertificate=True;");
}, ServiceLifetime.Transient);

builder.Services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddSingleton<IStreamTicketModifiedService, StreamTicketModifiedService>();
builder.Services.AddSingleton<TicketSubscription>();
builder.Services.AddSingleton<TicketQuery>();
builder.Services.AddSingleton<TicketMutation>();
builder.Services.AddSingleton<ISchema, TicketSchema>();
//builder.Services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
//builder.Services.AddSingleton<DataLoaderDocumentListener>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TicketsGetQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TicketsGetQueryHandler).Assembly));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddQuartz(q =>
{
    var jobName = typeof(TicketModifiedJob).Name;
    q.AddJob<TicketModifiedJob>(opt => opt.WithIdentity(jobName));

    var jobKey = new JobKey(jobName);
    q.AddTrigger(opts => opts
           .ForJob(jobKey)
           .WithIdentity(jobName + "-trigger")
           .WithCronSchedule("*/5 * * * * ?"));
});

builder.Services.AddQuartzServer(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddGraphQL(builder =>
{
    builder.AddGraphTypes()
        .AddSchema<TicketSchema>()
        .AddSystemTextJson()
        .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true);
}).AddWebSockets(opt => { });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true);
    });
});


builder.Services.AddControllers(opt =>
{
    opt.RespectBrowserAcceptHeader = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.UseWebSockets();
app.UseCors("CorsPolicy");
app.UseGraphQL<ISchema>("/graphql");
app.UseGraphQLPlayground(options: new GraphQL.Server.Ui.Playground.PlaygroundOptions 
{ 
    SchemaPollingEnabled = false,
    GraphQLEndPoint = "/graphql",
    SubscriptionsEndPoint = "/graphql"
});

app.Run();

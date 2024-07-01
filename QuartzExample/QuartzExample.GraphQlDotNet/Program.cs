using GraphQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
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
    opt.UseSqlServer(builder.Configuration.GetConnectionString("TicketsDB"));
}, ServiceLifetime.Transient); // Transient is used for adding ability to run multiple queries

builder.Services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddSingleton<IStreamTicketModifiedService, StreamTicketModifiedService>();
builder.Services.AddSingleton<TicketSubscription>();
builder.Services.AddSingleton<TicketQuery>();
builder.Services.AddSingleton<TicketMutation>();
builder.Services.AddSingleton<ISchema, TicketSchema>();
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
           // Run every 5 seconds
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

var app = builder.Build();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.UseWebSockets();
app.UseCors("CorsPolicy");
app.UseGraphQL<ISchema>("/graphql");
app.UseGraphQLPlayground(options: new PlaygroundOptions 
{ 
    SchemaPollingEnabled = false,
    GraphQLEndPoint = "/graphql",
    SubscriptionsEndPoint = "/graphql"
});

app.Run();

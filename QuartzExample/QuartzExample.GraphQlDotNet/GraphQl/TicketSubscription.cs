using GraphQL.Resolvers;
using GraphQL.Types;
using QuartzExample.Application.Services;
using QuartzExample.Application.ViewModels;
using QuartzExample.GraphQlDotNet.GraphQl.Types;

namespace QuartzExample.GraphQlDotNet.GraphQl;

public class TicketSubscription : ObjectGraphType
{
    public TicketSubscription(
        IStreamTicketModifiedService streamTicketModifiedService)
    {
        Name = "Subscription";

        AddField(new FieldType
        {
            Name = "ticketModified",
            Type = typeof(TicketModifiedType),
            Resolver = new FuncFieldResolver<TicketViewModel>(context =>
            {
                return context.Source as TicketViewModel;
            }),
            StreamResolver = new SourceStreamResolver<TicketViewModel>(context =>
            {
                return streamTicketModifiedService.GetMessages(DateTime.Now);
            }),
        });
    }
}

using GraphQL.Types;

namespace QuartzExample.GraphQlDotNet.GraphQl;

public class TicketSchema : Schema
{
    public TicketSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetService<TicketQuery>();
        Mutation = serviceProvider!.GetService<TicketMutation>();
        Subscription = serviceProvider.GetService<TicketSubscription>();
    }
}


using GraphQL.Types;
using GraphQLDemo.Query;

namespace GraphQLDemo
{
    public class DemoSchema : Schema
    {
        public DemoSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<ArticleQuery>();
        }
    }
}

using GraphQL.Types;
using GraphQLDemo.Domain;

namespace GraphQLDemo.Types
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType()
        {
            Field(x => x.Id).Description("The Id of the Article.");
            Field(x => x.Name).Description("The name of the Article.");
        }
    }
}

using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.Types;

namespace GraphQLDemo.Query
{
    public class ArticleQuery : ObjectGraphType
    {
        public ArticleQuery()
        {
            Field<ArticleType>(
              "articles",
              resolve: context => new Article { Id = Guid.NewGuid(), Name = "R2-D2" }
            );
        }
    }
}

using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Query
{
    public class RootMutation: ObjectGraphType
    {

        private readonly DbContextOptions<DemoContext> _options;
        public RootMutation(DbContextOptions<DemoContext> options)
        {
            _options = options;
            Field<ArticleType>(
                "addArticle",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ArticleInputType>> { Name = "article" }
                ),
                resolve: context =>
                {
                    var article = context.GetArgument<Article>("article");

                    using (var db = new DemoContext(_options))
                    {
                        db.Articles.Add(article);
                        db.SaveChanges();
                    }

                    return article;
                }
            );
        }
    }
}

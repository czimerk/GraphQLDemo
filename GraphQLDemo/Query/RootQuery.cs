using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;
using GraphQLDemo.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Query
{
    public class RootQuery : ObjectGraphType
    {
        private readonly DbContextOptions<DemoContext> _options;

        public RootQuery(DbContextOptions<DemoContext> options)
        {
            _options = options;
            Field<ListGraphType<ArticleType>>(
              "articles",
              arguments: new QueryArguments(
               new QueryArgument<StringGraphType>
               {
                   Name = "name"
               }
              ),
              resolve: context =>
              {
                  var nameFilter = context.GetArgument<string>("name");
                  using (var db = new DemoContext(_options))
                  {
                      IQueryable<Article> query = db.Articles;

                      if (!string.IsNullOrEmpty(nameFilter))
                      {
                          query = query.Where(x => x.Name.Contains(nameFilter));    
                      }
                          
                      return query.ToList();
                  }
              }
            );


            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: context =>
                {
                    using (var db = new DemoContext(_options))
                    {
                        return db.Orders.ToList();
                    }
                }
            );

            Field<ListGraphType<OrderType>>(
               "ordersWithLines",
               resolve: context =>
               {
                   //db access in a singleton way
                   //var singletonDb = context.RequestServices.GetRequiredService<DemoContext>();

                   //db connection as disposable
                   using (var db = new DemoContext(_options))
                   {
                       //tolist is a must -> else object disposed
                       return db.Orders
                        .Include(o => o.User)
                        .Include(o => o.OrderLines)
                        .ThenInclude(p => p.Article).ToList();
                   }
               }
           );

            Field<ListGraphType<OrderType>>(
               "orderLines",
               resolve: context =>
               {
                   //db access in a singleton way
                   //var singletonDb = context.RequestServices.GetRequiredService<DemoContext>();

                   //db connection as disposable
                   using (var db = new DemoContext(_options))
                   {
                       //tolist is a must -> else object disposed
                       return db.OrderLines.ToList();
                   }
               }
           );

        }
    }
}

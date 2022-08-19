using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Domain;

namespace GraphQLDemo.Types
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Price);
            Field(x => x.Unit);
            Field(x => x.Brand,true);
            Field(x => x.InventoryCode, true);
            Field(x => x.Created);

            Field(x => x.StoreId, true)
                .DeprecationReason("Will not be available after 2022.12.10");
            
            Field(x => x.Category, true);
            Field(x => x.IsActive);
            Field(x => x.ShortText, true);
            Field(x => x.LongText, true);
            Field(x => x.Type, true);
        }
    }

    public class ArticleInputType : InputObjectGraphType<Article>
    {
        public ArticleInputType()
        {
            Field(x => x.Name);
            Field(x => x.Price);
            Field(x => x.Unit);
            Field(x => x.Created);
        }
    }
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Field(x => x.Id);
            Field(x => x.Status);
            Field(x => x.OrderDate);
            Field(x => x.UpdatedDate, true);
            Field<UserType>("user", resolve: ResolveUser());
            Field<ListGraphType<OrderLineType>>("orderLines", resolve: ResolveOrderLines());
        }

        private Func<IResolveFieldContext<Order>, object> ResolveUser()
        {
            return context =>
            {
                return context.Source.User;
            };
        }

        private Func<IResolveFieldContext<Order>, object> ResolveOrderLines()
        {
            return context =>
            {
                return context.Source.OrderLines;
            };
        }
    }
    public class OrderLineType : ObjectGraphType<OrderLine>
    {
        public OrderLineType()
        {
            Field(x => x.Id);
            Field(x => x.Quantity);
            Field<ArticleType>("article", resolve: ResolveArticles());
            Field(x => x.OrderId);
        }

        private Func<IResolveFieldContext<OrderLine>, object> ResolveArticles()
        {
            return context =>
            {
                return context.Source.Article;
            };
        }
    }
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Address,true);
            Field(x => x.BirthDate);
            Field(x => x.Email, true);
            Field(x => x.RegisteredDate);
        }
    }
}

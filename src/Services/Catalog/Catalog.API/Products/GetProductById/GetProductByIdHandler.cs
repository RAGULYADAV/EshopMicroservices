using Catalog.API.Models;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    public class GetProductByIdQueryHandler(IDocumentSession session,ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation(@"GetProductByIdQueryHandler.Handle called with {@query}\", query);

            var product = session.LoadAsync<Product>(query.Id, cancellationToken);

            if(product is null)
            {
                throw InvalidCastException;
            }
        }
    }
}

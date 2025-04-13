using Catalog.API.Models;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductQuery():IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    internal class GetProductQueryHandler(IDocumentSession session,ILogger<GetProductQueryHandler> logger) : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductQueryHandler.Handle called with {@query}", query);

            var product =await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductResult(product);
        }
    }
}

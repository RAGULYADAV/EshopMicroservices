using Catalog.API.Models;



namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand Command, CancellationToken cancellationToken)
        {
            //Create Product Entity from command object
            var product = new Product
            {
                Name = Command.Name,
                Category = Command.Category,
                Description = Command.Description,
                ImageFile = Command.ImageFile,
                Price = Command.Price,
            };

            //Save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateProductResult(product.Id);
        }
    }
}

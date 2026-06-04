namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> category, string description, string imageFile, decimal Price) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool isSuccess);


    internal class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            product.Name= command.Name;
            product.Category = command.category;
            product.Description= command.description;
            product.ImageFile = command.imageFile;
            product.Price = command.Price;

            session.Update(product);

            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(isSuccess: true);
        }
    }
}

using DemoShoppe.Contract.Abstractions.Message;
using DemoShoppe.Contract.Abstractions.Shared;
using DemoShoppe.Contract.Services.Product.Request;
using DemoShoppe.Contract.Services.Product.Response;
using DemoShoppe.Domain.Abstractions;
using DemoShoppe.Domain.Abstractions.Repositories;

namespace DemoShoppe.Application.UserCase.Commands.Product
{
    public class CreateProductCommandHandler :
        ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public CreateProductCommandHandler(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductCategoryRepository productCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var findCategory = await _categoryRepository.FindById(request.categoryId, cancellationToken);
            if (findCategory is null)
                return Result.Failure<CreateProductResponse>
                    (Error.NotFound("Danh mục không tồn tại"));
            var newProduct = new Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                UserId = request.userId,
                Name = request.name,
                Price = request.price,
                Quantity = request.quantity,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "Admin@gmail.com"
            };
            _productRepository.Add(newProduct);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var newProductCategory = new Domain.Entities.ProductCategory
            {
                ProductId = newProduct.Id,
                CategoryId = request.categoryId
            };
            _productCategoryRepository.Add(newProductCategory);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var result = new CreateProductResponse(
                newProduct.Id,
                newProduct.UserId,
                newProduct.Name,
                newProduct.Description,
                newProduct.Price,
                newProduct.Quantity);
            return Result.Success(result);
        }
    }
}

using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _CategoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            Category categoryToUpdate = await _CategoryRepository.GetCategoryByIdToEdit(request.CategoryId);

            categoryToUpdate.Update(request.UpdateCategoryInput.Name);

            return (await _CategoryRepository.SaveChangesAsync());
        }
    }
}

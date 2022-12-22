using AutoMapper;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;

namespace FamilyFinance.Persistence.Services.Budget
{
    public class BudgetItemService : IBudgetItemService<BudgetItemModel>
    {
        private readonly IBudgetItemRepository _budgetItemRepository;
        private readonly IMapper _mapper;

        public BudgetItemService(IBudgetItemRepository budgetItemRepository, IMapper mapper)
        {
            if (budgetItemRepository == null || mapper == null)
            {
                throw new BadRequestException(nameof(BudgetItemService));
            }

            _budgetItemRepository = budgetItemRepository;
            _mapper = mapper;
        }

        public async Task<BudgetItemModel> GetByIdAsync(int id, CancellationToken ct)
        {
            if (id < 1)
                throw new NotFoundException(nameof(BudgetItemModel), id);

            var result = await _budgetItemRepository.GetByIdAsync(id, ct);

            return _mapper.Map<BudgetItemModel>(result);
        }

        public async Task<IEnumerable<BudgetItemModel>> ListAllAsync(CancellationToken ct)
        {
            IEnumerable<BudgetItem> entities = await _budgetItemRepository.ListAllAsync(ct);
            return _mapper.Map<List<BudgetItemModel>>(entities);
        }

        public async Task<BudgetItemModel> AddAsync(BudgetItemModel entity, CancellationToken ct)
        {
            if (entity == null)
                throw new NotFoundException(nameof(BudgetItemModel), entity);

            var entityT = _mapper.Map<BudgetItem>(entity);
            var result = await _budgetItemRepository.AddAsync(entityT, ct);

            return _mapper.Map<BudgetItemModel>(result);
        }

        public async Task<BudgetItemModel> UpdateAsync(BudgetItemModel entity, CancellationToken ct)
        {
            if (entity == null)
                throw new NotFoundException(nameof(BudgetItemModel), entity);

            var entityT = _mapper.Map<BudgetItem>(entity);
            var result = await _budgetItemRepository.UpdateAsync(entityT, ct);

            return _mapper.Map<BudgetItemModel>(entity);
        }


        public async Task DeleteAsync(BudgetItemModel entity, CancellationToken ct)
        {
            if (entity == null)
                throw new NotFoundException(nameof(BudgetItemModel), entity);

            var entityT = _mapper.Map<BudgetItem>(entity);
            await _budgetItemRepository.DeleteAsync(entityT, ct);
        }

    }
}

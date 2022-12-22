using AutoMapper;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;

namespace FamilyFinance.Persistence.Services.Budget
{
    public class UserOperationService : IUserOperationService<UserOperationModel>
    {
        private readonly IUserOperationRepository _userOperationRepository;
        private readonly IBudgetItemRepository _budgetItemRepository;
        private readonly IFinancialOperationRepository _financialOperationRepository;
        private readonly IMapper _mapper;

        public UserOperationService(IUserOperationRepository userOperationRepository, IBudgetItemRepository budgetItemRepository, IMapper mapper, IFinancialOperationRepository financialOperationRepository)
        {
            if (userOperationRepository == null || budgetItemRepository == null || mapper == null || financialOperationRepository == null)
            {
                throw new BadRequestException(nameof(UserOperationService));
            }

            _userOperationRepository = userOperationRepository;
            _budgetItemRepository = budgetItemRepository;
            _mapper = mapper;
            _financialOperationRepository = financialOperationRepository;
        }

        public async Task<UserOperationModel> GetByIdAsync(int id, CancellationToken ct)
        {
            if (id < 1)
                throw new NotFoundException(nameof(UserOperationModel), id);

            var result = await _userOperationRepository.GetByIdAsync(id, ct);

            return _mapper.Map<UserOperationModel>(result);
        }


        public async Task<IEnumerable<UserOperationModel>> ListAllAsync(CancellationToken ct)
        {
            IEnumerable<UserOperation> entities = await _userOperationRepository.ListAllAsync(ct);
            return _mapper.Map<List<UserOperationModel>>(entities);
        }

        public async Task<UserOperationModel> AddAsync(UserOperationModel entity, CancellationToken ct)
        {
            if (entity == null)
                throw new NotFoundException(nameof(UserOperationModel), entity);


            var entityT = _mapper.Map<UserOperation>(entity);

            var budgetItemResult = (int)entity.BudgetItemId > 0 ? await _budgetItemRepository.GetByIdAsync((int)entity.BudgetItemId, ct) : null;
            var finOperResult = (int)entity.FinOperationId > 0 ? await _financialOperationRepository.GetByIdAsync((int)entity.FinOperationId, ct) : null;

            entityT.BudgetItem = budgetItemResult;
            entityT.FinOperation = finOperResult;

            entityT.BudgetItemId = (int)entity.BudgetItemId > 0 ? entity.BudgetItemId : null;
            entityT.FinOperationId = (int)entity.FinOperationId > 0 ? entity.FinOperationId : null;

            entityT.Date = entity.Date.Date;
            var result = await _userOperationRepository.AddAsync(entityT, ct);

            return _mapper.Map<UserOperationModel>(result);
        }

        public async Task<UserOperationModel> UpdateAsync(UserOperationModel entity, CancellationToken ct)
        {
            if (entity == null || entity.Id == null || entity.Id < 1)
                throw new NotFoundException(nameof(UserOperationModel), entity);

            var entityT = _mapper.Map<UserOperation>(entity);

            var budgetItemResult = (int)entity.BudgetItemId > 0 ? await _budgetItemRepository.GetByIdAsync((int)entity.BudgetItemId, ct) : null;
            var finOperResult = (int)entity.FinOperationId > 0 ? await _financialOperationRepository.GetByIdAsync((int)entity.FinOperationId, ct) : null;

            entityT.BudgetItem = budgetItemResult;
            entityT.FinOperation = finOperResult;

            entityT.BudgetItemId = (int)entity.BudgetItemId > 0 ? entity.BudgetItemId : null;
            entityT.FinOperationId = (int)entity.FinOperationId > 0 ? entity.FinOperationId : null;
            entityT.Date = entity.Date.Date;

            var result = await _userOperationRepository.UpdateAsync(entityT, ct);

            return _mapper.Map<UserOperationModel>(entity);
        }


        public async Task DeleteAsync(UserOperationModel entity, CancellationToken ct)
        {
            if (entity == null || entity.Id < 1)
                throw new NotFoundException(nameof(UserOperationModel), entity);

            var entityT = _mapper.Map<UserOperation>(entity);

            var budgetItemResult = (int)entity.BudgetItemId > 0 ? await _budgetItemRepository.GetByIdAsync((int)entity.BudgetItemId, ct) : null;
            var finOperResult = (int)entity.FinOperationId > 0 ? await _financialOperationRepository.GetByIdAsync((int)entity.FinOperationId, ct) : null;

            entityT.BudgetItem = budgetItemResult;
            entityT.FinOperation = finOperResult;

            entityT.BudgetItemId = (int)entity.BudgetItemId > 0 ? entity.BudgetItemId : null;
            entityT.FinOperationId = (int)entity.FinOperationId > 0 ? entity.FinOperationId : null;
            entityT.Date = entity.Date.Date;

            await _userOperationRepository.DeleteAsync(entityT, ct);
        }

        public async Task<IEnumerable<UserOperationModel>> ListAllAsync(int userId, CancellationToken ct)
        {
            IEnumerable<UserOperation> entities = await _userOperationRepository.ListAllAsync(ct);
            var userOperations = entities.Where(x => x.UserId == userId).ToList();
            return _mapper.Map<List<UserOperationModel>>(userOperations);
        }
    }
}

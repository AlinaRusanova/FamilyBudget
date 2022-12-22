using AutoMapper;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;

namespace FamilyFinance.Persistence.Services.Budget
{
    public class FinancialOperationService : IFinancialOperationService<FinancialOperationModel>
    {
        private readonly IFinancialOperationRepository _financialOperationRepository;
        private readonly IMapper _mapper;

        public FinancialOperationService(IFinancialOperationRepository financialOperationRepository, IMapper mapper)
        {
            if (financialOperationRepository == null || mapper == null)
            {
                throw new BadRequestException(nameof(FinancialOperationService));
            }

            _financialOperationRepository = financialOperationRepository;
            _mapper = mapper;
        }

        public async Task<FinancialOperationModel> GetByIdAsync(int id, CancellationToken ct)
        {
            if (id < 1)
                throw new NotFoundException(nameof(FinancialOperationModel), id);

            var result = await _financialOperationRepository.GetByIdAsync(id, ct);

            return _mapper.Map<FinancialOperationModel>(result);
        }

        public async Task<IEnumerable<FinancialOperationModel>> ListAllAsync(CancellationToken ct)
        {
            IEnumerable<FinancialOperation> entities = await _financialOperationRepository.ListAllAsync(ct);
            return _mapper.Map<List<FinancialOperationModel>>(entities);
        }

        public async Task<FinancialOperationModel> AddAsync(FinancialOperationModel entity, CancellationToken ct)
        {
            if (entity == null)
                throw new NotFoundException(nameof(FinancialOperationModel), entity);

            var entityT = _mapper.Map<FinancialOperation>(entity);
            var result = await _financialOperationRepository.AddAsync(entityT, ct);

            return _mapper.Map<FinancialOperationModel>(result);
        }

        public async Task<FinancialOperationModel> UpdateAsync(FinancialOperationModel entity, CancellationToken ct)
        {
            if (entity == null || entity.Id == null || entity.Id < 1)
                throw new NotFoundException(nameof(FinancialOperationModel), entity);

            var entityT = _mapper.Map<FinancialOperation>(entity);
            var result = await _financialOperationRepository.UpdateAsync(entityT, ct);

            return _mapper.Map<FinancialOperationModel>(entity);
        }


        public async Task DeleteAsync(FinancialOperationModel entity, CancellationToken ct)
        {
            if (entity == null || entity.Id < 1)
                throw new NotFoundException(nameof(FinancialOperationModel), entity);

            var entityT = _mapper.Map<FinancialOperation>(entity);
            await _financialOperationRepository.DeleteAsync(entityT, ct);
        }

    }
}

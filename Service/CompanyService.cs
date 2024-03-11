﻿using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared;
namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)

        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
        {
            var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(id);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }


        public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto creationDto)
        {
            var companyEntity = _mapper.Map<Company>(creationDto);
            _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return companyToReturn;
        }
        public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var companiesEntities = await _repository.Company.GetByIDsAsync(ids, trackChanges);

            if (ids.Count() != companiesEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companiesEntities);
            return companiesToReturn;
        }
        public async Task<(IEnumerable<CompanyDto> companyDtos, string ids)> CreateCompanyCollectionAsync
         (IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();
            var companyCollectionEntity = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var companyEntity in companyCollectionEntity)
                _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();
            var companyToReturnCollection = _mapper.Map<IEnumerable<CompanyDto>>(companyCollectionEntity);
            var ids = string.Join(",", companyToReturnCollection.Select(c => c.Id));
            return (companies: companyToReturnCollection, ids: ids);
        }
        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            _repository.Company.DeleteCompany(company);
            _repository.SaveAsync();
        }
        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges)
        {
            var companyEntity = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);
            _mapper.Map(companyForUpdate, companyEntity);
            _repository.SaveAsync();
        }


    }
}

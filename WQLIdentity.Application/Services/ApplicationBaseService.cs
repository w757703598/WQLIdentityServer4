using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WQLIdentity.Application.Interfaces;
using WQLIdentity.Domain.Entities;
using WQLIdentity.Domain.Interface;
using WQLIdentityServer.Infra.Dto;
using WQLIdentityServer.Infra.Extensions;

namespace WQLIdentity.Application.Services
{
    public class ApplicationBaseService<TEntity>:IApplicationBaseService<TEntity> where TEntity: Entity
    {
        protected readonly IApplicationRepository<TEntity>  _repository;
        private IMapper _mapper;
        public ApplicationBaseService(IApplicationRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual bool Add(TEntity obj)
        {
            _repository.Add(obj);
            return _repository.SaveChanges() > 0;
        }

        public virtual TEntity GetById<T>(T id)
        {
            return _repository.GetById(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }
        public Pagelist<TEntity> GetAll(PageInputDto input)
        {
            var pageresult = _repository.GetAll().PageBy(input, d => d.Id);
            var result = _mapper.Map<Pagelist<TEntity>>(pageresult);
            return result;
        }
        public virtual bool Update(TEntity obj)
        {
            _repository.Update(obj);
            return _repository.SaveChanges() > 0;
        }

        public virtual bool Remove<T>(T id)
        {

            var claims = _repository.GetById(id);
            _repository.Remove(claims);
            return _repository.SaveChanges() > 0;
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public async Task<bool> AddAsync(TEntity obj)
        {
            await _repository.AddAsync(obj);
            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<TEntity> GetByIdAsync<T>(T id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

  
    }
}

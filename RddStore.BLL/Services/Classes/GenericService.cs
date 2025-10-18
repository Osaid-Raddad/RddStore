using Azure.Core;
using Mapster;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Classes;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {

        private readonly IGenericRepository<TEntity> _igenericRepository;

        public GenericService(IGenericRepository<TEntity> igenericRepository)
        {
            _igenericRepository = igenericRepository;
        }


        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _igenericRepository.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _igenericRepository.GetById(id);
            if (entity == null)
            {
                return 0;
            }
            return _igenericRepository.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll()
        {
            var entity = _igenericRepository.GetAll();
            return entity.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
            var entity = _igenericRepository.GetById(id);
            return entity == null ? default: entity.Adapt<TResponse>();
        }

        public bool ToogleStatus(int id)
        {
            var entity = _igenericRepository.GetById(id);
            if (entity == null)
            {
                return false;
            }
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            _igenericRepository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _igenericRepository.GetById(id);
            if (entity == null)
            {
                return 0;
            }
            var updatedEntity = request.Adapt(entity);
            return _igenericRepository.Update(updatedEntity);
        }
    }
}

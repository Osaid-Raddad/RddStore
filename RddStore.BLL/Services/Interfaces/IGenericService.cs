using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Interfaces
{
    public interface IGenericService <TRequest, TResponse,TEntity> where TEntity : BaseModel
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll(bool onlyActive = false);
        TResponse? GetById(int id, bool isAdmin=false);
        int Update(int id, TRequest request);
        int Delete(int id);
        public bool ToogleStatus(int id);
    }
}

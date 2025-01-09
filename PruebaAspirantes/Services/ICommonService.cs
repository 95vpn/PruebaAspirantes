using Microsoft.AspNetCore.Mvc;
using PruebaAspirantes.DTOs;

namespace PruebaAspirantes.Services
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI personaInsertDto);
        Task<T> Update(int id, TU personaUpdateDto);
        Task<T> Delete(int id);
    }
}

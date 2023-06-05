using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Services.DI.Abstract
{
    public interface IDogService
    {
        List<DogDTO> GetAllDogs();
        List<DogDTO> GetAllDogs(string attribute, string order);
        List<DogDTO> GetAllDogs(string attribute, string order, int pageNumber, int pageSize);
        Task<DogDTO?> GetDogByNameAsync(string name);
        Task AddDogAsync(DogDTO dog);
        Task UpdateDogAsync(string name, DogUpdateDTO dog);
        Task<bool> DeleteDogAsync(string name);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Models;

namespace BLL.Services.DI.Abstract
{
    public interface IDogService
    {
        List<DogDTO> GetAllDogs();
        List<DogDTO> GetAllDogs(string attribute, string order);
        List<DogDTO> GetAllDogs(string attribute, string order, int pageNumber, int pageSize);
        Task<DogDTO?> GetDogByNameAsync(string name);
        Task<Dog> AddDogAsync(DogDTO dog);
        Task<Dog> UpdateDogAsync(string name, DogUpdateDTO dog);
        Task<bool> DeleteDogAsync(string name);
    }
}

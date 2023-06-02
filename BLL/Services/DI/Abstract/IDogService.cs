using BLL.DTO;

namespace BLL.Services.DI.Abstract
{
    public interface IDogService
    {
        List<DogDTO> GetAllDogs();
        Task<DogDTO?> GetDogByNameAsync(string name);
        Task AddDogAsync(DogDTO dog);
        Task UpdateDogAsync(string name, DogUpdateDTO dog);
        Task<bool> DeleteDogAsync(string name);
    }
}

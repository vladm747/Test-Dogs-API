using System.Reflection;
using AutoMapper;
using BLL.DTO;
using BLL.Services.DI.Abstract;
using DAL.Infrastructure.DI.Abstract;
using DAL.Models;

namespace BLL.Services.DI.Implementation
{
    public class DogService : IDogService
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;
        public DogService(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }
        public List<DogDTO> GetAllDogs()
        {
            return _mapper.Map<IEnumerable<DogDTO>>(_dogRepository.GetAll()).ToList();
        }
        public List<DogDTO> GetAllDogs(string attribute, string? order)
        {
            var dogs = _dogRepository.GetAll();

            dogs = CustomSortByAttribute(dogs, attribute, order);

            return _mapper.Map<IEnumerable<DogDTO>>(dogs).ToList();
        }
        public List<DogDTO> GetAllDogs(string attribute, string order, int pageNumber, int pageSize)
        {
            var dogs = CustomSortByAttribute(_dogRepository.GetAll(), attribute, order).ToList();

            if (pageNumber * pageSize > dogs.Count || pageNumber < 1 || pageSize < 1)
                throw new InvalidOperationException($"Arguments pageNumber = {pageNumber}; pageSize = {pageSize}  are not appropriate");

            var dogsPaginated = dogs.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return _mapper.Map<IEnumerable<DogDTO>>(dogsPaginated).ToList();
        }

        private IEnumerable<Dog> CustomSortByAttribute(IEnumerable<Dog> dogs, string attribute, string? order)
        {
            PropertyInfo? property = typeof(Dog).GetProperty(char.ToUpper(attribute[0]) + attribute[1..]);
            if (property == null)
                throw new InvalidOperationException($"Cannot find property {char.ToUpper(attribute[0]) + attribute[1..]}");

            if (order == null || order.ToLower() == "asc")
                dogs = dogs.OrderBy(dog => property.GetValue(dog, null)).ToList();
            else if (order.ToLower() == "desc")
                dogs = dogs.OrderByDescending(dog => property.GetValue(dog, null)).ToList();
            return dogs;
        }

        public async Task<DogDTO?> GetDogByNameAsync(string name)
        {
            Dog? dog = await _dogRepository.GetByKeyAsync(name ?? throw new ArgumentNullException(nameof(name), "Name must be not null!"));
            if (dog == null)
                throw new KeyNotFoundException($"Dog with name {name} doesn't exist in database!");
            return _mapper.Map<DogDTO>(dog);
        }

        public async Task<Dog> AddDogAsync(DogDTO dog)
        {
            if (dog == null)
                throw new ArgumentNullException(nameof(dog), "The Dog you are trying to add is null!");

            Dog item = _mapper.Map<Dog>(dog);

            var dogToCheck = await _dogRepository.GetByKeyAsync(item.Name);

            if (dogToCheck != null)
                throw new InvalidOperationException($"Dog with name {dogToCheck.Name} already exists in database!");

            await _dogRepository.AddAsync(item);

            return item;
        }

        public async Task<Dog> UpdateDogAsync(string name, DogUpdateDTO dog)
        {
            Dog? item = await _dogRepository.GetByKeyAsync(name);

            if (item == null)
                throw new KeyNotFoundException($"Dog with name {name} doesn't exist in database!");

            _mapper.Map(dog, item);

            await _dogRepository.UpdateAsync(item);

            return item;
        }

        public async Task<bool> DeleteDogAsync(string name)
        {
            Dog? item = await _dogRepository.GetByKeyAsync(name);

            if (item == null)
                throw new KeyNotFoundException($"Dog with name {name} doesn't exist in database!");

            return await _dogRepository.DeleteAsync(item) > 0;
        }
    }
}

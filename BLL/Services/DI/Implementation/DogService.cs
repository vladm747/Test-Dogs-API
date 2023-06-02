using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Services.DI.Abstract;
using DAL.Infrastructure.DI.Abstract;
using DAL.Models;

namespace BLL.Services.DI.Implementation
{
    public class DogService: IDogService
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

        public async Task<DogDTO?> GetDogByNameAsync(string name)
        {
            Dog? dog = await _dogRepository.GetByKeyAsync(name ?? throw new ArgumentNullException(nameof(name), "Name must be not null!"));
            if (dog == null)
                throw new KeyNotFoundException($"Dog with name {name} doesn't exist in database!");
            return dog != null ? _mapper.Map<DogDTO>(dog) : null;
        }

        public async Task AddDogAsync(DogDTO dog)
        {
            if (dog == null)
                throw new ArgumentNullException(nameof(dog), "The Dog you are trying to add is null!");
        
            Dog item = _mapper.Map<Dog>(dog);

            if (_dogRepository.GetByKeyAsync(item.Name) != null)
                throw new ArgumentException("Dog already exists in database!", nameof(item));

            await _dogRepository.AddAsync(item);
        }

        public async Task UpdateDogAsync(string name, DogUpdateDTO dog)
        {
            Dog? item = await _dogRepository.GetByKeyAsync(name);

            if (item == null)
                throw new KeyNotFoundException($"Dog with name {name} doesn't exist in database!");

            _mapper.Map(dog, item);

            await _dogRepository.UpdateAsync(item);
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

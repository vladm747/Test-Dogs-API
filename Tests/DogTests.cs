using AutoMapper;
using BLL.AutoMapperProfile;
using BLL.DTO;
using BLL.Services.DI.Abstract;
using BLL.Services.DI.Implementation;
using DAL.Infrastructure.DI.Abstract;
using DAL.Models;
using DogsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;

namespace Tests
{
    public class DogTests
    {
        private readonly DogsController _controller;
        private readonly Mock<IDogRepository> _repository;
        private readonly IDogService _service;
        public DogTests()
        {
            _repository = new Mock<IDogRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<DogProfile>()).CreateMapper();
            _service = new DogService(_repository.Object, mapper);
            _controller = new DogsController(_service);
        }

        [SetUp]
        public void Setup()
        {
            //arrange
            _repository.Setup(repo => repo.GetAll()).Returns(new List<Dog>()
            {
                new Dog
                {
                    Name = "Barsik",
                    Color = "brown",
                    TailLength = 25,
                    Weight = 60
                },
                new Dog
                {
                    Name = "Friend",
                    Color = "white",
                    TailLength = 14,
                    Weight = 21
                }
            });
        }

        [Test]
        public void GetDogs_ReturnsDogDTOList()
        {
            var result = _controller.GetDogs() as OkObjectResult;
            List<DogDTO> dogs = result?.Value as List<DogDTO> ?? new List<DogDTO>();

            Assert.IsInstanceOf<List<DogDTO>>(dogs);
            Assert.That(dogs.Count, Is.EqualTo(2));
            Assert.That(dogs[0].Name, Is.EqualTo("Barsik"));
            Assert.That(dogs[0].Color, Is.EqualTo("brown"));
            Assert.That(dogs[0].TailLength, Is.EqualTo(25));
            Assert.That(dogs[0].Weight, Is.EqualTo(60));
            Assert.That(dogs[1].Name, Is.EqualTo("Friend"));
            Assert.That(dogs[1].Color, Is.EqualTo("white"));
            Assert.That(dogs[1].TailLength, Is.EqualTo(14));
            Assert.That(dogs[1].Weight, Is.EqualTo(21));
        }
        [Test]
        public void GetDogs_Weigth_ReturnsDogDTOList()
        {
            var result = _controller.GetDogs("weight") as OkObjectResult;
            List<DogDTO> dogs = result?.Value as List<DogDTO> ?? new List<DogDTO>();

            Assert.IsInstanceOf<List<DogDTO>>(dogs);
            Assert.That(dogs.Count, Is.EqualTo(2));
            Assert.That(dogs[1].Name, Is.EqualTo("Barsik"));
            Assert.That(dogs[1].Color, Is.EqualTo("brown"));
            Assert.That(dogs[1].TailLength, Is.EqualTo(25));
            Assert.That(dogs[1].Weight, Is.EqualTo(60));
            Assert.That(dogs[0].Name, Is.EqualTo("Friend"));
            Assert.That(dogs[0].Color, Is.EqualTo("white"));
            Assert.That(dogs[0].TailLength, Is.EqualTo(14));
            Assert.That(dogs[0].Weight, Is.EqualTo(21));
        }
        [Test]
        public void GetDogs_TailLengthDescOrder_ReturnsDogDTOList()
        {
            var result = _controller.GetDogs("tailLength", "desc") as OkObjectResult;
            List<DogDTO> dogs = result?.Value as List<DogDTO> ?? new List<DogDTO>();

            Assert.IsInstanceOf<List<DogDTO>>(dogs);
            Assert.That(dogs.Count, Is.EqualTo(2));
            Assert.That(dogs[0].Name, Is.EqualTo("Barsik"));
            Assert.That(dogs[0].Color, Is.EqualTo("brown"));
            Assert.That(dogs[0].TailLength, Is.EqualTo(25));
            Assert.That(dogs[0].Weight, Is.EqualTo(60));
            Assert.That(dogs[1].Name, Is.EqualTo("Friend"));
            Assert.That(dogs[1].Color, Is.EqualTo("white"));
            Assert.That(dogs[1].TailLength, Is.EqualTo(14));
            Assert.That(dogs[1].Weight, Is.EqualTo(21));
        }
        [Test]
        public void GetDogs_PageNumberPageSize_ReturnsDogDTOList()
        {
            var result = _controller.GetDogs(null, null, 2, 1) as OkObjectResult;
            List<DogDTO> dogs = result?.Value as List<DogDTO> ?? new List<DogDTO>();

            Assert.IsInstanceOf<List<DogDTO>>(dogs);
            Assert.That(dogs.Count, Is.EqualTo(1));
            Assert.That(dogs[0].Name, Is.EqualTo("Friend"));
            Assert.That(dogs[0].Color, Is.EqualTo("white"));
            Assert.That(dogs[0].TailLength, Is.EqualTo(14));
            Assert.That(dogs[0].Weight, Is.EqualTo(21));
        }
        [Test]
        public void GetAllDogs_InappropriatePageNumberPageSize_ThrowInvalidOperationException()
        {
            try
            {
                var res = _service.GetAllDogs("name", "desc", 2, 2);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
                Assert.Pass();
            }
        }
        [Test]
        public async Task AddDog_NewDog_ReturnsNewDog()
        {
            var dogToAdd = new DogDTO() { Name = "Kit", Color = "yellow", TailLength = 18, Weight = 32 };
            
            var newDog = await _controller.AddDog(dogToAdd) as CreatedAtActionResult;
            var dog = newDog?.Value as Dog;
           
            Assert.That(dog.Name, Is.EqualTo("Kit"));
            Assert.That(dog.Color, Is.EqualTo("yellow"));
            Assert.That(dog.TailLength, Is.EqualTo(18));
            Assert.That(dog.Weight, Is.EqualTo(32));
        }
        [Test]
        public async Task AddDog_ExistingDog_ReturnsException()
        {
            var dogToAdd = new DogDTO() { Color = "yellow", TailLength = 18, Weight = 32 };
            try
            {
                var response = await _controller.AddDog(dogToAdd) as CreatedAtActionResult;

            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}
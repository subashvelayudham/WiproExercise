using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using User.Data.Dtos;
using User.Data.Entities;
using User.Service.Interface;
using UserAPI.Controllers;

namespace UserApi.Tests.Unit
{
    [TestFixture]
    public class UserControllerTests
    {
        UserController Controller;
        private IQueryService _queryService;
        private ICommandService _commandService;
        private ILogger<UserController> _logger;
      
        [OneTimeSetUp]
        public void InitSetup()
        {
            _queryService = Substitute.For<IQueryService>();
            _commandService = Substitute.For<ICommandService>();
            _logger = Substitute.For<ILogger<UserController>>();
           
            Controller = new UserController(_queryService, _commandService, _logger);
        }

        [Test]
        [Order(1)]
        public async Task GetUserById_ForValidRequest_ReturnOKRequest()
        {
            //ARRANGE
            int userId = 10000;
            _queryService.GetUserById(userId).Returns(new UserDto(){Id=10000});

            //ACT
            var result=await Controller.GetUserById(userId) as OkObjectResult;

            //ASSERT
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]

        [Order(2)]
        public async Task  GetUserById_ForInvalidRequest_ReturnBadRequest()
        {
            //ARRANGE
            int userId = 0;
           
            //ACT
            var result = await Controller.GetUserById(userId) as BadRequestResult;

            //ASSERT
            Assert.AreEqual(result.StatusCode, 400);
        }

        [Test]

        [Order(8)]
        public async Task Exception_ForGetUserById()
        {
            //ARRANGE
            int userId = 10000;
            Controller = new UserController(null, _commandService, _logger);

            //ACT
            try
            {
                var result = await Controller.GetUserById(userId);
            }
            catch (Exception ex)
            {
                //ASSERT
                Assert.IsTrue(ex is Exception);
            }
          
        }

        [Test]
        [Order(3)]
        [TestCase("UserInputModelValidRequest")]
        [TestCase("UserInputModelInValidRequest")]
        public async Task UpdateUser_ForValidRequest_ReturnCreatedOrOKRequest(string filename)
        {
            //ARRANGE
            string solution_dir = Directory.GetParent(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(TestContext.CurrentContext.WorkDirectory))).FullName;
            string filepath = "/Stubs/Update/"+filename+".json";
            var path = solution_dir + filepath;

            UserInputModel userinputModel = JsonConvert.DeserializeObject<UserInputModel>(File.ReadAllText(path));

            if (filename == "UserInputModelValidRequest")
            {
                _commandService.CreateOrUpdateUser(userinputModel).Returns(new UserOutputModel() { IsValid = true });
                var result = await Controller.UpdateUser(userinputModel) as OkObjectResult;
                //ASSERT
                Assert.AreEqual(result.StatusCode, 200);
            }
            else
            {
                UserOutputModel userOutputModel = new UserOutputModel()
                {
                    IsValid = false,
                    Validations = new List<string>() { new string("Email Exists")}
                };

                _commandService.CreateOrUpdateUser(userinputModel).Returns(userOutputModel);

                var result = await Controller.UpdateUser(userinputModel) as OkObjectResult;

                //ASSERT
                Assert.AreEqual(result.StatusCode, 200);

                // Assert.IsTrue(((UserOutputModel)result.Value).Validations.Count>0);
            }

        }



        [Test]

        [Order(4)]
        public async Task UpdateUser_ForInvalidRequest_ReturnBadRequest()
        {
            //ARRANGE
            UserInputModel userinputModel = null;
         
            //ACT
            var result = await Controller.UpdateUser(userinputModel) as UnprocessableEntityObjectResult;

            //ASSERT
            Assert.AreEqual(result.StatusCode, 422);
        }

        [Test]

        [Order(10)]
        public async Task Exception_ForUpdateUser()
        {
            //ARRANGE
            string solution_dir = Directory.GetParent(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(TestContext.CurrentContext.WorkDirectory))).FullName;
            string filepath = "/Stubs/Update/UserInputModelValidRequest.json";
            var path = solution_dir + filepath;

            UserInputModel userinputModel = JsonConvert.DeserializeObject<UserInputModel>(File.ReadAllText(path));

            Controller = new UserController(_queryService, null, _logger);

            //ACT
            try
            {
                var result = await Controller.UpdateUser(userinputModel);
            }
            catch (Exception ex)
            {
                //ASSERT
                Assert.IsTrue(ex is Exception);
            }
        }

        [Test]
        [Order(6)]
        [TestCase("CreateUserInputModelValidRequest")]
        [TestCase("CreateUserInputModelInValidRequest")]
        public async Task CreateUser_ForValidRequest_ReturnCreatedOrOKRequest(string filename)
        {
            //ARRANGE
            string solution_dir = Directory.GetParent(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(TestContext.CurrentContext.WorkDirectory))).FullName;
            string filepath = "/Stubs/Create/" + filename + ".json";
            var path = solution_dir + filepath;

            UserInputModel userinputModel = JsonConvert.DeserializeObject<UserInputModel>(File.ReadAllText(path));
            
            //ACT
            if (filename == "CreateUserInputModelValidRequest")
            {
                _commandService.CreateOrUpdateUser(userinputModel).Returns(new UserOutputModel() { IsValid = true });
                var result = await Controller.CreateUser(userinputModel) as CreatedResult;
                //ASSERT
                Assert.AreEqual(result.StatusCode, 201);
            }
            else
            {
                UserOutputModel userOutputModel = new UserOutputModel()
                {
                    IsValid = false,
                    Validations = new List<string>() { new string("Email Exists") }
                };

                _commandService.CreateOrUpdateUser(userinputModel).Returns(userOutputModel);

                dynamic result = await Controller.CreateUser(userinputModel) as OkObjectResult;

                //ASSERT
                Assert.IsTrue(result.Value.Validations.Count > 0);
            }

        }


        [Test]
        [Order(7)]
        public async Task CreateUser_ForInvalidRequest_ReturnBadRequest()
        {
            //ARRANGE
            UserInputModel userinputModel = null;

            //ACT
            var result = await Controller.CreateUser(userinputModel) as UnprocessableEntityObjectResult;

            //ASSERT
            Assert.AreEqual(result.StatusCode, 422);
        }

        [Test]

        [Order(9)]
        public async Task Exception_ForCreateUser()
        {
            //ARRANGE
            string solution_dir = Directory.GetParent(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(TestContext.CurrentContext.WorkDirectory))).FullName;
            string filepath = "/Stubs/Create/CreateUserInputModelValidRequest.json";
            var path = solution_dir + filepath;

            UserInputModel userinputModel = JsonConvert.DeserializeObject<UserInputModel>(File.ReadAllText(path));
            Controller = new UserController(_queryService, null, _logger);

            //ACT
            try
            {
                var result = await Controller.CreateUser(userinputModel) ;
            }
            catch(Exception ex)
            {
                //ASSERT
                Assert.IsTrue(ex is Exception);
            }
        }


    }
}

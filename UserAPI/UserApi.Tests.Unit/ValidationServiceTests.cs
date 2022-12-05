using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using User.Data.Entities;
using User.Service;
using User.Service.Interface;

namespace UserApi.Tests.Unit
{
    [TestFixture]
    public class ValidationServiceTests
    {
        private IValidationService ValidationService;
        private ILogger<ValidationService> _logger;

        [OneTimeSetUp]
        public void InitSetup()
        {
            _logger = Substitute.For<ILogger<ValidationService>>();
            ValidationService = new ValidationService( _logger);
        }

        [Test]
        public async Task Verify_ValidateUser()
        {
            //ARRANGE
            string solution_dir = Directory.GetParent(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(TestContext.CurrentContext.WorkDirectory))).FullName;
            string filepath = "/Stubs/Create/CreateUserInputModelInValidRequest.json";
            var path = solution_dir + filepath;

            UserInputModel userInputModel = JsonConvert.DeserializeObject<UserInputModel>(File.ReadAllText(path));
            ValidationEntity validationEntity = new ValidationEntity();
            validationEntity.IsUniqueEmail = false;

            //ACT
            var result = await ValidationService.ValidateUser(userInputModel, validationEntity);

            //ASSERT
            Assert.IsTrue(result.IsValid == false);
            Assert.IsTrue(result.Errorlist.Count>0);

        }
    }
}

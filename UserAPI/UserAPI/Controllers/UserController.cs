using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data.Models;
using User.Data.Context;
using System.Web.Http.Results;
using System.Net.Http;
using User.Service.Interface;
using User.Data.Dtos;
using User.Repository.Interfaces;
using User.Data.Entities;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IQueryService _queryService;
        private readonly ICommandService _commandService;
        private readonly ILogger _logger;
        private const string controllerName = "UserController";

        public UserController(IQueryService queryService,ICommandService commandService,ILogger<UserController> logger)
        {
            this._queryService = queryService;
            this._commandService = commandService;
            this._logger = logger;
            
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetUserById(int userId)
        {
            _logger.LogInformation(controllerName+ " Action: GetUserById - Started");

            if (userId == 0)
            {
                return BadRequest();
            }
            try
            {
                UserDto userDto = await _queryService.GetUserById(userId);

                if (userDto != null)
                {
                    _logger.LogInformation(controllerName+ " Action: GetUserById - Ended");
                    return Ok(userDto);
                }
                else
                {
                    _logger.LogInformation(controllerName + " Action: GetUserById - User Not Found");
                    return NotFound("User Not Found Please Enter the Valid UserId.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(controllerName+ "Action: GetUserById - Error: " + ex.Message);
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserInputModel userInputModel)
        {
            _logger.LogInformation(controllerName + " Action: CreateUser - Ended");

            if (userInputModel == null)
            {
                return UnprocessableEntity(ModelState);
            }
            try
            {
                #region CreateUser
                UserOutputModel userOutputModel = await _commandService.CreateOrUpdateUser(userInputModel);

                if (userOutputModel.IsValid)
                {
                    userOutputModel.Output = "User ("+ userOutputModel.Output+") " + userInputModel.FirstName +" "+userInputModel.LastName+ " Created Successfully";
                    _logger.LogInformation(controllerName + " Action: CreateUser - User Creation Ended");

                    return Created("", userOutputModel);
                }
                else
                {
                    userOutputModel.Output = "Please see the below validations";
                    _logger.LogInformation(controllerName + " Action: CreateUser - Validations Ended");
                    return Ok(userOutputModel);
                }
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(controllerName + "Action: CreateUser - Error: " + ex.Message);
                throw ex;
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserInputModel userInputModel)
        {
            _logger.LogInformation(controllerName + " Action: UpdateUser - Started");

            if (userInputModel == null)
            {
                return UnprocessableEntity(ModelState);
            }
            try
            {
                #region Update User
                UserOutputModel userOutputModel = await _commandService.CreateOrUpdateUser(userInputModel);

                if (userOutputModel.IsValid)
                {
                    userOutputModel.Output = "User (" + userOutputModel.Output + ") " + userInputModel.FirstName + " " + userInputModel.LastName + " Updated Successfully";
                }
                else
                {
                    userOutputModel.Output = "Please see the below validations";
                }
                _logger.LogInformation(controllerName + " Action: CreateUser - User Creation Ended");
                return Ok(userOutputModel);

                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(controllerName + "Action: UpdateUser - Error: " + ex.Message);
                throw ex;
            }

        }


    }
}

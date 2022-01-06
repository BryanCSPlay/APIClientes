using APIClientes.Dto;
using APIClientes.Models;
using APIClientes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected ResponseDto _response;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var result = await _userRepository.Register(
                new User()
                {
                    UserName = user.UserName,

                }, user.Password);

            if (result == -1)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "User already exists";
                return BadRequest(_response);
            }

            if (result == -500)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error creating registry";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "User created successfully ";
            _response.Result = result;

            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var result = await _userRepository.Login(user.UserName, user.Password);

            if (result == "User does not exist")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = result;
                return BadRequest(_response);
            }

            if (result == "Wrong password")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = result;
                return BadRequest(_response);
            }

            return Ok("User Connect");
        }

    }
}

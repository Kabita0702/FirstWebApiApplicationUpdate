using FirstWebApiApplication.Models.Domain;
using FirstWebApiApplication.Models.DTO;
using FirstWebApiApplication.Repository;
using FirstWebApiApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FirstWebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("~/adduser")]
        public IActionResult AddUserDetails(AddUserDTO inputRequest)
        {
            try
            {
                var serviceObj = new UserService();

                return Ok(serviceObj.CreateUser(inputRequest));
            }
            catch (Exception ex)
            {

                return BadRequest(new Response()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                });
            }

        }
        [HttpDelete("~/deleteuser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var serviceObj = new UserService();

                return Ok(serviceObj.DeleteUser(id));

            }
            catch (Exception ex)
            {

                return BadRequest(new Response()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                });
            }
        }
        [HttpGet("~/users")]
        public IActionResult GetAllUserDetails()
        {
            try
            {
                var serviceObj = new UserService();

                return Ok(serviceObj.GetAllUsers());
            }
            catch (Exception ex)
            {

                return BadRequest(new Response()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                });
            }

        }
        [HttpGet("~/user/{id}")]
        public IActionResult GetUserByID(int id)
        {
            try
            {
                var serviceObj = new UserService();

                return Ok(serviceObj.GetUserById(id));
            }
            catch (Exception ex)
            {

                return BadRequest(new Response()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                });
            }

        }
        [HttpPost("~/login")]
        public IActionResult Login(User inputRequest)
        {
            try
            {
                var serviceObj = new UserService();

                return Ok(serviceObj.LoginUser(inputRequest));
            }
            catch (Exception ex)
            {

                return Unauthorized(new Response()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = ex.Message
                });
            }

        }
        [HttpPost("~/genericadduser")]
        public IActionResult GenericAddUserDetails(AddUserDTO inputRequest)
        {
            try
            {
                var serviceObj = new UserService();

                return Ok(serviceObj.GenericAddUser(inputRequest));
            }
            catch (Exception ex)
            {

                return BadRequest(new Response()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                });
            }

        }


    }
}

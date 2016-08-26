///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using Microsoft.AspNetCore.Mvc;
using Evidencija.Controllers.RequestBinders;
using Evidencija.Database.Models;
using Evidencija.Auth;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Evidencija.Controllers
{
    [Authorize]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IDbContextBinder _binder { get; set; }

        private JwtTokenProvider _tokenProvider { get; set; }

        public AuthController(IDbContextBinder Binder, JwtTokenProvider TokenProvider)
        {
            _tokenProvider = TokenProvider;
            _binder = Binder;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public JsonResult Login([FromBody]AuthRequest Request)
        {
            var User = _binder.GetUser(Request.UserName, Request.Key);

            if (User == default(User)) return new JsonResult(new object());

            var TokenData = _tokenProvider.Create(User, 600);

            return new JsonResult(TokenData);
        }

        [Authorize("Admin")]
        [HttpPost("admin/register")]
        public JsonResult AdminRegister([FromBody]AuthRequest Request)
        {
            var User = new User() { UserName = Request.UserName, LoginKey = Request.Key, IsAdmin = Request.IsAdmin };

            User = _binder.CreateUser(User);

            if (User == default(User)) return new JsonResult(new object());

            var TokenData = _tokenProvider.Create(User, 600);

            return new JsonResult(User);
        }

        [Authorize("Admin")]
        [HttpGet("admin/getusers")]
        public JsonResult AdminGetUsers()
        {
            var Users = _binder.GetAllUsers();

            return new JsonResult(Users);
        }

        [Authorize("Admin")]
        [HttpDelete("admin/deleteuser/{userid}")]
        public JsonResult AdminDeleteUser(int userid)
        {
            var success = _binder.DeleteUser(userid);

            return new JsonResult(success);
        }

        [HttpPut("modify")]
        public JsonResult ModifyUser(User User)
        {
            var dbUser = _binder.GetUser(User.Id);

            if (HttpContext.User.Claims.Where(c => c.ValueType == "UserName").SingleOrDefault().Value != User.UserName) return new JsonResult(new object());

            User = _binder.ModifyUser(User);

            return new JsonResult(User);
        }

        [Authorize("Admin")]
        [HttpPut("admin/modify")]
        public JsonResult AdminModifyUser(User User)
        {
            User = _binder.ModifyUser(User);

            return new JsonResult(User);
        }
    }
}

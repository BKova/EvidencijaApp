//using Microsoft.AspNetCore.Mvc;
//using Evidencija.Controllers.RequestBinders;
//using Evidencija.Database.Models;
//using System.Security.Claims;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using Evidencija.Auth;

//namespace Evidencija.Controllers
//{
//    [Route("api/auth")]
//    public class AuthController : Controller
//    {
//        private IDbContextBinder _binder { get; set; }

//        private JwtTokenProvider _tokenProvider { get; set; }

//        public AuthController(IDbContextBinder Binder, JwtTokenProvider TokenProvider)
//        {
//            _tokenProvider = TokenProvider;
//            _binder = Binder;
//        }

//        [HttpGet("login")]
//        public JsonResult Get([FromBody]AuthRequest Request)
//        {
//            var User = _binder.GetUser(Request.UserName, Request.Key);

//            if (User == default(User)) return new JsonResult(new object());



//            return new JsonResult();
//        }
//    }
//}

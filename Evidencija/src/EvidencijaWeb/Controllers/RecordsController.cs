using Evidencija.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Evidencija.Controllers.RequestBinders
{
    [Authorize]
    [Route("api/data")]
    public class RecordsController : Controller
    {
        private IDbContextBinder _binder { get; set; }

        public RecordsController(IDbContextBinder Binder)
        {
            _binder = Binder;
        }

        [HttpPut("modify")]
        public JsonResult Modify([FromBody]TimeStamp Stamp)
        {
            Stamp = _binder.GetTimeStamp(Stamp.Id);

            if (HttpContext.User.Claims.Where(c => c.ValueType == "UserName").SingleOrDefault().Value != Stamp.User.UserName) return new JsonResult(new object());

            Stamp = _binder.ModifyTimeStamp(Stamp);

            return new JsonResult(Stamp);
        }

        [Authorize("Admin")]
        [HttpPut("admin/modify")]
        public JsonResult AdminModify([FromBody]TimeStamp Stamp)
        {
            Stamp = _binder.GetTimeStamp(Stamp.Id);

            Stamp = _binder.ModifyTimeStamp(Stamp);

            return new JsonResult(Stamp);
        }

        [HttpGet("getstamps/{userid}")]
        public JsonResult GetStamps(int userid)
        {
            var User = _binder.GetUser(userid);
            if (HttpContext.User.Claims.Where(c => c.ValueType == "UserName").SingleOrDefault().Value != User.UserName) return new JsonResult(new object());

            var Stamps = _binder.UserTimeStamps(User);

            return new JsonResult(Stamps);
        }

        [Authorize("Admin")]
        [HttpGet("admin/getstamps/{userid}")]
        public JsonResult AdminStamps(int userid)
        {
            var User = _binder.GetUser(userid);

            var Stamps = _binder.UserTimeStamps(User);

            return new JsonResult(Stamps);
        }

        [HttpDelete("deletestamp/{stampid}")]
        public JsonResult DeleteStamp(int stampid)
        {
            var Stamp = _binder.GetTimeStamp(stampid);

            if (HttpContext.User.Claims.Where(c => c.ValueType == "UserName").SingleOrDefault().Value != Stamp.User.UserName) return new JsonResult(false);

            var success = _binder.DeleteTimeStamp(stampid);

            return new JsonResult(success);
        }

        [HttpDelete("admin/deletestamp/{stampid}")]
        public JsonResult AdminDeleteStamp(int stampid)
        {
            var success = _binder.DeleteTimeStamp(stampid);

            return new JsonResult(success);
        }
    }
}
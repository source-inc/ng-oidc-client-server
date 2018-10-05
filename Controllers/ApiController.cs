using System.Security.Claims;
using IdentityServer4.Extensions;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ng_oidc_client_server {

    [Route ("api")]
    public class ApiController : Controller {

        [HttpGet ("")]
        public IActionResult Index () {
            return Ok ("API works");
        }

        [Authorize (AuthenticationSchemes = "api")]
        [HttpGet ("protected")]
        public IActionResult GetProtected () {

            return Ok (new { auhtenticated = "true" });
        }
    }
}
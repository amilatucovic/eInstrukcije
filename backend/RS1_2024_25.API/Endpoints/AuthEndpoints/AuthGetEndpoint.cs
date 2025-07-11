﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.API.Helper.Api;
using RS1_2024_2025.Services;
using System.Threading;
using System.Threading.Tasks;
using static RS1_2024_2025.API.Endpoints.AuthEndpoints.AuthGetEndpoint;

namespace RS1_2024_2025.API.Endpoints.AuthEndpoints
{
    [Route("auth")]
    public class AuthGetEndpoint(MyAuthService authService) : MyEndpointBaseAsync
        .WithoutRequest
        .WithActionResult<AuthGetResponse>
    {
        [HttpGet]
        public override async Task<ActionResult<AuthGetResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            // Retrieve user info based on the token
            var authInfo = authService.GetAuthInfo();

            if (!authInfo.IsLoggedIn)
            {
                return Unauthorized("Invalid or expired token");
            }

            // Return user information if the token is valid
            return Ok(new AuthGetResponse
            {
                MyAuthInfo = authInfo
            });
        }

        public class AuthGetResponse
        {
            public required MyAuthInfo MyAuthInfo { get; set; }
        }
    }
}

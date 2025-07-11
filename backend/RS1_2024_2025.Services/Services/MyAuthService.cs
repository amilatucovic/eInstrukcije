﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Entities.Models;

namespace RS1_2024_2025.Services
{
    public class MyAuthService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, MyTokenGenerator myTokenGenerator)
    {

        // Generisanje novog tokena za korisnika
        public async Task<MyAuthenticationToken> GenerateAuthToken(MyAppUser user, CancellationToken cancellationToken = default)
        {
            string randomToken = myTokenGenerator.Generate(10);

            var authToken = new MyAuthenticationToken
            {
                IpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
                Value = randomToken,
                MyAppUser = user,
                RecordedAt = DateTime.Now
            };

            applicationDbContext.MyAuthenticationTokens.Add(authToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return authToken;
        }

        // Uklanjanje tokena iz baze podataka
        public async Task<bool> RevokeAuthToken(string tokenValue, CancellationToken cancellationToken = default)
        {
            var authToken = await applicationDbContext.MyAuthenticationTokens
                .FirstOrDefaultAsync(t => t.Value == tokenValue, cancellationToken);

            if (authToken == null)
                return false;

            applicationDbContext.MyAuthenticationTokens.Remove(authToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        // Dohvatanje informacija o autentifikaciji korisnika
        public MyAuthInfo GetAuthInfo()
        {
            string? authToken = httpContextAccessor.HttpContext?.Request.Headers["my-auth-token"];
            if (string.IsNullOrEmpty(authToken))
            {
                return GetAuthInfo(null);
            }

            var myAuthToken = applicationDbContext.MyAuthenticationTokens
                .Include(x => x.MyAppUser)
                .SingleOrDefault(x => x.Value == authToken);

            return GetAuthInfo(myAuthToken);
        }

        public MyAuthInfo GetAuthInfo(MyAuthenticationToken? myAuthToken)
        {
            if (myAuthToken == null || myAuthToken.MyAppUser == null)
            {
                return new MyAuthInfo
                {
                    IsLoggedIn = false,
                    UserType = UserType.Student // ili neka default vrijednost
                };
            }

            return new MyAuthInfo
            {
                UserId = myAuthToken.MyAppUserId,
                Username = myAuthToken.MyAppUser.Username,
                FirstName = myAuthToken.MyAppUser.FirstName,
                LastName = myAuthToken.MyAppUser.LastName,
                IsLoggedIn = true,
                UserType = myAuthToken.MyAppUser.UserType
            };
        }

    }

    // DTO to hold authentication information
    public class MyAuthInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public bool IsLoggedIn { get; set; }
        public string SlikaPath {  get; set; }
    }
}

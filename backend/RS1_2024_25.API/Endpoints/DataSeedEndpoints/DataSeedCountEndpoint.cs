namespace RS1_2024_2025.API.Endpoints.DataSeed
{
    using Microsoft.AspNetCore.Mvc;
    using RS1_2024_2025.Domain;
    using RS1_2024_2025.API.Helper.Api;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using RS1_2024_2025.Database;

    namespace FIT_Api_Example.Endpoints
    {
        [Route("data-seed")]
        public class DataSeedCountEndpoint(ApplicationDbContext db)
            : MyEndpointBaseAsync
            .WithoutRequest
            .WithResult<Dictionary<string, int>>
        {
            [HttpGet]
            public override async Task<Dictionary<string, int>> HandleAsync(CancellationToken cancellationToken = default)
            {
                Dictionary<string, int> dataCounts = new ()
                {
                   
                    { "City", db.Cities.Count() },
                    { "MyAppUser", db.MyAppUsers.Count() }
                };

                return dataCounts;
            }
        }
    }

}

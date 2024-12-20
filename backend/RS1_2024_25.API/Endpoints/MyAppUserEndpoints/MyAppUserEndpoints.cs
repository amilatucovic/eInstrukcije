using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.Domain.Entities.Models;
using RS1_2024_2025.Domain;
using RS1_2024_2025.API.Endpoints.SubjectEndpoint;
using RS1_2024_2025.Domain.Entities.Models.Auth;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.API.Endpoints.MyAppUserEndpoints
{

    [Route("api/[controller]")]
    [ApiController]
    public class MyAppUserEndpoint(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var myAppUsers = db.MyAppUsers
                .Select(s => new MyAppUser
                {
                   ID=s.ID,
                   FirstName=s.FirstName,
                   LastName=s.LastName,
                   Username=s.Username,
                   Email=s.Email
                })
                .ToArray();

            return Ok(myAppUsers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var myAppUser = db.MyAppUsers
                .Where(s => s.ID == id)
                .Select(s => new MyAppUser
                {
                    ID=s.ID,
                    FirstName=s.FirstName,
                    LastName=s.LastName,
                    Username=s.Username,
                    Email=s.Email
                })
                .FirstOrDefault();

            if (myAppUser == null)
            {
                return NotFound();
            }

            return Ok(myAppUser);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MyAppUserInsertRequest request, CancellationToken cancellationToken)
        {
            var newAppUser = new MyAppUser
            {
                Username = request.Username,
                Password= request.Password,
                FirstName=request.FirstName,
                LastName=request.LastName,
                Age=request.Age,
                Email=request.Email,
                PhoneNumber=request.PhoneNumber,
                CityId=request.CityId,
                UserType=request.UserType,
                Gender=request.Gender,
                ProfileImageUrl = await UploadImageHelper.UploadFile(request.ProfileImage)
            };

            db.MyAppUsers.Add(newAppUser);
            await db.SaveChangesAsync(cancellationToken);

            var userInsert = new MyAppUser
            {
                Username = request.Username,
                Password= request.Password,
                FirstName=request.FirstName,
                LastName=request.LastName,
                Age=request.Age,
                Email=request.Email,
                PhoneNumber=request.PhoneNumber,
            };

            return Ok(userInsert);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MyAppUserUpdateRequest request)
        {
            var appUser = db.MyAppUsers.FirstOrDefault(s => s.ID == id);
            if (appUser == null)
            {
                return NotFound();
            }

           
            appUser.FirstName = request?.FirstName??appUser.FirstName;
            appUser.LastName =request?.LastName??appUser.FirstName; ;
            appUser.Email = request?.Email??appUser.FirstName;
            appUser.Username=request.Username??appUser.FirstName;

            db.SaveChanges();

            var userRequest = new MyAppUser
            {

               Username=request.Username,
               FirstName = request.FirstName,
               LastName = request.LastName,
               Email = request.Email
            };

            return Ok(userRequest);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var appUser = db.MyAppUsers.Where(x => x.ID == id).FirstOrDefault();
            if (appUser == null)
            {
                return NotFound();
            }
            else
            {
                db.MyAppUsers.Remove(appUser);
                db.SaveChanges();
                return Ok(appUser);
            }
        }
    }
}

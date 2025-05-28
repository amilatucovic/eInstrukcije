using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            // Check if a user with the same username or email already exists
            var existingUser = await db.MyAppUsers
                .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email, cancellationToken);

            if (existingUser != null)
            {
                return BadRequest(new { Message = "A user with the same username or email already exists." });
            }

            // Check if the password already exists for another user
            var existingPassword = await db.MyAppUsers
                .AnyAsync(u => u.Password == request.Password, cancellationToken);

            if (existingPassword)
            {
                return BadRequest(new { Message = "The password is already in use. Please choose a different password." });
            }

            // Create a new user if validations pass
            var newAppUser = new MyAppUser
            {
                Username = request.Username,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CityId = request.CityId,
                UserType = request.UserType,
                Gender = request.Gender,
                ProfileImageUrl = await UploadImageHelper.UploadFile(request.ProfileImage)
            };

            db.MyAppUsers.Add(newAppUser);
            await db.SaveChangesAsync(cancellationToken);

            // Return a response without sensitive data like the password
            var userInsert = new
            {
                newAppUser.Username,
                newAppUser.FirstName,
                newAppUser.LastName,
                newAppUser.Age,
                newAppUser.Email,
                newAppUser.PhoneNumber
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

        [HttpGet("check-username-availability")]
        public async Task<IActionResult> CheckUsernameAvailability(string username, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new { Message = "Username is required." });
            }

            var isAvailable = !await db.MyAppUsers.AnyAsync(u => u.Username == username, cancellationToken);

            return Ok(new { Available = isAvailable });
        }

        [HttpGet("check-password-availability")]
        public async Task<IActionResult> CheckPasswordAvailability(string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new { Message = "Username is required." });
            }

            var isAvailable = !await db.MyAppUsers.AnyAsync(p => p.Password == password, cancellationToken);

            return Ok(new { Available = isAvailable });
        }


    }
}

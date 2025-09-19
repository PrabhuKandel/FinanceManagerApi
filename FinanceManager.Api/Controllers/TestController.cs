using FinanceManager.Application.Services;
using Hangfire;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // In-memory list with initial data and IDs
        public  static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new User { Id = 2, Name = "Bob", Email = "bob@example.com" },
            new User { Id = 3, Name = "Charlie", Email = "charlie@example.com" }
        };

      
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_users);
        }



       
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

    

        [HttpPost]
        public IActionResult Create(User user)
        {

            //BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));
            //BackgroundJob.Schedule(() => Console.WriteLine("Scheduled Job"), TimeSpan.FromSeconds(20));
            //RecurringJob.AddOrUpdate("write-log-job",() => TestJob.WriteLog(), Cron.Minutely);
            // Auto-generate Id
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPost("send-email")]
        public IActionResult SendEmail()
        {
            //BackgroundJob.Enqueue<MailKitEmailService>(x => x.SendEmailAsync("This is test email"));
            return Ok("Email job enqueued");
        }

        public static class TestJob
        {
            public static void WriteLog()
            {
                Console.WriteLine("Recurring job");
            }
        }

     



        //using post to update user data
        [HttpPost]
        [Route("user/update/{id}")]
        public IActionResult UpdateUser(int id, [FromForm] User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == id);
            if (existing == null)
                return NotFound();

            // Replace existing data
            existing.Name = user.Name;
            existing.Email = user.Email;

            return Content("Updated");
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            var existing = _users.FirstOrDefault(u => u.Id == id);
            if (existing == null)
                return NotFound();

            // Replace existing data
            existing.Name = updatedUser.Name;
            existing.Email = updatedUser.Email;

            return Content("Updated");
        }





        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var user = _users.Find(u => u.Id == id);
            if (user == null)
                return NotFound();

            patchDoc.ApplyTo(user);



            return Ok("Updated");
        }


        ////patch without using Json Patch
        //[HttpPatch("{id}")]
        ////public  IActionResult PatchUser(int id, UpdateUserDto dto)
        //{
        //    var user = _users.FirstOrDefault(u => u.Id == id);
        //    if (user == null)
        //        return NotFound();

        //    if (!string.IsNullOrEmpty(dto.Name))
        //        user.Name = dto.Name;

        //    if (!string.IsNullOrEmpty(dto.Email))
        //        user.Email = dto.Email;

        //    return Content("Updated");
        //}



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            _users.Remove(user);
            return Ok("Deleted");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

}

using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        public static string GenerateUniqueIdentifier(string user)
        {
            byte[] bytes = new byte[16];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            // Convert the bytes to a string representation
            return BitConverter.ToString(bytes).Replace("-", "").ToLower()+user;
        }
    }
}
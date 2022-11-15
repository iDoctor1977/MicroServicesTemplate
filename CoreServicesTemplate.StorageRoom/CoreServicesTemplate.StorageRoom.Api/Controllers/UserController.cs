using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [Route("StorageRoom/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserFeature _createUserFeature;
        private readonly IReadUsersFeature _readUsersFeature;

        public UserController(ICreateUserFeature createUserFeature, IReadUsersFeature readUsersFeature)
        {
            _createUserFeature = createUserFeature;
            _readUsersFeature = readUsersFeature;
        }

        // POST: StorageRoom/User/Post
        [HttpPost]
        public async Task Post(UserApiModel model)
        {
            await _createUserFeature.HandleAsync(model);
        }

        // GET: StorageRoom/User/Get
        [HttpGet]
        public async Task<UsersApiModel> Get()
        {
            var result = await _readUsersFeature.HandleAsync();

            return result;
        }
    }
}

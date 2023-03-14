using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;

namespace CoreServicesTemplate.StorageRoom.Data.Builders
{
    public class UserAppModelBuilder : IUserAppModelBuilder, IUserAppModelAdded
    {
        private ICollection<UserAppModel> _users;

        public UserAppModelBuilder() {
            _users = new List<UserAppModel>();
        }

        private UserAppModel CreateUser(string name)
        {
            var user = new UserAppModel
            {
                Guid = Guid.NewGuid(),
                Name = name,
            };

            return user;
        }

        private UserAppModel CreateUser(string name, string surname)
        {
            var user = CreateUser(name);
            user.Surname = surname;

            return user;
        }
        private UserAppModel CreateUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name);
            user.Surname = surname;
            user.Birth = birth;

            return user;
        }

        public IUserAppModelAdded AddUser(string name)
        {
            var user = CreateUser(name);
            _users.Add(user);

            return this;
        }

        public IUserAppModelAdded AddUser(string name, string surname)
        {
            var user = CreateUser(name, surname);
            _users.Add(user);

            return this;
        }
        public IUserAppModelAdded AddUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name, surname, birth);
            _users.Add(user);

            return this;
        }

        public IEnumerable<UserAppModel> Build()
        {
            var result = _users;
            _users = null;

            return result;
        }
    }

    public interface IUserAppModelBuilder
    {
        IUserAppModelAdded AddUser(string name);
        IUserAppModelAdded AddUser(string name, string surname);
        IUserAppModelAdded AddUser(string name, string surname, DateTime birth);
    }

    public interface IUserAppModelAdded : IUserAppModelBuilder
    {
        IEnumerable<UserAppModel> Build();
    }
}

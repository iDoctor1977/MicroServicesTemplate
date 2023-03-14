using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;

namespace CoreServicesTemplate.StorageRoom.Data.Builders
{
    public class UserAggModelBuilder : IUserAggModelBuilder, IUserAggModelAdded
    {
        private ICollection<UserAggModel> _users;

        public UserAggModelBuilder() {
            _users = new List<UserAggModel>();
        }

        private UserAggModel CreateUser(string name)
        {
            var user = new UserAggModel
            {
                Guid = Guid.NewGuid(),
                Name = name,
            };

            return user;
        }

        private UserAggModel CreateUser(string name, string surname)
        {
            var user = CreateUser(name);
            user.Surname = surname;

            return user;
        }
        private UserAggModel CreateUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name);
            user.Surname = surname;
            user.Birth = birth;

            return user;
        }

        public IUserAggModelAdded AddUser(string name)
        {
            var user = CreateUser(name);
            _users.Add(user);

            return this;
        }

        public IUserAggModelAdded AddUser(string name, string surname)
        {
            var user = CreateUser(name, surname);
            _users.Add(user);

            return this;
        }
        public IUserAggModelAdded AddUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name, surname, birth);
            _users.Add(user);

            return this;
        }

        public IEnumerable<UserAggModel> Build()
        {
            var result = _users;
            _users = null;

            return result;
        }
    }

    public interface IUserAggModelBuilder
    {
        IUserAggModelAdded AddUser(string name);
        IUserAggModelAdded AddUser(string name, string surname);
        IUserAggModelAdded AddUser(string name, string surname, DateTime birth);
    }

    public interface IUserAggModelAdded : IUserAggModelBuilder
    {
        IEnumerable<UserAggModel> Build();
    }
}

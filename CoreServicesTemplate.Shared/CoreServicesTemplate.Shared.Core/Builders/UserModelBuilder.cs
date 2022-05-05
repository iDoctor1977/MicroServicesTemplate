using System;
using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Shared.Core.Builders
{
    public class UserModelBuilder : IUserModelBuilder, IUserModelAdded
    {
        private ICollection<UserApiModel> _users;

        public UserModelBuilder() {
            _users = new List<UserApiModel>();
        }

        private UserApiModel CreateUser(string name)
        {
            var user = new UserApiModel
            {
                Guid = Guid.NewGuid(),
                Name = name,
            };

            return user;
        }

        private UserApiModel CreateUser(string name, string surname)
        {
            var user = CreateUser(name);
            user.Surname = surname;

            return user;
        }
        private UserApiModel CreateUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name);
            user.Surname = surname;
            user.Birth = birth;

            return user;
        }

        public IUserModelAdded AddUser(string name)
        {
            var user = CreateUser(name);
            _users.Add(user);

            return this;
        }

        public IUserModelAdded AddUser(string name, string surname)
        {
            var user = CreateUser(name, surname);
            _users.Add(user);

            return this;
        }
        public IUserModelAdded AddUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name, surname, birth);
            _users.Add(user);

            return this;
        }

        public IEnumerable<UserApiModel> Build()
        {
            var result = _users;
            _users = null;

            return result;
        }
    }

    public interface IUserModelBuilder
    {
        IUserModelAdded AddUser(string name);
        IUserModelAdded AddUser(string name, string surname);
        IUserModelAdded AddUser(string name, string surname, DateTime birth);
    }

    public interface IUserModelAdded : IUserModelBuilder
    {
        IEnumerable<UserApiModel> Build();
    }
}

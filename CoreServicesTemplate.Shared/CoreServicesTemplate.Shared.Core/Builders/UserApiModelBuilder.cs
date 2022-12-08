using System;
using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Shared.Core.Builders
{
    public class UserApiModelBuilder : IUserApiModelBuilder, IUserApiModelAdded
    {
        private ICollection<UserApiModel> _users;

        public UserApiModelBuilder() {
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

        public IUserApiModelAdded AddUser(string name)
        {
            var user = CreateUser(name);
            _users.Add(user);

            return this;
        }

        public IUserApiModelAdded AddUser(string name, string surname)
        {
            var user = CreateUser(name, surname);
            _users.Add(user);

            return this;
        }
        public IUserApiModelAdded AddUser(string name, string surname, DateTime birth)
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

    public interface IUserApiModelBuilder
    {
        IUserApiModelAdded AddUser(string name);
        IUserApiModelAdded AddUser(string name, string surname);
        IUserApiModelAdded AddUser(string name, string surname, DateTime birth);
    }

    public interface IUserApiModelAdded : IUserApiModelBuilder
    {
        IEnumerable<UserApiModel> Build();
    }
}

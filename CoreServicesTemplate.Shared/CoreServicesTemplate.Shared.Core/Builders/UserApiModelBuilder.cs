using System;
using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Shared.Core.Builders
{
    public class UserApiModelBuilder : IUserApiModelBuilder, IUserApiApiModelAdded
    {
        private ICollection<UserApiModel> _users;

        public UserApiModelBuilder() {
            _users = new List<UserApiModel>();
        }

        private UserApiModel CreateUser(string name)
        {
            var user = new UserApiModel
            {
                Id = new Random().Next(1, 100),
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

        public IUserApiApiModelAdded AddUser(string name)
        {
            var user = CreateUser(name);
            _users.Add(user);

            return this;
        }

        public IUserApiApiModelAdded AddUser(string name, string surname)
        {
            var user = CreateUser(name, surname);
            _users.Add(user);

            return this;
        }
        public IUserApiApiModelAdded AddUser(string name, string surname, DateTime birth)
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
        IUserApiApiModelAdded AddUser(string name);
        IUserApiApiModelAdded AddUser(string name, string surname);
        IUserApiApiModelAdded AddUser(string name, string surname, DateTime birth);
    }

    public interface IUserApiApiModelAdded : IUserApiModelBuilder
    {
        IEnumerable<UserApiModel> Build();
    }
}

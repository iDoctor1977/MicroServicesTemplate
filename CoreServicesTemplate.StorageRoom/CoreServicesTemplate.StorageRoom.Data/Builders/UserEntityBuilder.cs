using System;
using System.Collections.Generic;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Builders
{
    public class UserEntityBuilder : IUserEntityBuilder, IUserEntityAdded
    {
        private ICollection<User> _users;
        private int _id;

        public UserEntityBuilder() {
            _users = new List<User>();
        }

        private User CreateUser(string name)
        {
            var user = new User
            {
                Id = _id,
                Name = name,
            };

            _id++;

            return user;
        }

        private User CreateUser(string name, string surname)
        {
            var user = CreateUser(name);
            user.Surname = surname;

            return user;
        }
        private User CreateUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name);
            user.Surname = surname;
            user.Birth = birth;

            return user;
        }

        public IUserEntityAdded AddUser(string name)
        {
            var user = CreateUser(name);
            _users.Add(user);

            return this;
        }

        public IUserEntityAdded AddUser(string name, string surname)
        {
            var user = CreateUser(name, surname);
            _users.Add(user);

            return this;
        }
        public IUserEntityAdded AddUser(string name, string surname, DateTime birth)
        {
            var user = CreateUser(name, surname, birth);
            _users.Add(user);

            return this;
        }

        public IEnumerable<User> Build()
        {
            var result = _users;
            _users = null;
            _id = 0;

            return result;
        }
    }

    public interface IUserEntityBuilder
    {
        IUserEntityAdded AddUser(string name);
        IUserEntityAdded AddUser(string name, string surname);
        IUserEntityAdded AddUser(string name, string surname, DateTime birth);
    }

    public interface IUserEntityAdded : IUserEntityBuilder
    {
        IEnumerable<User> Build();
    }
}

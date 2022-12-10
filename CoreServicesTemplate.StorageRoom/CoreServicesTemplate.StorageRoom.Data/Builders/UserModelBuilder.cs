﻿using System;
using System.Collections.Generic;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Data.Builders
{
    public class UserModelBuilder : IUserModelBuilder, IUserModelAdded
    {
        private ICollection<UserModel> _users;

        public UserModelBuilder() {
            _users = new List<UserModel>();
        }

        private UserModel CreateUser(string name)
        {
            var user = new UserModel
            {
                Id = new Random().Next(1, 100),
                Guid = Guid.NewGuid(),
                Name = name,
            };

            return user;
        }

        private UserModel CreateUser(string name, string surname)
        {
            var user = CreateUser(name);
            user.Surname = surname;

            return user;
        }
        private UserModel CreateUser(string name, string surname, DateTime birth)
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

        public IEnumerable<UserModel> Build()
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
        IEnumerable<UserModel> Build();
    }
}
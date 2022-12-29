using System;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates
{
    public class UserAggregate : AAggregateBase<UserModel>
    {
        public UserAggregate(UserModel model) : base(model) { }

        public void SetGuid(Guid guid)
        {
            Model.Guid = guid;
        }

        public void SetName (string name)
        {
            Model.Name = name;
        }
        public void SetSurname (string surname)
        {
            Model.Surname = surname;
        }
        public void SetBirth (DateTime birth)
        {
            Model.Birth = birth;
        }

        public override UserModel ToModel()
        {
            return Model;
        }

        public override bool IsModelValid()
        {
            var value = !string.IsNullOrWhiteSpace(Model.Name);
            value = !string.IsNullOrWhiteSpace(Model.Surname);

            return value;
        }
    }
}
using System;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates
{
    public class GetAggregate : AAggregateBase<UserModel>
    {
        public GetAggregate(UserModel model) : base(model) { }

        public void SetGuid(Guid guid)
        {
            Model.Guid = guid;
        }

        public void SetName (string name)
        {
            Model.Name = name;
        }

        public void SetSurname(string surname)
        {
            Model.Name = surname;
        }

        public void SetBirthDay(DateTime birth)
        {
            Model.Birth = birth;
        }

        public override UserModel ToModel()
        {
            return Model;
        }

        protected override bool IsModelValid()
        {
            bool value = !string.IsNullOrWhiteSpace(Model.Name);
            value = !string.IsNullOrWhiteSpace(Model.Surname);

            return value;
        }
    }
}
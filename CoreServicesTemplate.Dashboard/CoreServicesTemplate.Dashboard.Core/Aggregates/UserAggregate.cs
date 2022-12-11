using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Dashboard.Core.Aggregates
{
    public class UserAggregate : AAggregateBase<UserModel>
    {
        public UserAggregate(UserModel model) : base(model) { }

        public void SetId(int id)
        {
            Model.Id = id;
        }

        public int GetId()
        {
            return Model.Id;
        }

        public void SetGuid(Guid guid)
        {
            Model.Guid = guid;
        }

        public Guid GetGuid()
        {
            return Model.Guid;
        }

        public void SetName (string name)
        {
            Model.Name = name;
        }
        public void SetSurame (string surname)
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

        protected override bool IsModelValid()
        {
            var value = !string.IsNullOrWhiteSpace(Model.Name);
            value = !string.IsNullOrWhiteSpace(Model.Surname);

            return value;
        }
    }
}
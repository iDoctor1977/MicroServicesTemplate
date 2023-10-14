using System.ComponentModel.DataAnnotations;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.Entities
{
    public class EntityEfBase : IEntityEfBase
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        public EntityState State { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        protected EntityEfBase()
        {
            Guid = Guid.NewGuid();
            State = EntityState.Added;
            DateCreated = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
    }
}

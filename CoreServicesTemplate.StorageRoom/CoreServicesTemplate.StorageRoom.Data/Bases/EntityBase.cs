using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        public EntityState State { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public EntityBase()
        {
            Guid = Guid.NewGuid();
            State = EntityState.Added;
            DateCreated = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
    }
}

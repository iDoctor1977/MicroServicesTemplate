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
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}

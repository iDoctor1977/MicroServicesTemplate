using System;
using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.StorageRoom.Data.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        public bool IsDeleted { get; set; }
        public string DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}

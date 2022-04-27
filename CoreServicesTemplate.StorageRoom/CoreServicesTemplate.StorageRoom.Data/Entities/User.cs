using System;
using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.StorageRoom.Data.Entities
{
    public class User : SoftDeleteEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}
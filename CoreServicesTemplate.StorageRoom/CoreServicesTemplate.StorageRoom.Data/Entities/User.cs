using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreServicesTemplate.StorageRoom.Data.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.Entities
{
    [Table("Users")]
    public class User : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public DateTime Birth { get; set; }
    }
}
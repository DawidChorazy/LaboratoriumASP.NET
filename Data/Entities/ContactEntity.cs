using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

    [Table("contacts")]
    public class ContactEntity
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        [MaxLength(12)]
        [MinLength(9)]
        public string Phone { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
        public int OrganizationId { get; set; }
        public OrganizationEntity? Organization { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }

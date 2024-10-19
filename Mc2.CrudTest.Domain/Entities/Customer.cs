using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Domain.Entities
{
    public class Customer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string BankAccountNumber { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }


    }
}

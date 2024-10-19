using IbanNet.DataAnnotations;
using Mc2.CrudTest.Presentation.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Mc2.CrudTest.Presentation.Shared.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please enter the FirstName")]
        [StringLength(50, ErrorMessage = "Maximum characters is {1}")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the LastName")]
        [StringLength(50, ErrorMessage = "Maximum characters is {1}")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please enter the PhoneNumber")]
        [StringLength(20, ErrorMessage = "Maximum characters is {1}")]
        [PhoneNumber(ErrorMessage = "Please enter a valid PhoneNumber!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter the Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email!")]
        [StringLength(50, ErrorMessage = "Maximum characters is {1}")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the BankAccountNumber")]
        [StringLength(50, ErrorMessage = "Maximum characters is {1}")]
        [Iban(ErrorMessage = "Please enter a valid BankAccountNumber!")]
        public string BankAccountNumber { get; set; }
    }
}

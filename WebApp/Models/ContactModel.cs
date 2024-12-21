using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace WebApp.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please, type in your name!")]
    [MaxLength(length: 20, ErrorMessage = "Name cannot be longer than 20 characters!")]
    [MinLength(length: 1, ErrorMessage = "Name cannot be shorter than 1 character!")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please, type in your name!")]
    [MaxLength(length: 50, ErrorMessage = "Last name cannot be longer than 20 characters!")]
    [MinLength(length: 1, ErrorMessage = "Last name cannot be shorter than 1 character!")]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime Birth { get; set; }
    
    [Phone]
    [RegularExpression("\\+\\d\\d \\d\\d\\d \\d\\d\\d \\d\\d\\d", ErrorMessage = "Enter number: +xx xxx xxx xxx")]
    public string PhoneNumber { get; set; }
    
    [Display(Name = "Priorytet")]
    public Priority Priority { get; set; }
    [HiddenInput]
    public DateTime Created { get; set; }
    
    
    [HiddenInput]
    public int OrganizationId { get; set; }

    [ValidateNever]
    public List<SelectListItem> Organizations{ get; set; }
}
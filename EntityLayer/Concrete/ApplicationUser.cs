using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

//namespace HospitalAutomation.Areas.Identity.Data;
namespace EntityLayer.Concrete;
//namespace DataAccessLayer.Concrete;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string NameSurname { get; set; }
}


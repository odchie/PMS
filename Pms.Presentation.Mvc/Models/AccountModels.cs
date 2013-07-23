using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

using Pms.Presentation.Mvc.Helper.Attribute;

namespace Pms.Presentation.Mvc.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("DefaultConnection") { }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [LocalizedDisplayName("Person_Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("Person_Password")]
        public string Password { get; set; }

        [LocalizedDisplayName("LoginModel_RememberMe")]
        public bool RememberMe { get; set; }

        public string PersonId { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public int AccountState { get; set; }
    }
}

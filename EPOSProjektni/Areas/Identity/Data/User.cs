using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EPOSProjektni.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")] // atributi koji oznacavaju da se radi o licnim podacima i ogranicenje za unos
        public string Ime { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Prezime { get; set; }

    }
}

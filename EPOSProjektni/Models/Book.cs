using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EPOSProjektni.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string Naziv { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Autor { get; set; }
        [Required]
        [RegularExpression("^[0-9 ]*$")]
        public string Godina { get; set; }
        [Required]
        [DisplayName("Broj strana")]
        public int BrojStrana { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Slika { get; set; }

        [NotMapped]
        [DisplayName("Priloži sliku")]
        public IFormFile FajlSlike { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPOSProjektni.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        public string Autor { get; set; }
        [RegularExpression("^[0-9 ]*$")]
        public string Godina { get; set; }
        public int BrojStrana { get; set; }
    }
}

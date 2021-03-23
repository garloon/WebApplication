using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public int GenreId { get; set; }
        [NotMapped]
        public string Genre { get; set; }
        public int AuthorId { get; set; }
        [NotMapped]
        public string Author { get; set; }
        public string Summary { get; set; }
    }
}

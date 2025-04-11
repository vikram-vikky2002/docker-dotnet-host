using System.ComponentModel.DataAnnotations;

namespace BookMyMovieWebServices.Models
{
    public class Theater
    {
        [Required] 
        public string Location { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int TheaterId { get; set; }
        [Required]
        public int TotalSeats { get; set; }

    }
}

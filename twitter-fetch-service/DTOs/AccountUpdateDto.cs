using System.ComponentModel.DataAnnotations;

namespace twitter_fetch_service.DTOs
{
    public class AccountUpdateDto
    {
        [Required]
        public int Id { get; set;}
        public string NewName { get; set; }
        public string OldName { get; set; }
    }
}

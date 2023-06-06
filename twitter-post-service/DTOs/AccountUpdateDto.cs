using System.ComponentModel.DataAnnotations;

namespace twitter_post_service.DTOs
{
    public class AccountUpdateDto
    {
        [Required]
        public int Id { get; set;}
        public string NewName { get; set; }
        public string OldName { get; set; }
    }
}

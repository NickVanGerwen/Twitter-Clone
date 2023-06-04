﻿using System.ComponentModel.DataAnnotations;

namespace twitter_account_service.DTOs
{
    public class AccountUpdateDto
    {
        [Required]
        public int Id { get; set;}
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

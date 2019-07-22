using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _20_MVC_Assignment_01.Models
{
    public class TweetViewModels
    {
        [Required]
        public string Message { get; set; }
    }
}
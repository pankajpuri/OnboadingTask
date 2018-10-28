using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboadingTask.Models
{
    public class ProductSold
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateSold { get; set; }


    }
}
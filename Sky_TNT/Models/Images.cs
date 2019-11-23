using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bai5_Anh.Models
{
    public class Images
    {
        [Key]
        public int IdImages { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Introduce { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateSubmit { get; set; }
        public string Email { get; set; }
        public int CountView { get; set; }
        [Required]
        public int IdGroupImage { get; set; }

        [ForeignKey("IdGroupImage")]
        public virtual GroupImages Images_GroupImages { get; set; }
    }
}
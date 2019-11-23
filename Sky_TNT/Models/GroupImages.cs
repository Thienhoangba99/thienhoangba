using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bai5_Anh.Models
{
    public class GroupImages
    {
        [Key]
        public int IdGroupImage { get; set; }
        [Required]
        public string NameGroup { get; set; }

        public virtual ICollection<Images> GroupImages_Images { get; set; }
    }
}
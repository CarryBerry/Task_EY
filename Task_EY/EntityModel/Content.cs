using System;
using System.ComponentModel.DataAnnotations;

namespace Task_EY.EntityModel
{
    public class Content
    {
        [Key]
        public int ContentId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateLine { get; set; }

        [StringLength(10, ErrorMessage = "Line must be 10 characters.")]
        [Display(Name = "Latin line")]
        public string LatinLine { get; set; }

        [StringLength(10, ErrorMessage = "Line must be 10 characters.")]
        [Display(Name = "Cyrillic char")]
        public string CyrillicLine { get; set; }
        
        [Range(1, 100000000)]
        [Display(Name = "Even integer number")]
        public int IntLine { get; set; }

        //[Range(1, 20)]
        [Display(Name = "Float number")]
        public string DoubleLine { get; set; }
    }
}

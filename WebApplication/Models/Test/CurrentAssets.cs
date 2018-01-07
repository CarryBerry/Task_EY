using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class CurrentAssets /*: DTO*/
    {
        [Key]
        public int CurrentAssetsId { get; set; } 

        public int AccountGroup { get; set; }
        public int AccountUnit { get; set; }

        //public int ClassId { get; set; }

        //public int ClassAsset { get; set; }

        //public int ClassLiability { get; set; }

        [Required]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public string Asset { get; set; }

        [Required]
        //[Range(0.1, 10000, ErrorMessage = "Price must be from {1} to {2}.")]
        public string Liability { get; set; }

        public ICollection<AccountField> Fields;
        //public ICollection<AccountClass> AccountClasses;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class IncomingBalance /*: DTO*/
    {
        [Key]
        public int IncomingBalanceId { get; set; }

        public int AccountGroup { get; set; }
        public int AccountUnit { get; set; }

        //public int IdGroup { get; set; }

        //public int Class { get; set; }

        [Required]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public string Asset { get; set; }
        public string Liability { get; set; }

        public ICollection<AccountField> Fields;
        //public ICollection<AccountClass> AccountClasses;
    }
}
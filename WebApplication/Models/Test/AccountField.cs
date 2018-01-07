using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class AccountField /*: DTO*/
    {
        [Key]
        public int AccountFieldId { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime OrderDate { get; set; }

        //[Required]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public int FieldAccountUnit { get; set; }

        public string IncomingBalanceFieldAsset { get; set; }

        public string IncomingBalanceFieldLiability { get; set; }

        public string CurrentAssetsFieldAsset { get; set; }

        public string CurrentAssetsFieldLiability { get; set; }

        public string OutgoingBalanceFieldAsset { get; set; }

        public string OutgoingBalanceFieldLiability { get; set; }

        public int UploadedFileInfoId { get; set; }

        public int CurrentAssetsId { get; set; }

        //[Required]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between {1} and {2} characters.")]
        public int IncomingBalanceId { get; set; }

        //[Required]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "The {0} name must be between {1} and {2} characters.")]
        public int OutgoingBalanceId { get; set; }

        public int AccountClassId { get; set; }

        public int AccountGroupSumId { get; set; }

        //[Required]
        //[Range(1, 100, ErrorMessage = "The {0} must be from {1} to {2}.")]
        //public int Amount { get; set; }

        //[Required]
        //[Range(0.1, 1000000, ErrorMessage = "The {0} must be from {1} to {2}.")]
        //public double Price { get; set; }
    }
}
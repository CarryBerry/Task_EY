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
        
        public int FieldAccountUnit { get; set; }

        public string IncomingBalanceFieldAsset { get; set; }

        public string IncomingBalanceFieldLiability { get; set; }

        public string CurrentAssetsFieldAsset { get; set; }

        public string CurrentAssetsFieldLiability { get; set; }

        public string OutgoingBalanceFieldAsset { get; set; }

        public string OutgoingBalanceFieldLiability { get; set; }

        public int UploadedFileInfoId { get; set; }

        public int CurrentAssetsId { get; set; }

        public int IncomingBalanceId { get; set; }

        public int OutgoingBalanceId { get; set; }

        public int AccountClassId { get; set; }

        public int AccountGroupSumId { get; set; }
    }
}
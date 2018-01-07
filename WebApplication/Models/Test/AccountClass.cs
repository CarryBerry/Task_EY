using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class AccountClass
    {
        [Key]
        public int AccountClassId { get; set; }

        public string IncomingBalanceClassAsset { get; set; }

        public string IncomingBalanceClassLiability { get; set; }

        public string CurrentAssetsClassAsset { get; set; }

        public string CurrentAssetsClassLiability { get; set; }

        public string OutgoingBalanceClassAsset { get; set; }

        public string OutgoingBalanceClassLiability { get; set; }

        public ICollection<AccountField> Fields;
    }
}
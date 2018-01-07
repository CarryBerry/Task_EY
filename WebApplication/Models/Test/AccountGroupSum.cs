using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.Test
{
    public class AccountGroupSum
    {
        [Key]
        public int AccountGroupSumId { get; set; }

        public string IncomingBalanceAccountGroupAsset { get; set; }

        public string IncomingBalanceAccountGroupLiability { get; set; }

        public string CurrentAssetsAccountGroupAsset { get; set; }

        public string CurrentAssetsAccountGroupLiability { get; set; }

        public string OutgoingBalanceAccountGroupAsset { get; set; }

        public string OutgoingBalanceAccountGroupLiability { get; set; }

        public ICollection<AccountField> Fields;
    }
}

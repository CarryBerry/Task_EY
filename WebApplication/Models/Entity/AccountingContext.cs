using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class AccountingContext : DbContext
    {
        public AccountingContext() : base("name=DefaultConnection") { }

        public DbSet<AccountClass> AccountClasses { get; set; }

        public DbSet<CurrentAssets> CurrentAssets { get; set; }

        public DbSet<IncomingBalance> IncomingBalance { get; set; }

        public DbSet<AccountField> Fields { get; set; }

        public DbSet<OutgoingBalance> OutgoingBalance { get; set; }

        public DbSet<AccountGroupSum> AccountGroupSums { get; set; }

        public DbSet<UploadedFileInfo> UploadedFiles { get; set; }
    }
}
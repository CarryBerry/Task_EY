﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class CurrentAssets 
    {
        [Key]
        public int CurrentAssetsId { get; set; } 

        public int AccountGroup { get; set; }

        public int AccountUnit { get; set; }
        
        public string Asset { get; set; }
        
        public string Liability { get; set; }

        public ICollection<AccountField> Fields;
    }
}
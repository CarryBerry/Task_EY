using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class UploadedFileInfo
    {
        [Key]
        public int UploadedFileInfoId { get; set; }

        public string FileName { get; set; }
        
        public string FilePath { get; set; }

        public ICollection<AccountField> Fields;
    }
}
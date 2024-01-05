using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShortenURL.Data.Models
{
    [Table("OriginalUrl")]
    [Index(nameof(OriginalLink), IsUnique = true)]
    public class OriginalUrl
    {
        [Key]
        public int OriginalLinkId { get; set; }
        [Required]
        public string OriginalLink { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortenURL.Data.Models
{
    [Table("NewUrl")]
    public class NewUrl
    {
        [Key]
        public int NewLinkId { get; set; }
        [Required]
        public string NewLink { get; set; }

        // Foreign key property
        public int OriginalLinkId { get; set; }

        [ForeignKey("OriginalLinkId")]
        public OriginalUrl OriginalUrl { get; set; }
    }
}

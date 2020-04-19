using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtectedFiles.Data.Entities
{
    [Table("cms_ProtectedFiles")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [MaxLength(8)]
        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; }
    }
}

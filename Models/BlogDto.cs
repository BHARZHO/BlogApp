using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogApp.Models;

    public class BlogDto
    {
        [Key]
        public int blogId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Content must not be exceeding 1000 character")]
        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        public string content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date publish")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //Date format
        public DataType datePublish { get; set; }

        [Display(Name = "Author")]
        [RegularExpression(@"\w{1,}")]
        public int authorId { get; set; }

        [Display(Name = "Date created")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] // Date format
        [Column(TypeName = "DateTime2")]
        public DateTime? dateCreated { get; set; }

        [Display(Name = "Date Modified")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] // Date format
        [Column(TypeName = "DateTime2")]
        public DateTime? dateModified { get; set; }
    }

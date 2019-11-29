using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_users.Repository.Models
{

    [Table("users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [StringLength(50)]
        [Column("username")]
        public string UserName { get; set; }

        [StringLength(60)]
        [Column("password")]
        public string PassWord { get; set; }
        
        [Column("role")]
        public string Role { get; set; }

        [Column("active")] 
        public bool Active { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Online_Shopping.DomainModel
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ResetCode { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}

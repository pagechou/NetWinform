using LinqToDB.Mapping;
using System;
using System.Collections.Generic; 

namespace App.Models
{
    [Table(Name = "tb_Users")]
    public partial class Tb_Users
    {
        [Column(Name = "UserId"),NotNull]
        public int UserId { get; set; }
        [Column(Name = "UserCode"),NotNull]
        public string UserCode { get; set; }
        [Column(Name = "UserName")]
        public string UserName { get; set; }
        [Column(Name = "Password")]
        public string Password { get; set; }
        [Column(Name = "Description")]
        public string Description { get; set; }
        [Column(Name = "CreateBy")]
        public string CreateBy { get; set; }
        [Column(Name = "CreateDate")]
        public DateTime? CreateDate { get; set; }
        [Column(Name = "ModifiedBy")]
        public string ModifiedBy { get; set; }
        [Column(Name = "ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }
        [Column(Name = "IsActive"),NotNull]
        public Boolean IsActive { get; set; }
        [Column(Name = "Email")]
        public string Email { get; set; }
        [Column(Name = "Tel")]
        public string Tel { get; set; }
        [Column(Name = "Product")]
        public string Product { get; set; }
        [Column(Name = "Att1")]
        public string Att1 { get; set; }
        [Column(Name = "Att2")]
        public string Att2 { get; set; }
        [Column(Name = "Att3")]
        public string Att3 { get; set; }
        [Column(Name = "Att4")]
        public string Att4 { get; set; }
        [Column(Name = "Att5")]
        public string Att5 { get; set; }
        [Column(Name = "Att6")]
        public string Att6 { get; set; }
        [Column(Name = "Att7")]
        public string Att7 { get; set; }
    }
}

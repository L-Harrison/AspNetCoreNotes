using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EFcore.Model
{

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime Bytes { get; set; }
        public string RowVersionStr => $"0x{BitConverter.ToUInt64(RowVersion.Reverse().ToArray(), 0).ToString()}";
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
    public class Orginazation
    {
        public int Id { get; set; }
        public string OrgNum { get; set; }
        public string  Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
    public class User
    {
        [DatabaseGenerated(databaseGeneratedOption:DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Addr { get; set; }
        public string dec { get; set; }
        //public string deca { get; set; }
        //public string decaq { get; set; }
        //[DatabaseGenerated(databaseGeneratedOption:DatabaseGeneratedOption.Computed)]
        public DateTime Birth { get; set; }
        [Timestamp]
        public byte[] timespan { get; set; }
        public int OrginazationId { get; set; }
        public virtual Orginazation Orginazation { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
    public class UserRole
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

    }

    public class Role
    {
        public int RoleId { set; get; }
        public string RName { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CityCompany> CityCompanies { get; set; }
        public virtual ICollection<Mayor> Mayor { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EstablishDate { get; set; }
        public  string LegalPerson { get; set; }
        public virtual ICollection<CityCompany> CityCompanies { get; set; }
    }

   public class CityCompany
    {
        public int CityID { get; set; }
        public int CompanyID { get; set; }
        public virtual City City{ get; set; }
        public virtual Company Company { get; set; }
    }

  public class Mayor
   {
       public int ID { get; set; }
       public string Name { get; set; }
       public int Age { get; set; }
       public int CityID { get; set; }
       public DateTime Birth { get; set; }
       public Gender Gender { get; set; }
       public virtual City City { get; set; }
       
   }

  public class Word
  {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Author { get; set; }
      public int ContentsId { get; set; }
      public virtual  Contents  Contents{ get; set; }
      public DateTime PublishTime { get; set; }
  }

  public class Contents
  {
      public int Id { get; set; }
      public string Text { get; set; }
      public int Size { get; set; }
      public string IKeys { get; set; }
      public int WordId { get; set; }
      public virtual Word Word { get; set; }
    }
  public enum Gender
  {
      Female=0,
      Male=1
  }
}

using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeGamer.Core.Entity
{
    public class Person : IdKeyIdentity
    {
        public Person()
        {
            Posts = new HashSet<Post>();
            Friends = new HashSet<Friend>();
            FriendSolicited = new HashSet<Friend>();
        }

        [Display(Name = "Nome")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "SobreNome")]
        public string LastName { get; set; }


        [Display(Name = "Data de Nascimento")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "A data tem que ter um formato dd/MM/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateBirth { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Friend> FriendSolicited { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Like Like { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual Login Login { get; set; }
    }
}

using SomeGamer.Core.Enum;
using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeGamer.Core.Entity
{
    public class Profile : IdKeyIdentity
    {
        public Profile()
        {
            Likes = new HashSet<Like>();
            Comments = new HashSet<Comment>();
        }
        public string Bio { get; set; }

        [Display(Name = "Privacidade")]
        Privacity Privacity { get; set; }

        [Display(Name = "Imagem")]
        public virtual Image ImageProfile { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

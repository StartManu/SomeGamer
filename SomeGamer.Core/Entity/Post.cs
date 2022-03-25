using SomeGamer.Core.Enum;
using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeGamer.Core.Entity
{
    public class Post : IdKeyIdentity
    {
        public Post()
        {
            Likes = new HashSet<Like>();
            Comments = new HashSet<Comment>();

        }
        [Display(Name = "Imagem")]
        public virtual Image Image { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        public int limiteChar = 400;


        [Display(Name = "Data de publicação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated
        {
            get { return DateCreated; }
            set { DateCreated = DateTime.UtcNow; }
        }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        [Display(Name = "Privacidade")]
        Privacity Privacity { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}

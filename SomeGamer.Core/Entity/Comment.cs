using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeGamer.Core.Entity
{
    public class Comment : IdKeyIdentity
    {
        public string Description { get; set; }

        public int limiteChar = 400;

        [Display(Name = "Data de publicação")]
        public DateTime DateCreated
        {
            get { return DateCreated; }
            set { DateCreated = DateTime.UtcNow; }
        }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}

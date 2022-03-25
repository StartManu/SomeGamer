using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SomeGamer.Core.Entity
{
    public class Like : IdKeyIdentity
    {
        public bool Fancy { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}

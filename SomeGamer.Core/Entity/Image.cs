using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeGamer.Core.Entity
{
    public class Image : IdKeyIdentity
    {
        [DataType(DataType.ImageUrl)]
        public string PathImage { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeGamer.Core.Util
{
    public class IdKeyIdentity
    {
        [Key]
        public int Id { get; set; }
    }
}

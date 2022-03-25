using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SomeGamer.Core.Entity
{
    public class Friend : IdKeyIdentity
    {
        public string Chat { get; set; }
        public int PersonId1 { get; set; }
        public Person Person1 { get; set; }
        public int PersonId2 { get; set; }
        public Person Person2 { get; set; }
    }
}

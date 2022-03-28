using Microsoft.AspNetCore.Identity;
using SomeGamer.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SomeGamer.Data.Identity
{
    public class User : IdentityUser
    {
         public Person Person { get; set; }
    }
}

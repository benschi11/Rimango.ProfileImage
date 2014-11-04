using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.ProfileImage.Models
{
    using Orchard.ContentManagement.Records;

    public class ProfileImagePartRecord : ContentPartRecord
    {
        public virtual int PlattformUserId { get; set; }

    }
}
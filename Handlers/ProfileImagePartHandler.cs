using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.ProfileImage.Handlers
{
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;

    using Rimango.ProfileImage.Models;

    public class ProfileImagePartHandler : ContentHandler
    {
        public ProfileImagePartHandler(IRepository<ProfileImagePartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
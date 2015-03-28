using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace Rimango.ProfileImage.Models
{
    public class ProfileImagePartRecord : ContentPartRecord
    {
        public virtual int PlattformUserId { get; set; }

    }
}
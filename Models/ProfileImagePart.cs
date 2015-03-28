using System.Linq;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Rimango.ProfileImage.Models
{
    public class ProfileImagePart : ContentPart<ProfileImagePartRecord>
    {
        public bool HasProfileImage
        {
            get
            {
                return (this.GetPartImageField() != null);
            }
        }

        public int PlattformUserId
        {
            get
            {
                return Record.PlattformUserId;
            }

            set
            {
                Record.PlattformUserId = value;
            }
        }

        private ImageField.Fields.ImageField GetPartImageField()
        {
            return (ImageField.Fields.ImageField)this.Fields.FirstOrDefault(f => f.Name == Globals.ImageFieldName);
        }

        public string PublicImageUrl
        {
            get
            {
                return (this.GetPartImageField() != null) ? this.GetPartImageField().FileName : string.Empty;
            }
        }
    }
}
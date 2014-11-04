using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.ProfileImage.Models
{
    using Orchard.ContentManagement;

    using Rimango.ImageField.Fields;

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

        private ImageField GetPartImageField()
        {
            return (ImageField)this.Fields.FirstOrDefault(f => f.Name == Globals.ImageFieldName);
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
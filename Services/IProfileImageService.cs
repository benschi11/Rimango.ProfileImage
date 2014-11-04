using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimango.ProfileImage.Services
{
    using Orchard;

    public interface IProfileImageService : IDependency
    {
        void SetProfileImage(byte[] image, int userId);

        string GetProfileImageUrl(int userId);
    }
}

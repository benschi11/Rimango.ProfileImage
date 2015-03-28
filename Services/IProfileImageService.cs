using Orchard;
using Orchard.Environment.Extensions;

namespace Rimango.ProfileImage.Services
{
    public interface IProfileImageService : IDependency
    {
        void SetProfileImage(byte[] image, int userId);

        string GetProfileImageUrl(int userId);
    }
}

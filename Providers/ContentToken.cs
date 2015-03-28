using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Tokens;
using Rimango.ProfileImage.Models;

namespace Rimango.ProfileImage.Providers
{
    public class ContentToken : ITokenProvider
    {
        private IContentManager _contentManager;

        public Localizer T { get; set; }

        public ContentToken(IContentManager contentManger)
        {
            _contentManager = contentManger;
            T = NullLocalizer.Instance;
        }
        public void Describe(DescribeContext context)
        {
            context.For("Content", T("Content Items"), T("Content Items"))
               .Token("ProfileImagePart", T("ProfileImagePart"), T("Gets the ProfileImagePart"));
        }

        public void Evaluate(EvaluateContext context)
        {
            context.For<IContent>("Content")
                .Token(
                    "ProfileImagePart",
                    content => content.As<ProfileImagePart>() ?? null);
        }
    }
}
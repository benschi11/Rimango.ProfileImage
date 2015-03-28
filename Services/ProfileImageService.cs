using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Logging;
using Orchard.MediaLibrary.Services;
using Orchard.Tokens;
using Rimango.ImageField.Settings;

namespace Rimango.ProfileImage.Services
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly IContentManager _contentManager;

        private readonly IMediaLibraryService _mediaLibraryService;

        private readonly ITokenizer _tokenizer;

        private readonly ILogger _logger;

        public ProfileImageService(
            IContentManager contentManager, 
            IMediaLibraryService mediaLibraryService, 
            ITokenizer tokenizer,
            ILogger logger) {
            _contentManager = contentManager;
            _mediaLibraryService = mediaLibraryService;
            _tokenizer = tokenizer;
            _logger = logger;
        }


        public void SetProfileImage(byte[] image, int userId)
        {
            var contentItem = _contentManager.Get(userId);
            var imageField = GetProfileImageFieldFromContentItem(contentItem);

            var settings = imageField.PartFieldDefinition.Settings.GetModel<ImageFieldSettings>();

            // TODO: make tokens compatible with standard token in the ImageField
            var tokenDict = new Dictionary<string, object>()
                                {
                                    { "content-type", contentItem.ContentType },
                                    { "content-item-id", contentItem.Id },
                                    { "field-name", imageField.Name }
                                };

            try
            {
                var path = _tokenizer.Replace(settings.MediaFolder, tokenDict);
                _mediaLibraryService.UploadMediaFile(path, Path.GetFileName(imageField.FileName), image);
            }
            catch (Exception exc)
            {
                _logger.Error(exc, "Error in ProfileImageService. "+ exc.Message);
                throw;
            }
            

           

        }

        private static ImageField.Fields.ImageField GetProfileImageFieldFromContentItem(ContentItem contentItem)
        {
            var profileImagePart = contentItem.Parts.FirstOrDefault(p => p.PartDefinition.Name == Globals.PartName);

            if (profileImagePart == null)
            {
                throw new Exception(
                    string.Format("{0} not attached to Contenttype {1}", Globals.PartName, contentItem.ContentType));
            }

            var imageField =
                profileImagePart.Fields.FirstOrDefault(f => f.Name == Globals.ImageFieldName) as
                ImageField.Fields.ImageField;

            if (imageField == null)
            {
                throw new Exception(string.Format("No {0} found in Part '{1}'", Globals.ImageFieldName, Globals.PartName));
            }

            return imageField;
        }

        public string GetProfileImageUrl(int userId)
        {
            var contentItem = _contentManager.Get(userId);
            var imageField = GetProfileImageFieldFromContentItem(contentItem);

            return imageField.FileName;


        }
    }
}
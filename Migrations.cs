using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Rimango.ProfileImage {
    public class Migrations : DataMigrationImpl {

        public int Create()
        {
            SchemaBuilder.CreateTable(
                "ProfileImagePartRecord",
                table => table.ContentPartRecord().Column<int>("PlattformUserId"));

            this.ContentDefinitionManager.AlterPartDefinition(Globals.PartName, 
                    p => p  .WithField(Globals.ImageFieldName, 
                                                f =>f   .OfType(Globals.ImageFieldTypeName)
                                                        .WithSetting("ImageFieldSettings.MaxHeight", "200")
                                                        .WithSetting("ImageFieldSettings.MaxWidth", "200")
                                                        .WithSetting("ImageFieldSettings.MediaFolder", "_profileImages/{content-item-id}")
                                                        .WithSetting("ImageFieldSettings.AlternativeText", "False")
                                                        .WithSetting("ImageFieldSettings.ResizeAction", "UserCrop")
                                                        .WithSetting("ImageFieldSettings.UserCropOption", "OnlyKeepRatio")
                                                        .WithSetting("ImageFieldSettings.Required", "False")
                                                        ).Attachable()
                        );

            ContentDefinitionManager.AlterTypeDefinition("User",
                cfg => cfg
                    .WithPart(Globals.PartName)
                );

            return 1;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition(Globals.PartName,
                    p => p.WithField(Globals.ImageFieldName,
                                                f => f.OfType(Globals.ImageFieldTypeName)
                                                        .WithSetting("ImageFieldSettings.DefaultImage", "http://www.gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50?f=y&d=mm")
                                                        )
                        );

            return 2;
        }
    }
}
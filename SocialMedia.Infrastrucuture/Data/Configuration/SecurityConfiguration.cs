using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastrucuture.Data.Configuration.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastrucuture.Data.Configuration
{
    public class SecurityConfiguration : ISecurityConfiguration
    {
        public void Configure(EntityTypeBuilder<Security> builder)
        {
           
        }
    }
}

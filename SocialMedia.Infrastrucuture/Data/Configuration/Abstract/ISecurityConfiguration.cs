using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastrucuture.Data.Configuration.Abstract
{
    public interface ISecurityConfiguration: IEntityTypeConfiguration<Security>
    {
    }
}

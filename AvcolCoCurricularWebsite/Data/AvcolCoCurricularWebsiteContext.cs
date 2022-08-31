using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Data
{
    public class AvcolCoCurricularWebsiteContext : DbContext
    {
        public AvcolCoCurricularWebsiteContext (DbContextOptions<AvcolCoCurricularWebsiteContext> options)
            : base(options)
        {
        }
    }
}

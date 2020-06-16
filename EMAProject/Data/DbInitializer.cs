using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ClinicContext context) 
        {
            context.Database.EnsureCreated();
        }
    }
}

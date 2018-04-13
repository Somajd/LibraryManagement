using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repository
{
    public class JenkinPipelineRepository : Repository<JenkinPipeline>, IJenkinPipelineRepository
    {
        public JenkinPipelineRepository(LibraryDbContext context) :base(context)
        {
                
        }

      
    }
}

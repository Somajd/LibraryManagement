using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class JenkinPipelineController: Controller
    {
        private readonly IJenkinPipelineRepository _jenkinPipelineRepository;
        

        public JenkinPipelineController(IJenkinPipelineRepository jenkinPipelineRepository)
        {
            _jenkinPipelineRepository = jenkinPipelineRepository;
      
        }

        [Route("Jenkins")]
        public IActionResult List()
        {

            var jPs = _jenkinPipelineRepository.GetAll();

            if (jPs.Count() == 0)
            {
                return View("Empty");
            
            }
            return View(jPs);
        }
    }
}

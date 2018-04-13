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
    public class PipelineController: Controller
    {
        private readonly IPipelineRepository _pipelineRepository;
        private readonly ICatalogRepository _catalogRepository;

        public PipelineController(IPipelineRepository pipelineRepository, ICatalogRepository catalogRepository)
        {
            _pipelineRepository = pipelineRepository;
            _catalogRepository = catalogRepository;


        }

       

        public  IActionResult Create( int id)
        {
            var pvm = new PipelineViewModel();
            var catalog = _catalogRepository.GetById(id);
            pvm.Catalog = catalog;
            var jpl = new Pipeline();
            jpl.CatalogId = id;
            pvm.Pipeline = jpl;

            return View(pvm);
        }

        [HttpPost]
        public IActionResult Create(PipelineViewModel pvm)
        {
            var cat = _catalogRepository.GetById(pvm.Catalog.CatalogId);

            var pl1 = pvm.Pipeline;
            pl1.Catalog = cat;

            if (pl1.JenkinsWebAPI != null)
                pl1.JenkinsWebAPI = pl1.JenkinsWebAPI.Trim().ToString();

            if (pl1.JenkinsWebAPI != null)
                if (pl1.JenkinsWebAPI.Length > 0 && pl1.JenkinsWebAPI.Substring(pl1.JenkinsWebAPI.Length - 1) != "/")
                pl1.JenkinsWebAPI += "/";

            if (pl1.JenkinsWebAPI != null)
                if (pl1.JenkinsWebAPI.Length > 0 && pl1.JenkinsWebAPI.Substring(pl1.JenkinsWebAPI.Length - 4) != "job/")
                    pl1.JenkinsWebAPI += "job/";

            if (pl1.ServiceNowWebAPI != null)
                pl1.ServiceNowWebAPI = pl1.ServiceNowWebAPI.Trim().ToString();

            if (pl1.ServiceNowWebAPI != null)
                if(pl1.ServiceNowWebAPI.Length > 0 && pl1.ServiceNowWebAPI.Substring(pl1.ServiceNowWebAPI.Length - 1) != "/")
                pl1.ServiceNowWebAPI += "/";

            _pipelineRepository.Create(pl1);
            
            return RedirectToRoute(new
            {
                controller = "Catalog",
                action = "View",
                id = cat.CatalogId
            });
        }

       
        public IActionResult Delete(int id)
        {
            var pipeline = _pipelineRepository.GetById(id);
            _pipelineRepository.Delete(pipeline);

            return RedirectToRoute(new
            {
                controller = "Catalog",
                action = "View",
                id = pipeline.CatalogId
            });



        }

        //public IActionResult Delete(int id)
        //{
        //    var pipeline = _pipelineRepository.GetById(id);
        //    _pipelineRepository.Delete(pipeline);

        //    return RedirectToRoute(new
        //    {
        //        controller = "Catalog",
        //        action = "View",
        //        id = pipeline.CatalogId
        //    });



        //}
    }
}

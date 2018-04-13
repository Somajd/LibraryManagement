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
    public class CatalogController: Controller
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IPipelineRepository _pipelineRepository;

        public CatalogController(ICatalogRepository catalogRepository, IPipelineRepository pipelineRepository)
        {
            _catalogRepository = catalogRepository;
            _pipelineRepository = pipelineRepository;

        }

        //[Route("Catalog")]
        public IActionResult List()
        {

            var catalogVM = new List<CatalogViewModel>();

            var catalogs = _catalogRepository.GetAll();

            if (catalogs.Count() == 0)
            {
                return View("Empty");
            

            }

            foreach (var catalog in catalogs)
            {
                catalogVM.Add(new CatalogViewModel
                {
                    Catalog = catalog,
                    PipelineCount = _pipelineRepository.Count(x => x.CatalogId == catalog.CatalogId)
                });
            }

            return View(catalogVM);
        }


        [Route("Catalog")]
        public IActionResult ListTile()
        {

            var catalogVM = new List<CatalogViewModel>();

            var catalogs = _catalogRepository.GetAll();

            if (catalogs.Count() == 0)
            {
                return View("Empty");


            }

            foreach (var catalog in catalogs)
            {
                catalogVM.Add(new CatalogViewModel
                {
                    Catalog = catalog,
                    PipelineCount = _pipelineRepository.Count(x => x.CatalogId == catalog.CatalogId)
                });
            }

            return View(catalogVM);
        }

        public IActionResult Delete(int id)
        {
            var catalog = _catalogRepository.GetById(id);
            _catalogRepository.Delete(catalog);

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            var cvm = new CatalogViewModel();
          
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Create(CatalogViewModel cvm)
        {
            _catalogRepository.Create(cvm.Catalog);
            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var cvm = new CatalogViewModel();
            cvm.Catalog = _catalogRepository.GetById(id);
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Update(CatalogViewModel cvm)
        {
            _catalogRepository.Update(cvm.Catalog);
            return RedirectToAction("ListTile");
        }

        public IActionResult Launch(int id)
        {
            var cvm = new CatalogViewModel();
            cvm.Catalog =    _catalogRepository.GetById(id);
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Launch(Catalog catalog)
        {
            _catalogRepository.OrderCatalog(catalog);
            return RedirectToAction("List");
        }

        public IActionResult View(int id)
        {
            
            var catalog = _catalogRepository.GetCatalogWithPipelines(id);
            return View(catalog.First());
        }

    }
}

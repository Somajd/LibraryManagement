using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using LibraryManagement.Models;
using LibraryManagement.Utils;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class OrderController:Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IPipelineRepository _pipelineRepository;
        private readonly JenkinsSettings _jenkinsSettings ;

        public OrderController(IOrderRepository orderRepository, ICatalogRepository catalogRepository, IPipelineRepository pipelineRepository , IOptions<JenkinsSettings> jenkinsSettings)
        {
            _orderRepository = orderRepository;
            _catalogRepository = catalogRepository;
            _pipelineRepository = pipelineRepository;
            _jenkinsSettings = jenkinsSettings.Value;

        }

        [Route("Order")]
        public IActionResult List()
        {
            var orders = _orderRepository.GetAllAvailableCatalog();

            if (orders.Count() == 0)
            {
                return View("Empty");


            }
            return View(orders);
        }

        public IActionResult Create(int Id)
        {
            
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.Catalog = _catalogRepository.GetById(Id);
            
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Create(OrderViewModel ovm)
        {
            var catalog = _catalogRepository.GetById(ovm.Catalog.CatalogId);

            var order = ovm.Order;
            order.Status = eOrderStatus.NotStarted;
            order.Catalog = catalog;

            _orderRepository.Create(order);

            LaunchJenkinsPipeline(catalog, order);

            return RedirectToAction("List");
        }

        public IActionResult ReLaunch(int id)
        {
            var ord = _orderRepository.GetOrderWithCatalog(id);

            var order = ord.First();
            var catalog = order.Catalog;

            LaunchJenkinsPipeline(catalog, order);

            return RedirectToAction("List");
        }


        public void LaunchJenkinsPipeline(Catalog catalog, Order order)
        {

            var pipeline = _pipelineRepository.GetAllWithCatalog(catalog.CatalogId);
            if (pipeline.Count() > 0)
            {
                var p1 = pipeline.First();
                String errorText = "";

                order.JenkinsProjectName = p1.JenkinsPipelineName;

                if (p1.JenkinsWebAPI.Length > 0)
                {

                    RESTClient rClient = new RESTClient();
                    //rClient.endPoint = p1.JenkinsWebAPI + p1.JenkinsPipelineName + "/build?delay=0sec";
                    rClient.authType = autheticationType.Basic;
                    //rClient.httpMethod = httpVerb.POST;
                    rClient.userName = _jenkinsSettings.UID;
                    rClient.userPassword = _jenkinsSettings.PWD;

                    //..Getting the Next Build Number for this Jenkins Project
                    rClient.endPoint = p1.JenkinsWebAPI + p1.JenkinsPipelineName + "/api/xml/";
                    rClient.httpMethod = httpVerb.GET;
                    int nextBuildNumber = rClient.GetNextBuildNumber( ref errorText);
                    if (errorText.Length > 0)
                    {
                        order.JenkinsDetails += "Connecting to URL :" + rClient.endPoint;
                        order.JenkinsDetails += "Error connecting to Jenkins REST API \n Getting Next Build Number Failed:\n" + errorText;
                        _orderRepository.Update(order);
                        return;
                    }


                    //..Start the Build 
                    rClient.endPoint = p1.JenkinsWebAPI + p1.JenkinsPipelineName + "/build?delay=0sec";
                    rClient.httpMethod = httpVerb.POST;
                    if (!rClient.StartBuild(ref errorText))
                    {
                        order.JenkinsDetails += "Error connecting to Jenkins REST API \n Starting Build  Failed:" + errorText;
                        _orderRepository.Update(order);
                        return;
                    }


                    //..Getting the Next Build Number for this Jenkins Project
                    rClient.endPoint = p1.JenkinsWebAPI + p1.JenkinsPipelineName + "/api/xml/";
                    rClient.httpMethod = httpVerb.GET;
                    int nextBuildNumber2 = rClient.GetNextBuildNumber(ref errorText);
                    if (errorText.Length > 0)
                    {
                        order.JenkinsDetails += "Error connecting to Jenkins REST API \n Getting Next Build Number Failed:" + errorText;
                        _orderRepository.Update(order);
                        return;
                    }

                    if (nextBuildNumber != nextBuildNumber2)
                    {

                        order.JenkinsBuildNumber = nextBuildNumber.ToString();
                        order.JenkinsDetails += "Jenkin Build Started - Build number is " + nextBuildNumber.ToString();

                        order.Status = eOrderStatus.InProgress;
                    }
                }
                _orderRepository.Update(order);
                return;
            }
        }


        public IActionResult View(int Id)
        {

            var orders = _orderRepository.GetOrderWithCatalog(Id);
            var or = orders.First();

            if (or.Status != eOrderStatus.Completed)
            {
                var pipelines = _pipelineRepository.GetAllWithCatalog(or.CatalogId);
                var p1 = pipelines.First();

                RESTClient rClient = new RESTClient();
                rClient.authType = autheticationType.Basic;
                rClient.userName = _jenkinsSettings.UID;
                rClient.userPassword = _jenkinsSettings.PWD;

                //..Getting the Next Build Number for this Jenkins Project
                rClient.endPoint = p1.JenkinsWebAPI + p1.JenkinsPipelineName + "/" + or.JenkinsBuildNumber + "/api/xml/";
                rClient.httpMethod = httpVerb.GET;
                string result = rClient.GetJenkinStatus();

                if (result == "SUCCESS")
                {
                    or.Status = eOrderStatus.Completed;
                    or.JenkinsDetails += "/nJenkins build completed!";
                }else if (result == "FAILURE")
                {
                    or.Status = eOrderStatus.ErroredOut;
                    or.JenkinsDetails += "/nJenkins build failed!";
                }

                rClient.endPoint = p1.JenkinsWebAPI + p1.JenkinsPipelineName + "/" + or.JenkinsBuildNumber + "/consoleText";
                rClient.httpMethod = httpVerb.GET;
                string errorText = "";
                string consoleText = rClient.GetJenkinConsoleText(ref errorText);
                if (errorText.Length == 0)
                {
                    or.JenkinsConsoleText = "Status :" + result + "\n" + consoleText;
                }
                else
                {
                    or.JenkinsDetails += "Error getting Jenkin Project Console Output";
                }

                _orderRepository.Update(or);
            }
            return View(or);
        }





    }
}

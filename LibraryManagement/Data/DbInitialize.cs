using LibraryManagement.Data.Model;
using LibraryManagement.Models;
using LibraryManagement.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class DbInitialize
    {
        public readonly JenkinsSettings _jenkinsSettings;

        public static void Seed(IApplicationBuilder app,  IOptions<JenkinsSettings> jenkinSettings)
        {
           // _jenkinsSettings = jenkinSettings.Value;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();

                //Add Catalog
                var cat1 = new Catalog {
                    Name = "Drupal on AWS - Small" ,
                    Description ="Drupal instance for Dev/Test purpose, with single instance deployment" ,
                    PublishedStatus =ePublishedStatus.Yes,
                    Icon="drupal.png",
                    Pipelines = new List<Pipeline>()
                    //Add Pipelines
                    { new Pipeline
                    {
                    Name = "JenkinsDrupalSmall",
                    Description = "Jenkins pipeline to create Drupal Small",
                    JenkinsPipelineName = "petClinicProject",
                    JenkinsWebAPI = jenkinSettings.Value.ServerURL.ToString() + "/job/",
                    ServiceNowWebAPI = "something here"
                }
                    }
                };
                var cat2 = new Catalog {
                    Name = "Drupal on AWS - Medium",
                    Description = "Drupal instance for SIT/UAT purpose, with 2-tier deployment" ,
                    PublishedStatus = ePublishedStatus.Yes ,
                    Icon = "drupal.png",
                    Pipelines = new List<Pipeline>()
                    //Add Pipelines
                    { new Pipeline
                    {
                    Name = "JenkinsDrupalMedium",
                    Description = "Jenkins pipeline to create Drupal Medium",
                    JenkinsPipelineName = "petClinicProject",
                    JenkinsWebAPI = jenkinSettings.Value.ServerURL.ToString() + "/job/",
                    ServiceNowWebAPI = "something here"
                }
                    }


                };
                var cat3 = new Catalog {
                    Name = "Drupal on AWS - Large",
                    Description = "Drupal instance for Pre-Prod/Prod purpose, with full scale deployment",
                    PublishedStatus = ePublishedStatus.No,
                    Icon = "drupal.png",
                    Pipelines = new List<Pipeline>()
                    //Add Pipelines
                    { new Pipeline
                    {
                    Name = "JenkinsDrupalLarge",
                    Description = "Jenkins pipeline to create Drupal Large",
                    JenkinsPipelineName = "petClinicProject",
                    JenkinsWebAPI = jenkinSettings.Value.ServerURL.ToString() + "/job/",
                    ServiceNowWebAPI = "something here"
                }
                    }
                };

                context.Catalogs.Add(cat1);
                context.Catalogs.Add(cat2);
                context.Catalogs.Add(cat3);

                context.SaveChanges();


                RESTClient rClient = new RESTClient();
                rClient.endPoint = jenkinSettings.Value.ServerURL.ToString() + "/api/xml";
                rClient.authType = autheticationType.Basic;
                rClient.userName = jenkinSettings.Value.UID;
                rClient.userPassword = jenkinSettings.Value.PWD;

                

                string strResponse = string.Empty;
                List<JenkinPipeline> JenkinPipelines = new List<JenkinPipeline>();

                if ( rClient.LoadJenkinsXML(JenkinPipelines))
                {

                    foreach(JenkinPipeline jp in JenkinPipelines)
                    { context.JenkinPipelines.Add(jp); }
                    context.SaveChanges();
                }


            }

            }
        }
    }


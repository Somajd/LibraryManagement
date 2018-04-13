using LibraryManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryManagement.Utils
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum autheticationType
    {
        Basic,
        NTLM
    }

    public enum authenticationTechnique
    {
        RollYourOwn,
        NetworkCredential
    }

     public class RESTClient
    {

        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        public autheticationType authType { get; set; }
        public authenticationTechnique authTech { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }





        public RESTClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;

        }

         string makeRequest( ref String errorText)
        {

            string strResponseValue = null;



            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            string authHeader = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(userName + ":" + userPassword));
            request.Headers.Add("Authorization", authType.ToString() + " " + authHeader);

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();

                // if (response.StatusCode != HttpStatusCode.OK)
                //{
                //    throw new ApplicationException("Error Code:" + response.StatusCode.ToString());
                //}

                //Process the response stream (could be JSOn, XML, or HTML etc...)

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();

                        }//End of using streamreader
                    }

                }//end of using ResponseSteam
            }
            catch (Exception ex)
            {
                strResponseValue = "WebAPI Error:" + ex.Message.ToString() ;
                errorText = "WebAPI Error:" + ex.Message.ToString() ;
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;

        }

        public bool StartBuild(ref String errorText)
        {
            //throw new NotImplementedException();

            string xmlStrm = makeRequest(ref errorText);
            if (errorText.Length >0)
            { return false; }
            else
            { return true; }
        }


        public int GetNextBuildNumber(ref String errorText)
        {
            //throw new NotImplementedException();
            string xmlStrm = makeRequest(ref errorText);
            if (errorText.Length >0)
            { return 0; }

            string nextBuildNumber = null;
            try
            {
                XDocument xdoc = XDocument.Parse(xmlStrm);


                xdoc.Descendants("nextBuildNumber")
                 .Select(p => new
                 {
                     nextBuildNo = p.Value,
                                     //color = p.Element("color").Value,

                 }).ToList().ForEach(p =>
                 {

                     nextBuildNumber = p.nextBuildNo;

                 });
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }
            return int.Parse(nextBuildNumber);
        }

        public bool LoadJenkinsXML( List<JenkinPipeline> JenkinPipelines )
    {
            string errorText = "";
            
            try
            {
                string xmlStrm = makeRequest(ref errorText);
                if (errorText.Length >0)
                {
                    return false;
                }
                XDocument xdoc = XDocument.Parse(xmlStrm);


                xdoc.Descendants("job")
                 .Select(p => new
                 {
                     cname = p.Attribute("_class").Value,
                     name = p.Element("name").Value,
                     url = p.Element("url").Value,
             //color = p.Element("color").Value,

         }).ToList().ForEach(p =>
         {

             JenkinPipeline jp = new JenkinPipeline();
             jp.ClassName = p.cname;
             jp.Name = p.name;
             jp.Url = p.url;
                 //jp.Color = p.color;

                 JenkinPipelines.Add(jp);

         });
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return false;
            }

        return true;

    }

        public string GetJenkinStatus()
        {
            //throw new NotImplementedException();
            string errorText="";
            string xmlStrm = makeRequest(ref errorText);
            string res = null;
            try
            {
                XDocument xdoc = XDocument.Parse(xmlStrm);


                xdoc.Descendants("result")
                 .Select(p => new
                 {
                     res = p.Value,
                     //color = p.Element("color").Value,

                 }).ToList().ForEach(p =>
                 {

                     res = p.res;

                 });
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return "";
            }
            return res;
        }

        public string GetJenkinConsoleText(ref String errorText)
        {
            //throw new NotImplementedException();
            
            string xmlStrm = makeRequest(ref errorText);

            return xmlStrm;
        }
    }
}

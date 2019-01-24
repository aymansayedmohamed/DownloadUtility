using DownloadUtilityLogger;
using IBusiness;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Services.Controllers
{
    public class DownloadController : ApiController
    {

        private readonly IDownloadManager downloadManager;
        private readonly ILogger logger;


        public DownloadController(IDownloadManager downloadManager, ILogger logger)
        {
            this.downloadManager = downloadManager;
            this.logger = logger;
        }


        [HttpGet]
        public HttpResponseMessage Download(string sources)
        {
            try
            {

                downloadManager.Process(sources);

                return Request.CreateErrorResponse(HttpStatusCode.OK,"Data");
            }
            catch(Exception ex)
            {
                //handle exception
                //logger.AddErrorLog(ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

    }
}

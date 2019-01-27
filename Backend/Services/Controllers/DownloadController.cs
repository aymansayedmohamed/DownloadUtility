using DownloadUtilityLogger;
using IBusiness;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ViewModels;

namespace Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DownloadController : ApiController
    {

        private readonly IDownloadManager downloadManager;
        private readonly ILogger logger;


        public DownloadController(IDownloadManager downloadManager, ILogger logger)
        {
            this.downloadManager = downloadManager;
            this.logger = logger;
        }


        [HttpPost]
        [ActionName("DownloadBatchFiles")]
        public HttpResponseMessage DownloadBatchFiles(BatchSources batchSources)
        {
            try
            {

                downloadManager.Process(batchSources.Sources);

                var files = downloadManager.GetReadyForProcessingFiles().ToList();

                return Request.CreateResponse(HttpStatusCode.OK,files);
            }
            catch(Exception ex)
            {
                //handle exception

                logger.AddErrorLog(ex);

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,ex);
            }
        }

        [HttpGet]
        [ActionName("GetReadyForProcessingFiles")]
        public HttpResponseMessage GetReadyForProcessingFiles()
        {
            try
            {

                var files = downloadManager.GetReadyForProcessingFiles().ToList();

                downloadManager.GetFilesType(files);

                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (Exception ex)
            {
                //handle exception

                logger.AddErrorLog(ex);

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [ActionName("ApproveFile")]
        public HttpResponseMessage ApproveFile(DownloadedFile file)
        {
            try
            {

                var approvedFile = downloadManager.ApproveFile(file);

                return Request.CreateResponse(HttpStatusCode.OK, approvedFile);
            }
            catch (Exception ex)
            {
                //handle exception

                logger.AddErrorLog(ex);

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [ActionName("RejectFile")]
        public HttpResponseMessage RejectFile(DownloadedFile file)
        {
            try
            {

                var rejectedFile = downloadManager.RejectFile(file);

                return Request.CreateResponse(HttpStatusCode.OK, rejectedFile);
            }
            catch (Exception ex)
            {
                //handle exception

                logger.AddErrorLog(ex);

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}

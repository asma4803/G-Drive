using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:60248", headers: "*", methods: "*")]
    public class WebAPIController : ApiController
    {
        public String generatePdf() {
            var pfdID = HttpContext.Current.Request["id"];
            int pfid = Convert.ToInt32(pfdID);
            String pPath = HttpContext.Current.Server.MapPath("~/docs");
            String pdfName = PDFGenerator.PdfHelper.GeneratePDF(pPath, pfid);
            return pdfName;
        }

        public List<Files> FindFiles() {
            String text = HttpContext.Current.Request["textTofind"];
            List<Files> list = BAL.FilesBAL.Find(text);
            return list;
        }

    }
}

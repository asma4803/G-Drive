using Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class FolderController : Controller
    {
       
        public ActionResult subFolder(int fid)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.filelist = BAL.FilesBAL.getAllFilesById(fid);
            mymodel.folderlist = BAL.FolderBAL.getAllFoldersById(fid);
            return View(mymodel);
        }
    }
}
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
 
    [EnableCors(origins: "http://localhost:60248", headers:"*", methods:"*")]
    public class FolderAPIController : ApiController
    {
        [HttpPost]
        public int saveFolder(String fname, int id, int userID) {
            int i = BAL.FolderBAL.saveFolder(fname, id, userID);
            int recent_id = BAL.FolderBAL.getRecentFolderID();
            return recent_id;
        }
        [HttpPost]
        public List<Folder> GetSubfolder(int parentID) {
            List<Folder> list = BAL.FolderBAL.getAllFoldersById(parentID);
            return list;
        }
        [HttpPost]
        public int remove(int id) {
            return BAL.FolderBAL.remove(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BAL
{
    public class FolderBAL
    {
        public static int saveFolder(String folderName, int id, int userId) {
            return DAL.FolderDAL.saveFolder(folderName, id, userId);
        }
        public static List<Folder> getAllFolders(int userID) {
            return DAL.FolderDAL.getAllFolders(userID);
        }
        public static List<Folder> getAllFoldersById(int id) {
            return DAL.FolderDAL.getAllFoldersById(id);
        }
        public static int getRecentFolderID() {
            return DAL.FolderDAL.getRecentFolderID();
        }
        public static int remove(int id) {
            return DAL.FolderDAL.remove(id);
        }
    }
}

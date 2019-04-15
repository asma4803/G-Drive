using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class FilesBAL
    {
        public static int saveFileInDB(Files f) {
            return DAL.FilesDAL.saveFileInDB(f);
        }
        public static Files GetFileByFileID(int id) {
            return DAL.FilesDAL.GetFileByFileID(id);
        }
        public static List<Files> getAllFiles(int userID) {
            return DAL.FilesDAL.getAllFiles(userID);
        }
        public static int removeFile(int id) {
            return DAL.FilesDAL.removeFile(id);
        }
        public static List<Files> getAllFilesById(int id) {
            return DAL.FilesDAL.getAllFilesById(id);
        }
        public static int getRecentFile() {
            return DAL.FilesDAL.getRecentFile();
        }
        public static List<Files> Find(String text) {
            return DAL.FilesDAL.Find(text);
        }
    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FilesDAL
    {
        public static int saveFileInDB(Files f) {
            String query = "Insert into dbo.Files (Name, ParentFolderId, FileExt, FileSizeInKB, UploadedOn,IsActive, ContentType, fileUniqueName, CreatedBy)values ('" + f.Name+"','"+f.ParentFolderId+"', '"+f.FileExt+"', '"+f.FileSizeInKB+"','"+f.UploadedOn+"','1','"+f.ContentType+"', '"+f.fileUniqueName + "','"+f.userID+"')";
            using (DB_Class helper = new DB_Class()) {
                return helper.ExecuteQuery(query);
            }
        }
        public static int getRecentFile() {
            String query = "Select MAX(Id) from dbo.Files";
            int id = 0;
            using (DB_Class helper = new DB_Class())
            {
                var res = helper.ExecuteReader(query);
                if (res.Read())
                {
                    id = res.GetInt32(0);
                }
            }
            return id;
        }
        public static Files GetFileByFileID(int id) {
            String query = "Select * from dbo.Files where Id= '"+id+"'";
            Files f = new Files();
            using (DB_Class helper = new DB_Class()) {
                var result = helper.ExecuteReader(query);
                if (result.Read()) {
                    f = fillDTO(result);
                }
            }
            return f;
        }
        public static List<Files> getAllFiles(int userID) {
            String query = "Select * from dbo.Files where ParentFolderId = 0 AND IsActive= 1 AND CreatedBy='"+userID+"'";
            List<Files> fileList = new List<Files>();
            using (DB_Class helper = new DB_Class()) {
                var result = helper.ExecuteReader(query);
                while (result.Read()) {
                    Files f = fillDTO(result);
                    fileList.Add(f);
                }
            }
            return fileList;
        }

        public static List<Files> getAllFilesById(int id) {
            List<Files> fileList = new List<Files>();
            String query = "Select * from dbo.Files where ParentFolderId='"+id+"' AND IsActive=1";
            using (DB_Class helper = new DB_Class()) {
                var result = helper.ExecuteReader(query);
                while (result.Read()) {
                    Files f = fillDTO(result);
                    fileList.Add(f);
                }
            }
            return fileList;
        }

        public static int removeFile(int id) {
            String query = "Update dbo.Files set IsActive=0 where id='"+id+"'";
            using (DB_Class helper = new DB_Class()) {
                return helper.ExecuteQuery(query);
            }
        }

        public static List<Files> getMetaDataOfFiles(int id)
        {
            List<int> id_list = new List<int>();
            List<Files> files_list = new List<Files>();
            int i = 0;
            id_list.Add(id);
            do
            {
                String name = "ROOT";
                String query = "Select * from dbo.Files where ParentFolderId='" + id_list[i] + "' AND IsActive=1";
                try
                {
                    using (DB_Class helper = new DB_Class())
                    {
                        Files file = new Files();
                        var result = helper.ExecuteReader(query);
                        while (result.Read())
                        {
                            using (DB_Class helper2 = new DB_Class())
                            {
                                String parent_query = "Select * from dbo.Folder where Id ='" + id_list[i] + "'";
                                var parent = helper2.ExecuteReader(parent_query);
                                if (parent.Read())
                                {
                                    name = parent.GetString(1);
                                }
                            }
                            file = fillDTO(result);
                            file.parentFolderName = name;
                            files_list.Add(file);
                        }
                        using (DB_Class helper3 = new DB_Class())
                        {
                            String parentFol = "Select * from dbo.Folder where ParentFolderId='" + id_list[i] + "' AND IsActive=1";
                            var fid_result = helper3.ExecuteReader(parentFol);
                            while (fid_result.Read())
                            {
                                id_list.Add(fid_result.GetInt32(0));
                            }
                        }

                    }
                    i++;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }

            } while (i < id_list.Count);
            return files_list;
        }

        public static List<Files> Find(String text) {
            String query = "Select * from dbo.Files";
            List<Files> list = new List<Files>();
            using (DB_Class helper = new DB_Class()) {
                var result = helper.ExecuteReader(query);
                while (result.Read()) {
                    if (text.ToLower() == result.GetString(1).ToLower() ) {
                        Files f = fillDTO(result);
                        list.Add(f);
                    }
                }
            }
            return list;

        }

        private static Files fillDTO(SqlDataReader reader) {
            Files f = new Files();
            f.Id = reader.GetInt32(0);
            f.Name = reader.GetString(1);
            f.ParentFolderId = reader.GetInt32(2);
            f.FileExt = reader.GetString(3);
            f.FileSizeInKB = reader.GetInt32(4);
            f.UploadedOn = reader.GetDateTime(5);
            f.ContentType = reader.GetString(7);
            f.fileUniqueName = reader.GetString(8);
            return f;
        }
    }
}

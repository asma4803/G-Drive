using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace DAL
{
    public class FolderDAL
    {
        public static int saveFolder(String folderName, int id, int userId)
        {
             DateTime date = DateTime.Now;
            String query = "Insert into dbo.Folder (Name, ParentFolderId , CreatedOn, IsActive,CreatedBy) Values ('"+folderName+"', '"+id+"' , '"+date+"', 1,'"+userId+"')";
            using (DB_Class helper = new DB_Class())
            {
                return helper.ExecuteQuery(query);
            }
                
        }
        public static List<Folder> getAllFolders(int userID) {
            List<Folder> list = new List<Folder>();
            String query = "Select * from dbo.Folder where ParentFolderId=0 AND IsActive=1 AND CreatedBy='"+userID+"'";
            using (DB_Class helper = new DB_Class()) {
                var result = helper.ExecuteReader(query);
                while (result.Read()) {
                    Folder folder = fillDTO(result);
                    list.Add(folder);
                }
            }
            return list;
        }
        public static List<Folder> getAllFoldersById(int id) {
            List<Folder> list = new List<Folder>();
            String query = "Select * from dbo.Folder where ParentFolderId='"+id+"' AND IsActive=1";
            using (DB_Class helper = new DB_Class())
            {
                var result = helper.ExecuteReader(query);
                while (result.Read())
                {
                    Folder folder = fillDTO(result);
                    list.Add(folder);
                }
            }
            return list;
        }

        public static int remove(int id) {
            DateTime date = DateTime.Now;
            String query = String.Format("Update dbo.Folder set IsActive={0} where Id={1}",0,id);
            using (DB_Class helper = new DB_Class())
            {
                return helper.ExecuteQuery(query);
            }
        }

        public static List<Folder> getMetaDataOfFolders(int id) {
            List<int> id_list = new List<int>();
            List<Folder> folder_list = new List<Folder>();
            int i = 0;
            id_list.Add(id);
            do {
                String name = "ROOT";
                String query = "Select * from dbo.Folder where ParentFolderId='"+id_list[i]+"' AND IsActive=1";
                try {
                    using (DB_Class helper = new DB_Class())
                    {
                        Folder folder = new Folder();
                        var result = helper.ExecuteReader(query);
                        while (result.Read())
                        {
                            using (DB_Class helper2 = new DB_Class()) {
                                String parent_query = "Select * from dbo.Folder where Id ='" + id_list[i] + "' AND IsActive=1";
                                var parent = helper2.ExecuteReader(parent_query);
                                if (parent.Read()) {
                                    name = parent.GetString(1);
                                }
                            }
                            folder = fillDTO(result);
                            folder.parentFolderName = name;
                            folder_list.Add(folder);
                            id_list.Add(result.GetInt32(0));
                        }
                    }
                    i++;
                }
                catch (Exception e) {
                    Console.Write(e.Message);
                }

            } while (i < id_list.Count);
            return folder_list;
        }

        public static int getRecentFolderID() {
            String query = "Select MAX(Id) from dbo.Folder";
            int id = 0;
            using (DB_Class helper = new DB_Class()) {
                var res = helper.ExecuteReader(query);
                if (res.Read())
                {
                    id = res.GetInt32(0);
                }
            }
            return id;
        }

        private static Folder fillDTO(SqlDataReader reader) {
            Folder f = new Folder();
            f.folderID = reader.GetInt32(0);
            f.name = reader.GetString(1);
            f.parentFolderID = reader.GetInt32(2);
            f.CreatedOn = reader.GetDateTime(3);
            f.isActive = reader.GetBoolean(4);
            return f;
        }
    }
}

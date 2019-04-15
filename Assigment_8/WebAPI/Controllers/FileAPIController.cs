using Entities;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:60248", headers: "*", methods: "*")]
    public class FileAPIController : ApiController
    {
        public int uploadFile()
        {
            int result = 0;
            int recent = 0;
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    foreach (var fileName in HttpContext.Current.Request.Files.AllKeys)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                        if (file != null)
                        {
                            Files fileDTO = new Files();
                            fileDTO.Name = file.FileName;
                            fileDTO.FileExt = Path.GetExtension(file.FileName);
                            fileDTO.UploadedOn = DateTime.Now;
                            fileDTO.ContentType = file.ContentType;
                            var pfdID = HttpContext.Current.Request["fid"];
                            int pfid = Convert.ToInt32(pfdID);
                            var id = HttpContext.Current.Request["userID"];
                            int userID = Convert.ToInt32(id);
                            fileDTO.userID = userID;
                            fileDTO.ParentFolderId = pfid;
                            if ((file.ContentLength / 1048576.0) <= 8) {
                                fileDTO.FileSizeInKB = file.ContentLength / 1024; //file.ContentLength is the file size in bytes
                                fileDTO.fileUniqueName = Guid.NewGuid().ToString();
                                var rootpath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                                var fileSavePath = System.IO.Path.Combine(rootpath, fileDTO.fileUniqueName + fileDTO.FileExt);
                                file.SaveAs(fileSavePath);
                                result = BAL.FilesBAL.saveFileInDB(fileDTO);
                                recent = BAL.FilesBAL.getRecentFile();
                            }
                            
                         }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return recent;
        }

        [HttpGet]
        public Object DownloadFile(int id)
        {
            try
            {
                var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                var fileDTO = BAL.FilesBAL.GetFileByFileID(id);
                if (fileDTO != null)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileFullPath = System.IO.Path.Combine(rootPath, fileDTO.fileUniqueName + fileDTO.FileExt);
                    byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

                    response.Content = new ByteArrayContent(file);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileDTO.ContentType);
                    response.Content.Headers.ContentDisposition.FileName = fileDTO.Name;
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return null;
        }
        [HttpGet]
        public Object GetThumbnail(int id)
        {
            try {
                var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");

                // Find file from DB using unique name
                var FileDTO = BAL.FilesBAL.GetFileByFileID(id);
                var fileFullPath = Path.Combine(rootPath, FileDTO.fileUniqueName + FileDTO.FileExt);

                ShellFile shellFile = ShellFile.FromFilePath(fileFullPath);
                Bitmap shellThumb = shellFile.Thumbnail.MediumBitmap;

                if (FileDTO != null)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                    byte[] file = ImageToBytes(shellThumb); // Calling private ImageToBytes

                    MemoryStream ms = new MemoryStream(file);

                    response.Content = new ByteArrayContent(file);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(FileDTO.ContentType);
                    response.Content.Headers.ContentDisposition.FileName = FileDTO.Name;
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }

            }
            catch (Exception e) {
                Console.Write(e.Message);
            }
            // Physical path of root folder
            return null;

        }
        private byte[] ImageToBytes(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public int removeFile(int id) {
            return BAL.FilesBAL.removeFile(id);
        }
    }
}

using Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFGenerator
{
    public class PdfHelper
    {
        public static String GeneratePDF(String physicalPath, int id) {
            List<Files> fileList = DAL.FilesDAL.getMetaDataOfFiles(id);
            List<Folder> folderList = DAL.FolderDAL.getMetaDataOfFolders(id);
            String pdfName = DateTime.Now.Ticks.ToString() + ".pdf";
            var filePath = physicalPath + "\\" + pdfName;
            var doc1 = new Document();
            var streamObject = new System.IO.FileStream(filePath, System.IO.FileMode.CreateNew);
            PdfWriter.GetInstance(doc1, streamObject);
            doc1.Open();

            Chunk linebreak = new Chunk(new LineSeparator());

            if (fileList.Count == 0 && folderList.Count == 0)
            {
                doc1.Add(new Paragraph("No data found"));               
            }
            for (int i = 0; i < folderList.Count; i++)
            {
                doc1.Add(new Paragraph("Name:"+folderList[i].name));
                doc1.Add(new Paragraph("Type:Folder"));
                doc1.Add(new Paragraph("Size:NONE"));
                doc1.Add(new Paragraph("Parent:"+folderList[i].parentFolderName));
                doc1.Add(linebreak);
            }

            for (int j = 0; j < fileList.Count; j++)
            {
                doc1.Add(new Paragraph("Name:" + fileList[j].Name));
                doc1.Add(new Paragraph("Type:File"));
                doc1.Add(new Paragraph("Size:" + fileList[j].FileSizeInKB * 1024));
                doc1.Add(new Paragraph("Parent:" + fileList[j].parentFolderName));
                doc1.Add(linebreak);
            }
           
            doc1.Close();
            return pdfName;
        }
    }
}

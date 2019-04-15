using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Files
    {
        public int Id { get; set; }
        public String Name{ get; set; }
        public int ParentFolderId { get; set; }
        public String FileExt { get; set; }
        public int FileSizeInKB { get; set; }
        public String ContentType { get; set; }
        public DateTime UploadedOn { get; set; }
        public String fileUniqueName { get; set; }
        public String parentFolderName { get; set; }
        public int userID { get; set; }
    }
}

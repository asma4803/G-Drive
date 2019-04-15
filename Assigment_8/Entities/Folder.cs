using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Folder
    {
        public int folderID { get; set; }
        public String name { get; set; }
        public int parentFolderID { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean isActive { get; set; }
        public String parentFolderName { get; set; }
    }
}

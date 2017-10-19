using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webcrawler.util
{
    public static class utils
    {
        public static string getFolderPath() {
            return @"D:\";
        }
        public static string getFileName() {
            return string.Format("webCrawler");
       }
        public static string getFileName(string enterName) {
            return enterName;
        }
        public static string getCompileFilePathAndFileExtension(string enterName = "") {
            if (string.IsNullOrEmpty(enterName))
            {
                return string.Format("{0}\\{1}.json", getFolderPath(),getFileName());
            }
            else {
                return string.Format("{0}\\{1}.json", getFolderPath(),getFileName(enterName));
            }
        }
    }
}

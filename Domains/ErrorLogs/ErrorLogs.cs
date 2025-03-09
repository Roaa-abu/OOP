using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioneersAcademy.Domains.ErrorLogs
{
    public class ErrorLogs
    {
        private const string FILE_PATH = "C:\\Logs\\ErrorLogs.log";
        public static void Log(Exception ex)
        {
            if (!File.Exists(FILE_PATH))
            {
                File.Create(FILE_PATH).Close();
            }
            using (StreamWriter writer = new StreamWriter(FILE_PATH, true))
            {
                writer.WriteLine($"Date time : {DateTime.Now} - Exception Title : {ex.Message} - Excetion Details : {ex.StackTrace} ");
            }
        }
    }
}

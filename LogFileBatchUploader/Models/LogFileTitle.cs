using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LogFileBatchUploader.Models
{
    public class LogFileTitle
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int NumOfRows { get; set; }
    }
}
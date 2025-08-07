using System;

namespace Playaway_Custom_Print_Buddy.Models
{
    public class JobItem
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string UPC { get; set; }
        public ProductType ProductType { get; set; }
        public ProcessingMode ProcessingMode { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FilePath { get; set; }
        public string Notes { get; set; }
        public bool IsSelected { get; set; }

        public JobItem()
        {
            CreatedDate = DateTime.Now;
            Status = "Pending";
            IsSelected = false;
        }

        public override string ToString()
        {
            return $"{SKU} - {Title}";
        }
    }
} 
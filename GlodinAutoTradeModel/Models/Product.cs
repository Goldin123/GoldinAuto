using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GlodinAutoTradeModel.Models
{

    public class Product
    {
        public int PID { get; set; }
        public int SID { get; set; }
        [Required(ErrorMessage = "Please enter product name.")]
        public string ProductName { get; set; }
        public string Brand { get; set; }
        [Required(ErrorMessage = "Please enter product unit price.")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Please enter product units.")]
        public int UnitsInStock { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select product image.")]
        public HttpPostedFileBase Image { get; set; }
        public string FileName { get; set; }
        public string ConvertedImage { get; set; }
        public Byte[] ByteImage { get; set; }
    }

}

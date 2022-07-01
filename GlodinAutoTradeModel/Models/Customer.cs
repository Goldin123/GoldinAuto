using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlodinAutoTradeModel.Models
{
    public class Customer
    {
        public int CID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string Telephone { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        [Required]
        public string CardType { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

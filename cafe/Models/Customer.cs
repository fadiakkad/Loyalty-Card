using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cafe.Models
{
    public class Customer
    {
        //will increment the customer by Id
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set;  }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public int Age { get; set; }
    }
}

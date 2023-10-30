using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace CGullProject.Models
{
    [PrimaryKey(nameof(OrderId))]
    public class Address
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }    

        [Required]
        public string State { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string Zipcode { get; set; }

        public string Descriptor { get; set; }

        [Required]
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

    }
}

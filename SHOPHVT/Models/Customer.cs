using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SHOPHVT.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Yêu cầu nhập đúng số điện thoại")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Sai định dạng")]
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string HiddenPassword
        {
            get
            {
                return "".PadLeft(Password.Length, '*');
            }
        }
    }
}
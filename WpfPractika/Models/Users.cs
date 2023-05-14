using System;
using System.Collections.Generic;

namespace WpfPractika.Models
{
    public partial class Users
    {
        public int IdUser { get; set; }
        public string SecondNameUser { get; set; }
        public string FirstNameUser { get; set; }
        public string FatherNameUser { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LoginUser { get; set; }
        public string PasswordUser { get; set; }
        public string NumberBankCardUser { get; set; }
        public string BankCardExpirationDay { get; set; }
        public string BankCardExpirationMonth { get; set; }
        public string CvvCode { get; set; }
        public string RoleUser { get; set; }
        public int IsDeleted { get; set; }
    }
}

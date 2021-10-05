using System;

namespace PasswordSafe.Models
{
    public class Account
    {
        public Account()
        {
            OwnerName = Email = "Owner";
            Password = "Password";
            ComputerName = ComputerModel = "Computer";
            MacAddress = IPAddress = "Address";
            SerialNumber = BarCode = "Code";
            State = "State";
        }
        public Guid Id { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ComputerName { get; set; }
        public string BarCode { get; set; }
        public string ComputerModel { get; set; }
        public string SerialNumber { get; set; }
        public string MacAddress { get; set; }
        public string IPAddress { get; set; }
        public string State { get; set; }
    }
}

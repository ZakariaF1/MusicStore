using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Api.Requests.CreateRequests
{
    public class EmployeeCreateRequest
    {
        [Required(ErrorMessage = "The first name field is required.")]
        [RegularExpression(@"^[a-zA-Z ,.'-]+$", ErrorMessage = "Please provide a proper first name. Allowed characters are ( a-z , . , ' , - )")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Between 3 and 20 characters is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The last name field is required.")]
        [RegularExpression(@"^[a-zA-Z ,.'-]+$", ErrorMessage = "Please provide a proper last name. Allowed characters are ( a-z , . , ' , - )")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Between 3 and 20 characters is required")]
        public string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z ,.'-]+$", ErrorMessage = "Please provide a proper title. Allowed characters are ( a-z , . , ' , - )")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Between 3 and 30 characters is required")]
        public string Title { get; set; }

        public int? ReportsTo { get; set; }

        [Required(ErrorMessage = "The birth date field is required.")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "The hire date field is required.")]
        public DateTime? HireDate { get; set; }

        [RegularExpression(@"^(?=.*?[a-zA-Z])[0-9a-zA-Z /.'-]+$", ErrorMessage = "Please provide a proper address. Allowed characters are ( 0-9, a-z, A-Z, /, ', ., - ) with at least one letter Ex: 11120 Jasper Ave NW or New York Ave. 75")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "Between 5 and 70 characters is required")]
        public string Address { get; set; }

        [RegularExpression(@"^[a-zA-Z' .-]+$", ErrorMessage = "Please provide a proper city.Allowed characters are (a-z, A-Z, ', ., - ) Ex: Budapest or Warsaw")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Between 4 and 40 characters is required")]
        public string City { get; set; }

        [RegularExpression(@"^[a-zA-Z' .-]+$", ErrorMessage = "Please provide a proper state.Allowed characters are (a-z, A-Z, ', ., - ) Ex: Ilfov or Nevada ")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Between 2 and 40 characters is required")]
        public string State { get; set; }

        [MaxLength(40)]
        public string Country { get; set; }

        [RegularExpression(@"^[0-9A-Z -]+$", ErrorMessage = "Please provide a proper postal code. Allowed characters are ( 0-9, A-Z, - ) Ex: T5K 2N1 or 12345-6789")]
        [StringLength(13, MinimumLength = 4, ErrorMessage = "Between 4 and 13 characters is required")]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[+]*[0-9 ()-]+$", ErrorMessage = "Please provide a proper phone number. Allowed characters are ( 0-9, -, () ) Ex: +1 (403) 263-4289, 123-456-7899 or 1234567899")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Between 5 and 20 characters is required")]
        public string Phone { get; set; }

        [RegularExpression(@"^[+]*[0-9 ()-]+$", ErrorMessage = "Please provide a proper fax number. Allowed characters are ( 0-9, -, () ) Ex: +1 (403) 263-4289, 123-456-7899 or 1234567899")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Between 5 and 20 characters is required")]
        public string Fax { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9]+[.]+[a-zA-Z]+$", ErrorMessage = "Please provide a proper email. Ex: andrew@chinookcorp.com or zack23@gmail.ro")]
        [StringLength(60, ErrorMessage = "No more than 60 characters is allowed")]
        [Remote("ValidateEmailAddress", "employee")]
        public string Email { get; set; }
    }
}

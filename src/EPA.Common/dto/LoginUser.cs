using System.ComponentModel.DataAnnotations;

namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes login fields
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// Gets or sets user's email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user's password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

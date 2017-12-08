namespace EPA.Common.DTO
{
    /// <summary>
    /// This class describes password change fields
    /// </summary>
    public class ChangePassword
    {
        /// <summary>
        /// Gets or sets old password
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets new password
        /// </summary>
        public string NewPassword { get; set; }
    }
}

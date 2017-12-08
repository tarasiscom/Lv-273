namespace EPA.Common.Interfaces
{
    /// <summary>
    /// This class describes constant values used in application
    /// </summary>
    public class ConstSettings
    {
        /// <summary>
        /// Gets or sets number of applicants to the number of passed student factor constant
        /// </summary>
        public float KoefOfNumApplication { get; set; }

        /// <summary>
        /// Gets or sets constant that represents "all districts" value
        /// </summary>
        public int AllDistricts { get; set; }

        /// <summary>
        /// Gets or sets common count of elements for page constant(used in specialties pagination)
        /// </summary>
        public int CountForPage { get; set; }

        /// <summary>
        /// Gets or sets count of elements for top universities display constant
        /// </summary>
        public int TopUniversities { get; set; }

        /// <summary>
        /// Gets or sets constant password for email from which registration confirmation letter is sent
        /// </summary>
        public string EmailPassword { get; set; }

        /// <summary>
        /// Gets or sets constant email address from which registration confirmation letter is sent
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets successful registration string constant
        /// </summary>
        public string RegistrSuccess { get; set; }
    }
}

namespace Authentication.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
        
        /// <summary>
        /// PasswordHash
        /// </summary>
        public byte[] PasswordHash { get; set; }
        
        /// <summary>
        /// PasswordSalt
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}
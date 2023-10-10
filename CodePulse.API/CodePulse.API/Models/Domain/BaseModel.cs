namespace CodePulse.API.Models.Domain
{
    public class BaseModel
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Created Time.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last Updated Time.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}

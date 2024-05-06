namespace WebApi.Requests.Basket
{   /// <summary>
    /// The request to create a new basket for the user.
    /// </summary>
    public class CreateBasketRequest
    {
        /// <summary>
        /// The Id the user.
        /// </summary>
        /// <example>1</example>
        public int UserId { get; set; }
    }
}

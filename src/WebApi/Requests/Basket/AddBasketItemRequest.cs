namespace WebApi.Requests.Basket
{   /// <summary>
    /// The request to Add an Item to the Basket.
    /// </summary>
    public class AddBasketItemRequest
    {   /// <summary>
        /// The Id of the MilitaryJet to add the to basket.
        /// </summary>
        /// <example>1</example>
        public int MilitaryJetId { get; set; }
    }
}

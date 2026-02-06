using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Exceptions
{
    public class InvalidCartArticlesException : Exception
    {
        public IReadOnlyCollection<int> ArticleIds { get; }

        public InvalidCartArticlesException(IEnumerable<int> articleIds)
        : base("Some articles in the cart are no longer available.")
        {
            ArticleIds = articleIds.ToList().AsReadOnly();
        }
    }
}

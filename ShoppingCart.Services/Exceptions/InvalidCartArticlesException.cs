using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Exceptions
{
    public class InvalidCartArticlesException : Exception
    {
        public IReadOnlyCollection<int> ArticleIds { get; }

        public InvalidCartArticlesException(IEnumerable<int> articleIds)
        : base("Some items in your cart are no longer available. Please remove them to continue.")
        {
            ArticleIds = articleIds.ToList().AsReadOnly();
        }
    }
}

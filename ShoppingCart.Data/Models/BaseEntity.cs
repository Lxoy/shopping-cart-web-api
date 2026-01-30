using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingCart.Data.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}

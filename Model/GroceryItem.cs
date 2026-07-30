using System;
using System.Collections.Generic;
using System.Text;

namespace PodB_MAUI.Model
{
    public record GroceryItem(string name, GroceryCategory category, double price);
}

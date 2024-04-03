using GildedRoseCSharp;

namespace csharp
{
    // Req: do not alter the Item class or Items 
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public override string ToString()
        {
            return this.Name + ", " + this.SellIn + ", " + this.Quality;
        }  
    }


    public class ReadOnlyItem
    {
        public ValidItems Name { get;  }
        public string NameAsString { get; }
        public int SellIn { get; }
        public int Quality { get; }

        public ReadOnlyItem(Item item)
        {
            NameAsString = item.Name;
            Name = ItemHelper.ConvertToValidItem(item.Name);
            SellIn = item.SellIn;
            Quality = item.Quality;
        }

    }
}

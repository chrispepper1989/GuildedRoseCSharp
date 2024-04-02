using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp;

namespace GildedRoseCSharp
{

    


    public enum ValidItems

    {
        DexterityVest,
        AgedBrie,
        ElixirOfMongoose,
        SulfurasHandOfRagnaros,
        BackstagePassToTalk80ETCConcert,
        ConjuredManaCake,
    }

  
    public static class ItemHelper
    {
        private static readonly Dictionary<ValidItems, string> ItemNames = new Dictionary<ValidItems, string>()
        {
            { ValidItems.DexterityVest, "+5 Dexterity Vest" },
            { ValidItems.AgedBrie, "Aged Brie" },
            { ValidItems.ElixirOfMongoose, "Elixir of the Mongoose" },
            { ValidItems.SulfurasHandOfRagnaros, "Sulfuras, Hand of Ragnaros" },
            { ValidItems.BackstagePassToTalk80ETCConcert, "Backstage passes to a TAFKAL80ETC concert" },
            { ValidItems.ConjuredManaCake, "Conjured Mana Cake" }
        };


        public static bool IsItem(this Item item, ValidItems itemType)
        {
            return item.Name == ItemNames[itemType];
        }

        public static Item CreateItem(ValidItems itemType, int sellIn, int quality)
        {
            var itemName = ItemNames[itemType] ?? "";
            return new Item() { Name = itemName, Quality = quality, SellIn = sellIn };

        }

 
    }

    
}

using System.Collections.Generic;
using GildedRoseCSharp;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        private const int MaxIncreasableQuality = 50;

        public Item GetUpdatedItem(ReadOnlyItem itemReadOnly)
        {
            
            if (itemReadOnly.Name == ValidItems.SulfurasHandOfRagnaros)
            {
                // do nothing
                return new Item()
                    { Name = itemReadOnly.NameAsString, Quality = itemReadOnly.Quality, SellIn = itemReadOnly.SellIn };
            }
            
            // all other items assume day has passed and sell in is reduced
            var newSellIn = itemReadOnly.SellIn - 1;

            var newQuality = itemReadOnly.Name switch
            {
                ValidItems.AgedBrie => GetNewAgedBrieItemQuanity(itemReadOnly),
                ValidItems.BackstagePassToTalk80ETCConcert => GetNewBackStagePassQuality(itemReadOnly, newSellIn),
                ValidItems.ConjuredManaCake => GetNewConjuredManaCakeQuality(itemReadOnly),
                _ => GetNewStandardItemQuality(itemReadOnly)
            };

            return new Item()
                { Name = itemReadOnly.NameAsString, Quality = newQuality, SellIn = newSellIn };
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                var newItem = GetUpdatedItem(new ReadOnlyItem(item));
                item.Quality= newItem.Quality;
                item.SellIn = newItem.SellIn;
            }
        }

        // a more functional approach
        public IEnumerable<Item> GetUpdateQualities(IList<Item> items)
        {
            return items.Select(item => GetUpdatedItem(new ReadOnlyItem(item)));
        }
      
        private static int GetNewBackStagePassQuality(ReadOnlyItem item, int newSellIn)
        {
            /*
            Backstage passes, like aged brie, increases in Quality as its SellIn value approaches;

            Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
            Quality drops to 0 after the concert
             */
            if (newSellIn < 0)
            {
                return 0;
            }
           

            // Quality increase based on remaining days
            var daysRemaining = item.SellIn;
            var qualityIncrease = daysRemaining < 6 ? 3 : (daysRemaining < 11 ? 2 : 1);

          
            return GetNewItemQuality(item, qualityIncrease);
        }

        private static int GetNewItemQuality(ReadOnlyItem item, int qualityChange)
        {

            var newQuality = item.SellIn-1 < 0
                ? item.Quality + (qualityChange * 2)
                : item.Quality + qualityChange;

            //any new increases are clamped
            if (newQuality > item.Quality)
            {
                return Math.Clamp(newQuality, 0, MaxIncreasableQuality);
            }
            return  Math.Max(0, newQuality);

        }


        // aged brie improves in quality once past sell by date
        private static int GetNewAgedBrieItemQuanity(ReadOnlyItem item)
        {
            if (item.Quality > MaxIncreasableQuality)
                return item.Quality;

            return GetNewItemQuality(item, 1);

        }

        public static int GetNewStandardItemQuality(ReadOnlyItem item)
        {
            return GetNewItemQuality(item, -1);

        }
        public static int GetNewConjuredManaCakeQuality(ReadOnlyItem item)
        {
            return GetNewItemQuality(item, -2);
        }
    }
}
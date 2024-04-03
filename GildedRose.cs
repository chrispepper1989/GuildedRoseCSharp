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


        public void UpdateQuality()
        {
                
            foreach (var item in Items)
            {
                if (item.IsItem(ValidItems.SulfurasHandOfRagnaros))
                {
                    // do nothing
                    continue;
                }

                // all other items assume day has passed and sell in is reduced
                int newSellIn = item.SellIn - 1;
                int newQuality;
                var itemCopy = new Item() { Name = item.Name, Quality = item.Quality, SellIn = item.SellIn };

                if (item.IsItem(ValidItems.AgedBrie))
                {
                    newQuality = GetNewAgedBrieItemQuanity(itemCopy);
                }
                else if (item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
                {
                    newQuality = GetNewBackStagePassQuality(item, newSellIn);
                }
                else if (item.IsItem(ValidItems.ConjuredManaCake))
                {
                    newQuality = GetNewConjuredManaCakeQuality(item);
                }
                else
                {
                    newQuality =GetNewStandardItemQuality(itemCopy);
                }

                

                item.SellIn = newSellIn;
                //quality can not be below 0 or above 50
                item.Quality = newQuality;//Math.Clamp(newQuality, 0, MaxIncreasableQuality);

            }
        }
      
        private static int GetNewBackStagePassQuality(Item item, int newSellIn)
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

        private static int GetNewItemQuality(Item item, int qualityChange)
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
        private static int GetNewAgedBrieItemQuanity(Item item)
        {
            if (item.Quality > MaxIncreasableQuality)
                return item.Quality;

            return GetNewItemQuality(item, 1);

        }

        public static int GetNewStandardItemQuality(Item item)
        {
            return GetNewItemQuality(item, -1);

        }
        public static int GetNewConjuredManaCakeQuality(Item item)
        {
            return GetNewItemQuality(item, -2);
        }
    }
}
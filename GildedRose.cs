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

                //getUpdatedItem(item);
            }

            Items = Items.Select(getUpdatedItem).ToList();
        }

        private static Item getUpdatedItem(Item item)
        {
            if (item.IsItem(ValidItems.SulfurasHandOfRagnaros))
            {
                // do nothing
                return item;
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
                newQuality = GetNewBackStagePassQuality(itemCopy.Quality, itemCopy.SellIn, newSellIn);
            }
            else if (item.IsItem(ValidItems.ConjuredManaCake))
            {
                newQuality = GetNewConjuredManaCakeQuality(itemCopy);
            }
            else
            {
                newQuality =GetNewStandardItemQuality(itemCopy);
            }

            item.SellIn = newSellIn;
            //quality can not be below 0 or above 50
            item.Quality = Math.Clamp(newQuality, 0, MaxIncreasableQuality);
            return new Item() { Name = item.Name, Quality = item.Quality, SellIn = item.SellIn };
        }

        private static int GetNewBackStagePassQuality(int currentQuality, int currentSellIn, int newSellIn)
        {
            /*
            Backstage passes, like aged brie, increases in Quality as its SellIn value approaches;

            Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
            Quality drops to 0 after the concert (otherwise increases by 1)
             */
            if (newSellIn < 0)
            {
                return 0;
            }
           
            // Quality increase based on remaining days
            int daysRemaining = currentSellIn;
            int qualityIncrease = daysRemaining <= 5 ? 3 : 
                                  daysRemaining <= 10 ? 2 :
                                     1;

            return currentQuality + qualityIncrease;
        }

        private static int GetNewItemQuality(in Item item, int qualityChange)
        {
            return item.SellIn-1 < 0
                ? item.Quality + (qualityChange * 2)
                : item.Quality + qualityChange;
    
        }


        // aged brie improves in quality once past sell by date
        private static int GetNewAgedBrieItemQuanity(in Item item)
        {
            return GetNewItemQuality(item, 1);

        }

        public static int GetNewStandardItemQuality(in Item item)
        {
            return GetNewItemQuality(item, -1);

        }
        public static int GetNewConjuredManaCakeQuality(in Item item)
        {
            return GetNewItemQuality(item, -2);
        }
    }
}
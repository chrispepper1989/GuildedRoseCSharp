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

                // all other items assume day has passed and sellin is reduced
                int newSellIn = item.SellIn - 1;
                int newQuality;
                Item itemCopy = new Item() { Name = item.Name, Quality = item.Quality, SellIn = item.SellIn };

                if (item.IsItem(ValidItems.AgedBrie))
                {
                    newQuality = UpdateAgedBrieItemQuanity(itemCopy);
                }
                else if (item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
                {
                    newQuality = GetNewBackStagePassQuality(item.Quality, item.SellIn, newSellIn);
                }
                else
                {
                    newQuality =UpdateItemQuantity(itemCopy);
                }

                item.SellIn = newSellIn;
                //qualituy can not be below 0 or above 50
                item.Quality = Math.Max(0,Math.Min(newQuality, MaxIncreasableQuality)); 

            }
        }
        //todo what is wrong ith this function....
        private static int GetNewBackStagePassQuality(int currentQuality, int currentSellIn, int newSellIn)
        {
            /*
            Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;

            Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
            Quality drops to 0 after the concert
             */
            if (newSellIn < 0)
            {
                return 0;
            }
           

            // Quality increase based on remaining days
            int daysRemaining = currentSellIn;
            int qualityIncrease = daysRemaining < 6 ? 3 : (daysRemaining < 11 ? 2 : 1);

            return currentQuality + qualityIncrease;
        }

        private static int UpdateQuality(Item item, int qualityChange)
        {

            int newQuality = item.SellIn-1 < 0
                ? item.Quality + (qualityChange * 2)
                : item.Quality + qualityChange;
     
            return newQuality; 
        }


        // aged brie improves in quality once past sell by date
        private static int UpdateAgedBrieItemQuanity(Item item)
        {
            return UpdateQuality(item, 1);

        }
        

        private static int UpdateItemQuantity(Item item)
        {
            return UpdateQuality(item, -1);

        }
    }
}
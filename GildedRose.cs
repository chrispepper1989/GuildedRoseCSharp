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
                if (item.IsItem(ValidItems.AgedBrie))
                {
                    UpdateAgedBrieItemQuanity(item);
                }
                else if (item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
                {
                    UpdateBackstagePassToTalk80ETCConcertItemQuantity(item);
                    //todo: item.Quality = GetNewBackStagePassQuality(item.Quality, item.SellIn, item.SellIn - 1);

                }
                else if (item.IsItem(ValidItems.SulfurasHandOfRagnaros))
                {
                    // do nothing
                }
                else
                {
                    UpdateItemQuantity(item);
                }
                
            }
        }
        //todo what is wrong ith this function....
        private static int GetNewBackStagePassQuality(int currentQuality, int currentSellIn, int newSellIn)
        {
            // Backstage passes increase in Quality as SellIn approaches 0

            if (newSellIn < 0)
            {
                return 0;
            }

            if (currentQuality >= GildedRose.MaxIncreasableQuality)
            {
                return currentQuality;
            }

            // Quality increase based on remaining days
            int daysRemaining = currentSellIn;
            int qualityIncrease = daysRemaining < 6 ? 3 : (daysRemaining < 11 ? 2 : 1);

            return currentQuality + qualityIncrease;
        }

        private static void UpdateBackstagePassToTalk80ETCConcertItemQuantity(Item item)
        {

            if (item.Quality < MaxIncreasableQuality)
            {
                item.Quality += 1;

             
                if (item.SellIn < 11 && item.Quality < GildedRose.MaxIncreasableQuality)
                {
                    item.Quality += 1;
                }

                if (item.SellIn < 6 && item.Quality < GildedRose.MaxIncreasableQuality)
                {
                    item.Quality += 1;
                }
                
            }

            
            item.SellIn -= 1;
            

            if (item.SellIn < 0)
            {
                item.Quality -= item.Quality;

            }
        }


        // aged brie improves in quality once past sell by date
        private static void UpdateAgedBrieItemQuanity(Item item)
        {
            item.SellIn -= 1;

            if (item.Quality >= GildedRose.MaxIncreasableQuality)
            {
                return;
            }
            
            item.Quality += 1;
            //when the sell in is less than 0 (past best before) quality degrades/increases twice as fast
            if (item.SellIn < 0 && item.Quality < GildedRose.MaxIncreasableQuality)
            {
                item.Quality += 1;
            }
        }


        private static void UpdateItemQuantity(Item item)
        {
         
            if (item.Quality > 0)
            {
                if (!item.IsItem(ValidItems.SulfurasHandOfRagnaros))
                {
                    item.Quality = item.Quality - 1;
                }
            }
               
         
            item.SellIn = item.SellIn - 1;
            

            if (item.SellIn < 0)
            {
                if (item.Quality > 0)
                {
                    item.Quality = item.Quality - 1;
                }
                
            }
        }
    }
}
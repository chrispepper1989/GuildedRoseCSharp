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
                    //newQuality = UpdateBackstagePassToTalk80ETCConcertItemQuantity(itemCopy);
                    newQuality = GetNewBackStagePassQuality(item.Quality, item.SellIn, newSellIn);
                }
                else
                {
                    newQuality =UpdateItemQuantity(itemCopy);
                }

                item.SellIn = newSellIn;
                item.Quality = newQuality;

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

            return Math.Min(currentQuality + qualityIncrease, 50);
        }

        private static int UpdateBackstagePassToTalk80ETCConcertItemQuantity(Item item)
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

            if (item.SellIn-1 < 0)
            {
                item.Quality -= item.Quality;

            }
            return item.Quality;
        }


        // aged brie improves in quality once past sell by date
        private static int UpdateAgedBrieItemQuanity(Item item)
        {
         

            if (item.Quality >= GildedRose.MaxIncreasableQuality)
            {
                return item.Quality;
            }
            
            item.Quality += 1;
            //when the sell in is less than 0 (past best before) quality degrades/increases twice as fast
            if (item.SellIn - 1 < 0 && item.Quality < GildedRose.MaxIncreasableQuality)
            {
                item.Quality += 1;
            }
            return item.Quality;

        }
        

        private static int UpdateItemQuantity(Item item)
        {

            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }


    

            if (item.Quality <= 0)
                return item.Quality; ;
           

            if (item.SellIn -1  < 0)
            {
                item.Quality = item.Quality - 1;
                
            }
            return item.Quality;
        }
    }
}
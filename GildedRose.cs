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

        private const int maxIncreasableQuality = 50;


        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.IsItem(ValidItems.AgedBrie))
                {
                    //update sellin
                    item.SellIn -= 1;
                    UpdateAgedBrieItemQuanity(item);
                }
                else
                {
                    UpdateItemQuantity(item);
                }
                
            }
        }

     
        // aged brie improves in quality once past sell by date
        private static void UpdateAgedBrieItemQuanity(Item item)
        {
            if (item.Quality >= maxIncreasableQuality)
            {
                return;
            }
            
            item.Quality += 1;
            //when the sell in is less than 0 (past best before) quality degrades/increases twice as fast
            if (item.SellIn < 0 && item.Quality < maxIncreasableQuality)
            {
                item.Quality += 1;
            }
        }


        private static void UpdateItemQuantity(Item item)
        {
            if (!item.IsItem(ValidItems.AgedBrie) && !item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
            {
                if (item.Quality > 0)
                {
                    if (!item.IsItem(ValidItems.SulfurasHandOfRagnaros))
                    {
                        item.Quality = item.Quality - 1;
                    }
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;

               
                    if (item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
                    {
                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality = item.Quality + 1;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality = item.Quality + 1;
                            }
                        }
                    }
                }
            }

               
            if (!item.IsItem(ValidItems.SulfurasHandOfRagnaros))
            {
                item.SellIn = item.SellIn - 1;
            }

            if (item.SellIn < 0)
            {
                if (!item.IsItem(ValidItems.AgedBrie))
                {
                    if (!item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
                    {
                        if (item.Quality > 0)
                        {
                            if(!item.IsItem(ValidItems.SulfurasHandOfRagnaros))
                            {
                                item.Quality = item.Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        item.Quality = item.Quality - item.Quality;
                    }
                }
                
            }
        }
    }
}
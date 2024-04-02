﻿using System.Collections.Generic;
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

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                // if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                if (!item.IsItem(ValidItems.AgedBrie) && !item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name != "Sulfuras, Hand of Ragnaros")
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

                        //if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
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

                //if (item.Name != "Sulfuras, Hand of Ragnaros")
                if (!item.IsItem(ValidItems.SulfurasHandOfRagnaros))
                {
                    item.SellIn = item.SellIn - 1;
                }

                if (item.SellIn < 0)
                {
                    // if (item.Name != "Aged Brie")
                    if (!item.IsItem(ValidItems.AgedBrie))
                    {
                        // if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                        if (!item.IsItem(ValidItems.BackstagePassToTalk80ETCConcert))
                        {
                            if (item.Quality > 0)
                            {
                               // if (item.Name != "Sulfuras, Hand of Ragnaros")
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
                    else
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
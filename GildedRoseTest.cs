using System;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using csharp;

namespace GildedRoseCSharp
{
    [UseReporter(typeof(DiffReporter))]
    [TestFixture]
    public class GildedRoseTest
    {

        //generate a bunch of test cases
        public IList<Item> GenerateTestItems(string[] names, int[] sellIns, int[] qualities)
        {
            IList<Item> Items = new List<Item>
            {
            };
            foreach (var name in names)
            {
                foreach (var sellIn in sellIns)
                {
                    foreach (var quality in qualities)
                    {
                        var item = new Item{ Name =name, Quality = quality, SellIn = sellIn};
                        Items.Add( item);
                    }

                }

            }

            return Items;

        }

         [Test]
         public void TestUpdateQuality()
         {

            // new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            // new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            // new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
            // new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            // new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },

             //arrange
             var names = new[] { "Aged Brie" , "+5 Dexterity Vest" , "Elixir of the Mongoose", "Sulfuras, Hand of Ragnaros", "Sulfuras, Hand of Ragnaros" };
             var sellIns = new[] { 1, 5, 10, 2, 4 };
             var qualities = new[] { 0, 1, 2, 5, 7, 20, 80 };
             var Items = GenerateTestItems(names, sellIns, qualities );
             GildedRose app = new GildedRose(Items);
             //act
             app.UpdateQuality();
             //assert
             Approvals.VerifyAll(Items,"All Items");

         }
    }
}

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

        //[TestCase(10)] //todo why doesnt this work with Approvals?
        public void TestUpdateQuality(int days)
        {

            //arrange
            var names = new[] { "Aged Brie", "+5 Dexterity Vest", "Elixir of the Mongoose", "Sulfuras, Hand of Ragnaros", "Sulfuras, Hand of Ragnaros" };
            var sellIns = new[] { 1, 5, 10, 2, 4 };
            var qualities = new[] { 0, 1, 2, 5, 29 };
            var Items = GenerateTestItems(names, sellIns, qualities);
            GildedRose app = new GildedRose(Items);
            //act
            for (var i = 0; i < days; ++i)
            {
                app.UpdateQuality();
            }
            //assert
            Approvals.VerifyAll(Items, "All Items");

        }
        [Test]
        public void ConjuredManaCakeDegradesTwiceAsFastAsNormalItems()
        {
            //arrange
            var conitem = ItemHelper.CreateItem(ValidItems.ConjuredManaCake, 10, 100);
            var normalItem = ItemHelper.CreateItem(ValidItems.ElixirOfMongoose, 10, 100);

            IList<Item> Items = new List<Item>
            {
                conitem,
                //include normal item twice so it degrades twice as fast
                normalItem,
                normalItem
            };

            GildedRose app = new GildedRose(Items);

            IList<Item> ItemsOverTime = new List<Item>();
            //act
            for (var i = 0; i < 100; ++i)
            {
                app.UpdateQuality();
                //assert
                Assert.AreEqual(conitem.Quality, normalItem.Quality);
                ItemsOverTime.Add(new Item()
                {
                    Name = normalItem.Name,
                    Quality = normalItem.Quality,
                    SellIn = normalItem.SellIn

                });
                ItemsOverTime.Add(new Item()
                {
                    Name = conitem.Name,
                    Quality = conitem.Quality,
                    SellIn = conitem.SellIn

                });

            }
            Approvals.VerifyAll(ItemsOverTime, "Conjured Items ");

        }

        [Test]
         public void TestUpdateQuality_1Days()
         {
             TestUpdateQuality(1);
         }
         [Test]
         public void TestUpdateQuality_5Days()
         {
             TestUpdateQuality(5);
         }
         [Test]
         public void TestUpdateQuality_10Days()
         {
             TestUpdateQuality(10);
         }
         [Test]
         public void TestUpdateQuality_30Days()
         {
             TestUpdateQuality(30);
         }

    }
}

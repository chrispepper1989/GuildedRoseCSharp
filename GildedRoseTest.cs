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
        public IList<Item> GenerateTestItems(ValidItems[] names, int[] sellIns, int[] qualities)
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
                        var item = ItemHelper.CreateItem(name, sellIn, quality);
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
            ValidItems[] namesProper = new[]
            {
                ValidItems.AgedBrie, ValidItems.DexterityVest, ValidItems.ElixirOfMongoose,
                ValidItems.SulfurasHandOfRagnaros, ValidItems.ConjuredManaCake
            };
            var sellIns = new[] { 1, 5, 10, 2, 4 };
            var qualities = new[] { 0, 1, 2, 5, 29 };
            var Items = GenerateTestItems(namesProper, sellIns, qualities);
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
         
            };

            GildedRose app = new GildedRose(Items);

            IList<Item> ItemsOverTime = new List<Item>();
            //act
            for (var i = 0; i < 100; ++i)
            {
                conitem.SellIn -= 1;
                normalItem.SellIn -= 1;
                //normal item done twice
                normalItem.Quality =  GildedRose.GetNewStandardItemQuality(normalItem);
                normalItem.Quality = GildedRose.GetNewStandardItemQuality(normalItem);
                conitem.Quality = GildedRose.GetNewConjuredManaCakeQuality(conitem);
               
                //assert
                Assert.AreEqual(normalItem.Quality, conitem.Quality);
      
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

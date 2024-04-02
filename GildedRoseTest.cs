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
         public void Test2()
         {


             //arrange
             var Items = GenerateTestItems( new [] { "Aged Brie" }, new []{1, 5,10,2,4}, new[] { 1, 2 });
             GildedRose app = new GildedRose(Items);
             //act
             app.UpdateQuality();
             //assert
             Approvals.VerifyAll(Items,"All Items");

         }
    }
}

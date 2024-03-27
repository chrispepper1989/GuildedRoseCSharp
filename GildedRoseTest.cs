using csharp;

namespace GildedRoseCSharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [TestCase("foo")]
        public void foo(string name)
        {
            //arrange
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            //act
            app.UpdateQuality();
            //assert
            Assert.AreEqual("fixme", Items[0].Name);
        }
    }
}

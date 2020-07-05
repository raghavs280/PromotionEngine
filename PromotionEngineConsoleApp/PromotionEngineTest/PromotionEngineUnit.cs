using Schema.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Schema.CheckOut;

namespace PromotionEngineTest
{
    public class PromotionEngineUnit
    {
        // Test Data to verify the logic        
        List<ActivePromotion> activePromotions = new List<ActivePromotion> {
            new ActivePromotion{SKUIds="A",Price=130,Unit=3},
            new ActivePromotion{SKUIds="B",Price=45,Unit=2},
            new ActivePromotion{SKUIds="C,D",Price=30,Unit=1}
        };
        [Fact]
        public void Test1()
        {
            var listofCarts = new Dictionary<string, int>
            {
                { "A",1},
                { "B",1},
                { "C",1}
            };
            decimal orderValue=CheckOutCart.CalculateOrderValue(listofCarts);
            Assert.Equal(100, orderValue);
        
        }
        [Fact]
        public void Test2()
        {
            var listofCarts = new Dictionary<string, int>
            {
                { "A",5},
                { "B",5},
                { "C",1},
            };
            decimal orderValue = CheckOutCart.CalculateOrderValue(listofCarts);
            Assert.Equal(370, orderValue);
        }
        [Fact]
        public void Test3()
        {

        }       
    }
}

using Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schema.CheckOut
{
    public static class CheckOutCart
    {
        static Dictionary<string, decimal> ActualPrice = new Dictionary<string, decimal>
        {
            { "A",50},
            { "B",30},
            { "C",20},
            { "D",15},
        };
        // Test Data to verify the logic
        static List<ActivePromotion> activePromotions = new List<ActivePromotion> {
            new ActivePromotion{SKUIds="A",Price=130,Unit=3},
            new ActivePromotion{SKUIds="B",Price=45,Unit=2},
            new ActivePromotion{SKUIds="C,D",Price=30,Unit=1}
    };
        public static decimal CalculateOrderValue(Dictionary<string, int> items)
        {

            // iterating the list of items in a cart and then applying the promotion
            decimal orderValue = 0;

            foreach (KeyValuePair<string, int> item in items)
            {
                // TODO: many promotions can exist for a single Id right now only fetching first promotion if matches
                // getting the promotion Engine on the basis of key which is Id
                var actualPrice = ActualPrice[item.Key];
                var promotion = activePromotions.Where(x => x.SKUIds.Contains(item.Key) && x.Unit>=item.Value).
                    FirstOrDefault();
                if (promotion != null)
                {
                    var skuId = promotion.SKUIds;
                    if (skuId.Contains(","))
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                    orderValue += actualPrice;
                }

            }
            return orderValue;
        }

    }
}


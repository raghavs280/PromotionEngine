using Schema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Schema.CheckOut
{
    public static class CheckOutCart
    {
        // Test Data to verify the logic
        static Dictionary<string, decimal> ActualPrice = new Dictionary<string, decimal>
        {
            { "A",50},
            { "B",30},
            { "C",20},
            { "D",15},
        };

        static List<ActivePromotion> activePromotions = new List<ActivePromotion> {
            new ActivePromotion{SKUIds="A",Price=130,Unit=3},
            new ActivePromotion{SKUIds="B",Price=45,Unit=2},
            new ActivePromotion{SKUIds="C,D",Price=30,Unit=1}
    };
        public static decimal CalculateOrderValue(Dictionary<string, int> items)
        {

            // iterating the list of items in a cart and then applying the promotion
            decimal orderValue = 0;
            var isPromotionActiveforMultipleId = false;

            foreach (KeyValuePair<string, int> item in items)
            {
                // TODO: many promotions can exist for a single Id right now only fetching first promotion if matches
                // getting the promotion Engine on the basis of key which is Id
                var actualPrice = ActualPrice[item.Key];
                var promotion = activePromotions.Where(x => x.SKUIds.Contains(item.Key) && x.Unit <= item.Value).
                    FirstOrDefault();
                if (promotion != null)
                {
                    var skuId = promotion.SKUIds;
                    if (skuId.Contains(","))
                    {
                        // logic for handing the one promotion exist for combination of sku ids
                        string[] combinedPromtionId = skuId.Split(',').ToArray();

                        // iterating the combined list of id's for promotion
                        // TODO: revisit down logic
                        foreach (var promotionSku in combinedPromtionId)
                        {
                            // checking for other sku id which is part of promotion and validating the no of units allowed in the promotion
                            if (items.ContainsKey(promotionSku) && item.Key != promotionSku && items[promotionSku] <= item.Value)
                                isPromotionActiveforMultipleId = true;
                            else
                            {
                                isPromotionActiveforMultipleId = false;
                                break;
                            }
                        }
                        if (!isPromotionActiveforMultipleId)
                            orderValue += actualPrice * item.Value;
                        else
                        {
                            // apply promotion price
                        }

                    }
                    else
                    {
                        //var unitsConsiderforPromotion = item.Value;
                        // checking for remainder by appling the modulus function                        
                        var remainder = item.Value % promotion.Unit;
                        orderValue += ((item.Value - remainder) / promotion.Unit) * promotion.Price;
                        if (remainder > 0)
                            orderValue += actualPrice * remainder;
                    }
                }
                else
                    orderValue += actualPrice * item.Value;

            }
            return orderValue;
        }

    }
}


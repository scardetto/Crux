using System;
using System.Collections.Generic;
using System.Linq;
using Crux.Core.Extensions;

namespace Crux.Core
{
    public class MoneyDistributor<T> where T : class
    {
        private readonly IEnumerable<T> _items;

        public MoneyDistributor(IEnumerable<T> items)
        {
            _items = items;
        }

        public IEnumerable<Adjustment<T>> GetAdjustments(decimal amountToDistribute, Func<T, decimal> getItemWeight)
        {
            if (_items.IsBlank()) {
                return new Adjustment<T>[] { };
            }

            var adjustments = _items.Select(i => new Adjustment<T>(i, getItemWeight(i))).ToArray();
            var sumOfWeights = adjustments.Sum(a => a.Weight);
            var lastAdjustment = adjustments.Last();
            var remainder = amountToDistribute;

            adjustments.Each(a => {
                var adjustmentValue = (amountToDistribute*a.Weight/sumOfWeights).RoundToCurrency();
                a.Increment(adjustmentValue);
                remainder -= adjustmentValue;
            });

            lastAdjustment.Increment(remainder);

            return adjustments;
        }
    }

    public class Adjustment<T>
    {
        public T Item { get; private set; }
        public decimal Weight { get; private set; }
        public decimal Value { get; private set; }

        public Adjustment(T item, decimal weight)
        {
            Item = item;
            Weight = weight;
            Value = 0;
        }

        public void Increment(decimal value)
        {
            Value += value;
        }
    }
}

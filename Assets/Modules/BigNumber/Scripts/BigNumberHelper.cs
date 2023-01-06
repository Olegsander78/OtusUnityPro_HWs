using System;
using System.Numerics;
using UnityEngine;

internal static class BigNumberHelper
{
    internal static BigNumber.Order EvaluateOrder(BigInteger bigInteger)
    {
        var fullString = bigInteger.ToString();
        var length = fullString.Length;
        return (BigNumber.Order) ((length - 1) / 3);
    }

    internal static float EvaluateBaseValue(BigInteger bigInteger)
    {
        var fullString = bigInteger.ToString();
        var length = fullString.Length;
        if (length < 4)
            return Convert.ToInt32(fullString);

        var simbolsCount = (length - 1) % 3 + 2;
        var simbols = fullString.Substring(0, simbolsCount);
        var intValue = Convert.ToInt32(simbols);
        return intValue / 10f;
    }

    internal static BigInteger EvaluateBigInteger(float baseValue, BigNumber.Order order)
    {
        var intValue = Mathf.FloorToInt(baseValue);
        var decValue = Mathf.RoundToInt((baseValue - intValue) * 1000);

        var addZeroBlockCount = decValue == 0
            ? (int) order
            : (int) order - 1;

        //TODO: STRING BUILDER
        var strValue = intValue.ToString();
        if (decValue > 0)
        {
            strValue = $"{strValue}{decValue}";
        }

        if ((int) order >= (int) BigNumber.Order.K)
        {
            for (var i = 0; i < addZeroBlockCount; i++)
            {
                strValue = $"{strValue}000";
            }
        }

        return BigInteger.Parse(strValue);
    }
}
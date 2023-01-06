using System;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public struct BigNumber : ISerializationCallbackReceiver
{
    [Serializable]
    public enum Order
    {
        _ = 0,
        K = 1,
        M = 2,
        B = 3,
        T = 4,
        QA = 5,
        QI = 6,
        SX = 7,
        SP = 8,
        OC = 9,
        NO = 10,
        AA = 11,
        BB = 12,
        CC = 13,
        DD = 14,
        EE = 15,
        FF = 16,
        GG = 17,
        HH = 18,
        II = 19,
        JJ = 20,
        KK = 21,
        LL = 22,
        MM = 23,
        NN = 24,
        OO = 25,
        PP = 26,
        QQ = 27,
        RR = 28,
        SS = 29,
        TT = 30,
        UU = 31,
        VV = 32,
        WW = 33,
        XX = 34,
        YY = 35,
        ZZ = 36
    }
    
    public const string FORMAT_SERIALIZED = "SERIALIZED";
    
    //In Project!!!
    public const string FORMAT_DYNAMIC_3C = "DYNAMIC3C";

    private const string FORMAT_XXX_C = "XXX C";

    private const string FORMAT_XXXC = "XXXC";

    private const string FORMAT_XXX_XX_C = "XXX.XX C";

    private const string FORMAT_XXX_XXC = "XXX.XXC";

    private const string FORMAT_XXX_X_C = "XXX.X C";

    private const string FORMAT_XXX_XC = "XXX.XC";

    private const string FORMAT_DYNAMIC_3_C = "DYNAMIC3 C";

    private const string FORMAT_DYNAMIC_4_C = "DYNAMIC4 C";

    private const string FORMAT_DYNAMIC_4C = "DYNAMIC4C";

    [SerializeField]
    private float value;

    [SerializeField]
    private Order order;

    private BigInteger bigInteger;

    public BigNumber(float baseValue, Order order)
    {
        this.value = baseValue;
        this.order = order;
        this.bigInteger = BigNumberHelper.EvaluateBigInteger(baseValue, order);
    }

    public BigNumber(BigNumber bigNumber)
    {
        this.bigInteger = bigNumber.bigInteger;
        this.order = bigNumber.GetOrder();
        this.value = bigNumber.GetBaseValue();
    }

    public BigNumber(BigInteger bigInteger)
    {
        this.bigInteger = bigInteger;
        var maxValue = MaxNumber();
        if (bigInteger > maxValue.bigInteger)
        {
            this.bigInteger = maxValue.bigInteger;
        }

        this.order = BigNumberHelper.EvaluateOrder(this.bigInteger);
        this.value = BigNumberHelper.EvaluateBaseValue(this.bigInteger);
    }

    public BigNumber(int value)
    {
        this.bigInteger = value;
        this.order = BigNumberHelper.EvaluateOrder(this.bigInteger);
        this.value = BigNumberHelper.EvaluateBaseValue(this.bigInteger);
    }

    public BigNumber(string serializedValue)
    {
        this.bigInteger = BigInteger.Parse(serializedValue);
        this.order = BigNumberHelper.EvaluateOrder(this.bigInteger);
        this.value = BigNumberHelper.EvaluateBaseValue(this.bigInteger);
    }

    public int ToInt()
    {
        return (int) this.bigInteger;
    }

    public float GetBaseValue()
    {
        return BigNumberHelper.EvaluateBaseValue(this.bigInteger);
    }

    public Order GetOrder()
    {
        return BigNumberHelper.EvaluateOrder(this.bigInteger);
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        this.bigInteger = BigNumberHelper.EvaluateBigInteger(this.value, this.order);
    }

    public static BigNumber MaxNumber()
    {
        var countOfOrders = Enum.GetNames(typeof(Order)).Length;
        var finalValueString = "";
        for (var i = 0; i < countOfOrders; i++)
        {
            finalValueString = $"{finalValueString}999";
        }

        return new BigNumber(finalValueString);
    }

    public static BigNumber operator +(BigNumber num1, BigNumber num2)
    {
        BigInteger bigSum = num1.bigInteger + num2.bigInteger;
        return Clamp(new BigNumber(bigSum));
    }

    public static BigNumber operator -(BigNumber num1, BigNumber num2)
    {
        BigInteger result = num1.bigInteger - num2.bigInteger;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator /(BigNumber dividedNumb, BigNumber divider)
    {
        BigInteger result = dividedNumb.bigInteger / divider.bigInteger;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator *(BigNumber num1, BigNumber num2)
    {
        BigInteger result = num1.bigInteger * num2.bigInteger;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator +(BigNumber num, int value)
    {
        BigInteger result = num.bigInteger + value;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator -(BigNumber num, int value)
    {
        BigInteger result = num.bigInteger - value;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator *(BigNumber num1, int value)
    {
        BigInteger result = num1.bigInteger * value;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator /(BigNumber dividedNumb, int value)
    {
        BigInteger result = dividedNumb.bigInteger / value;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator %(BigNumber dividedNumb, int value)
    {
        BigInteger result = dividedNumb.bigInteger % value;
        return Clamp(new BigNumber(result));
    }

    public static BigNumber operator *(BigNumber num, double mul)
    {
        if (mul < 0)
            throw new Exception(string.Format("Multiplicator cannot be negative: {0}", mul));

        if (num.bigInteger < 100 && mul < 1000)
        {
            var intValue = (int) num.bigInteger;
            var floatMul = (float) mul;
            var result = Mathf.CeilToInt((intValue * floatMul));
            var bigIntResult = new BigInteger(result);
            return new BigNumber(bigIntResult);
        }

        var roundedMul = Math.Round(mul, 2);
        var mul100 = new BigInteger(roundedMul * 100);
        var bitIntResult = num.bigInteger * mul100 / 100;
        return new BigNumber(bitIntResult);
    }

    public static BigNumber operator /(BigNumber num, float div)
    {
        var div100 = Mathf.RoundToInt((float) Math.Round(div, 2) * 100);
        var num100 = num.bigInteger * 100;
        var result = num100 / div100;
        return new BigNumber(result);
    }

    private static BigNumber Clamp(BigNumber clampingValue)
    {
        var countOfOrders = Enum.GetNames(typeof(Order)).Length;
        var maxValueLength = countOfOrders * 3; // Every order contains 3 digits.
        var clampingValueLength = clampingValue.ToString(FORMAT_SERIALIZED).Length;

        if (clampingValueLength > maxValueLength)
            return MaxNumber() * clampingValue.bigInteger.Sign;

        return clampingValue;
    }

    public static double DivideToDouble(BigNumber dividedNumb, BigNumber divider)
    {
        return Math.Exp(BigInteger.Log(dividedNumb.bigInteger) - BigInteger.Log(divider.bigInteger));
    }

    public static double GetLog10(BigNumber bigNumber)
    {
        return BigInteger.Log10(bigNumber.bigInteger);
    }

    public static double GetLog(BigNumber bigNumber, int baseValue)
    {
        return BigInteger.Log(bigNumber.bigInteger, baseValue);
    }


    public static bool operator <=(BigNumber num1, BigNumber num2)
    {
        return num1.bigInteger <= num2.bigInteger;
    }

    public static bool operator >=(BigNumber num1, BigNumber num2)
    {
        return num1.bigInteger >= num2.bigInteger;
    }

    public static bool operator <(BigNumber num1, BigNumber num2)
    {
        return num1.bigInteger < num2.bigInteger;
    }

    public static bool operator >(BigNumber num1, BigNumber num2)
    {
        return num1.bigInteger > num2.bigInteger;
    }

    public static bool operator >=(BigNumber num, int intValue)
    {
        return num.bigInteger >= intValue;
    }

    public static bool operator <=(BigNumber num, int intValue)
    {
        return num.bigInteger <= intValue;
    }

    public static bool operator <(BigNumber num, int intValue)
    {
        return num.bigInteger < intValue;
    }

    public static bool operator >(BigNumber num, int intValue)
    {
        return num.bigInteger > intValue;
    }


    public static bool operator ==(BigNumber num, int intValue)
    {
        return num.bigInteger == intValue;
    }

    public static bool operator !=(BigNumber num, int intValue)
    {
        return num.bigInteger != intValue;
    }


    public static BigNumber RandomRange(BigNumber num1, BigNumber num2)
    {
        var random = RandomNumberGenerator.Create();
        BigInteger randomInteger = RandomInRange(random, num1.bigInteger, num2.bigInteger);
        return new BigNumber(randomInteger);
    }

    private static BigInteger RandomInRange(RandomNumberGenerator rng, BigInteger min, BigInteger max)
    {
        if (min > max)
        {
            (min, max) = (max, min);
        }

        // offset to set min = 0
        BigInteger offset = -min;
        min = 0;
        max += offset;

        var value = RandomInRangeFromZeroToPositive(rng, max) - offset;
        return value;
    }

    private static BigInteger RandomInRangeFromZeroToPositive(RandomNumberGenerator rng, BigInteger max)
    {
        BigInteger value;
        var bytes = max.ToByteArray();

        // count how many bits of the most significant byte are 0
        // NOTE: sign bit is always 0 because `max` must always be positive
        byte zeroBitsMask = 0b00000000;

        var mostSignificantByte = bytes[^1];

        // we try to set to 0 as many bits as there are in the most significant byte, starting from the left (most significant bits first)
        // NOTE: `i` starts from 7 because the sign bit is always 0
        for (var i = 7; i >= 0; i--)
        {
            // we keep iterating until we find the most significant non-0 bit
            if ((mostSignificantByte & (0b1 << i)) != 0)
            {
                var zeroBits = 7 - i;
                zeroBitsMask = (byte) (0b11111111 >> zeroBits);
                break;
            }
        }

        do
        {
            rng.GetBytes(bytes);

            // set most significant bits to 0 (because `value > max` if any of these bits is 1)
            bytes[^1] &= zeroBitsMask;

            value = new BigInteger(bytes);

            // `value > max` 50% of the times, in which case the fastest way to keep the distribution uniform is to try again
        } while (value > max);

        return value;
    }


    private bool Equals(BigNumber other)
    {
        return value.Equals(other.value) &&
               order == other.order &&
               bigInteger.Equals(other.bigInteger);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BigNumber) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = value.GetHashCode();
            hashCode = (hashCode * 397) ^ (int) order;
            hashCode = (hashCode * 397) ^ bigInteger.GetHashCode();
            return hashCode;
        }
    }


    #region ToString

    public override string ToString()
    {
        return this.ToString(FORMAT_XXX_XC);
    }

    public string ToString(string format)
    {
        return this.Formate(format);
    }

    private string Formate(string format)
    {
        format = format.ToUpperInvariant();

        if (String.IsNullOrEmpty(format) || (this.bigInteger < 1000 && format != FORMAT_SERIALIZED))
            format = FORMAT_XXX_C;

        var fullNumberToString = this.bigInteger.ToString();
        var numberLength = fullNumberToString.Length;
        var orderInt = (numberLength - 1) / 3;
        var m_order = (Order) orderInt;

        var olderNumbersLength = numberLength % 3 == 0 ? 3 : numberLength % 3;
        var olderNumberString = fullNumberToString.Substring(0, olderNumbersLength);
        var orderToString = m_order.ToString();
        if (m_order == 0)
            orderToString = "";

        var finalStringWithoutOrder = "";

        switch (format)
        {
            case FORMAT_XXX_XX_C:
                finalStringWithoutOrder =
                    this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, true);
                break;

            case FORMAT_XXX_XXC:
                finalStringWithoutOrder =
                    this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, false);
                break;

            case FORMAT_XXX_X_C:
                finalStringWithoutOrder =
                    this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, true);
                break;

            case FORMAT_XXX_XC:
                finalStringWithoutOrder =
                    this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, false);
                break;

            case FORMAT_XXX_C:
                finalStringWithoutOrder = $"{olderNumberString} ";
                break;

            case FORMAT_XXXC:
                finalStringWithoutOrder = $"{olderNumberString}";
                break;

            case FORMAT_SERIALIZED:
                return this.bigInteger.ToString();

            case FORMAT_DYNAMIC_3_C:
                finalStringWithoutOrder = olderNumbersLength switch
                {
                    1 => this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, true),
                    2 => this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, true),
                    3 => $"{olderNumberString} ",
                    _ => finalStringWithoutOrder
                };

                break;

            case FORMAT_DYNAMIC_3C:
                switch (olderNumbersLength)
                {
                    case 1:
                        finalStringWithoutOrder =
                            this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, false);
                        break;
                    case 2:
                        finalStringWithoutOrder =
                            this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, false);
                        break;
                    case 3:
                        finalStringWithoutOrder = $"{olderNumberString}";
                        break;
                }

                break;

            case FORMAT_DYNAMIC_4_C:
                if (olderNumbersLength < 3)
                {
                    finalStringWithoutOrder =
                        this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, true);
                }
                else
                {
                    finalStringWithoutOrder =
                        this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, true);
                }

                break;

            case FORMAT_DYNAMIC_4C:
                if (olderNumbersLength < 3)
                {
                    finalStringWithoutOrder =
                        this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, false);
                }
                else
                {
                    finalStringWithoutOrder =
                        this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, false);
                }

                break;

            default:
                throw new FormatException(String.Format("The '{0}' format string is not supported.", format));
        }

        return $"{finalStringWithoutOrder}{orderToString}";
    }

    private string GetFinalStringWithoutOrder(string fullNumberToString, string olderNumberString,
        int youngerNumbersLength, bool withSpace)
    {
        int olderNumbersLength = olderNumberString.Length;
        var youngerNumberString = $"{fullNumberToString.Substring(olderNumbersLength, youngerNumbersLength)}";
        if (Convert.ToInt32(youngerNumberString) == 0)
            youngerNumberString = "";
        else
            youngerNumberString = $".{youngerNumberString}";
        return $"{olderNumberString}{youngerNumberString}" + (withSpace ? " " : "");
    }

    #endregion

    public static BigNumber Max(BigNumber first, BigNumber second)
    {
        if (first > second)
        {
            return first;
        }

        return second;
    }

    public static BigNumber Min(BigNumber first, BigNumber second)
    {
        if (first < second)
        {
            return first;
        }

        return second;
    }
}
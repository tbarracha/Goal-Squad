
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsMath
{
    public static float RoundDecimals(float value, int decimalAmount)
        => (float)Math.Round(value, decimalAmount);

    public static int ConvertToMiliseconds(float seconds)
        => (int)(seconds * 0.001f);

    public static bool IsValueBetween(float value, float min, float max, bool inclusive = false)
    {
        if (inclusive == false)
        {
            if (value >= min && value <= max)
                return true;
            else
                return false;
        }

        else
        {
            if (value > min && value < max)
                return true;
            else
                return false;
        }
    }
    public static bool IsValueBetween(int value, int min, int max, bool inclusive = false)
    {
        if (inclusive == false)
        {
            if (value >= min && value <= max)
                return true;
            else
                return false;
        }

        else
        {
            if (value > min && value < max)
                return true;
            else
                return false;
        }
    }

    public static int GetIntegerFromFloat(float value)
        => (int) Math.Truncate(value);

    public static void SplitFloat(float value, ref int integer, ref float remainder)
    {
        integer = GetIntegerFromFloat(value);
        remainder = Mathf.Abs(integer - value);
    }

    public static float PercentageDifference(float a, float b)
    {
        // 1) Find the absolute difference between two numbers: | a - b |
        // 2) Find the average of those two numbers: (a + b) / 2
        // 3) Divide the difference by the average: | a - b | / ((a + b) / 2)
        // 4) Express the result as percentages by multiplying it by 100

        // percentage difference = 100 * | a - b | / ((a + b) / 2)

        return 100 * Mathf.Abs(a - b) / ((a + b) / 2 );
    }

    public static float PercentageBetweenMinMax(float min, float max, float t)
    {
        // If m is the minimum, M is the maximum, and t is the number of tenths:
        // m + t10(M−m)

        //return min + t / 100 * (max - min);
        return Mathf.Clamp((t - min) * 1 / (max - min), 0, 1);
    }

    public static float ValueBetweenMinMaxPercent(float min, float max, float percent)
    {
        return Mathf.Clamp((percent * (max - min) / 1) + min, 0, 1);
    }
}
using System;

public class RandomGenNum
{
    public Random random;

    public RandomGenNum()
    {
        random = new Random();
    }

    public int GetRandomMerchId()
    {
        return random.Next(1, 13);
    }

    public int GetDailyItemSaleNumber(int low, int high)
    {
        return random.Next(low, high);
    }

    public int SalePriceModifier()
    {
        return random.Next(0, 3);
    }

    public int GetRandomSaleTime(int totalDayTime)
    {
        return random.Next(0, totalDayTime + 1);
    }
}
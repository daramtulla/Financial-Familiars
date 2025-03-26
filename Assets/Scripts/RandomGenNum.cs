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
        return random.Next(1, 19);
    }

    public int GetDailyItemSaleNumber(int low, int high)
    {
        return random.Next(low, high);
    }

    public int SalePriceModifier()
    {
        return random.Next(1, 4);
    }

    public int GetRandomSaleTime(int totalDayTime)
    {
        return random.Next(10, totalDayTime + 1);
    }

    public int GetRandomCustomerEntrance(int totalDayTime)
    {
        return random.Next(0, totalDayTime + 1);
    }


    public int GetRandomLoanAmount(int low, int high)
    {
        return random.Next(low, high);
    }

    public int GetLoanInterestModifier()
    {
        return random.Next(-3, 4);
    }

    public int GetBinary()
    {
        return random.Next(0, 2);
    }
}
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
}
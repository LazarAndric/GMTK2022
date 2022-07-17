using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtil
{
    public static List<int> getAvgRandom(int numberOfItems, int min, int max, float range)
    {
        int currentAvg=(int)((min+1)+range*((max-1)-(min+1)));
        List<int> result = new List<int>();
        int iteratorMax= max;
        int iteratorMin= min;
        int positiveSign = +1;
        int negativeSign = -1;
        while (result.Count != numberOfItems)
        {
            if (iteratorMax+ negativeSign < currentAvg || iteratorMax + negativeSign > max)
            {
                negativeSign *= -1;
            }
            iteratorMax += negativeSign;
            if (iteratorMin + positiveSign > currentAvg || iteratorMin +positiveSign < 0)
            {
                positiveSign *= -1;
            }
            iteratorMin += positiveSign;
            result.Add(Random.Range(iteratorMin, currentAvg));
            result.Add(Random.Range(currentAvg, iteratorMax));
        }
        return result;
    }
}

using System.Numerics;

namespace AdventOfCode2023.CSharp;

public static class MathUtility
{
    
    // The least common multiple (LCM) is the smallest number that two or more numbers can divide into evenly.
    // To find the LCM, you can use the prime factorization method or list the multiples of each number.
    public static long LeastCommonMultiple(List<long> input)
    {
        var queue = new Queue<long>(input.Count * 2);

        foreach (var item in input)
        {
            queue.Enqueue(item);
        }
        
        while (true)
        {
            long left;
            
            long right;
            
            if (queue.Count == 2)
            {
                left = queue.Dequeue();

                right = queue.Dequeue();

                return left * right / GreatestCommonFactor(left, right);
            }

            left = queue.Dequeue();

            right = queue.Dequeue();

            var lowestCommonMultiple = left * right / GreatestCommonFactor(left, right);

            queue.Enqueue(lowestCommonMultiple);
        }
    }

    // The greatest common factor (GCF) of a set of numbers is the largest factor that all the numbers share. 
    // For example, 12, 20, and 24 have two common factors: 2 and 4. The largest is 4, so we say that the 
    // GCF of 12, 20, and 24 is 4. GCF is often used to find common denominators.
    public static long GreatestCommonFactor(long left, long right)
    {
        var gcdExponentOnTwo = BitOperations.TrailingZeroCount(left | right);

        left >>= gcdExponentOnTwo;
        
        right >>= gcdExponentOnTwo;

        while (left != right)
        {
            if (left < right)
            {
                right -= left;

                right >>= BitOperations.TrailingZeroCount(right);
            }
            else
            {
                left -= right;

                left >>= BitOperations.TrailingZeroCount(left);
            }
        }

        return left << gcdExponentOnTwo;
    }
}

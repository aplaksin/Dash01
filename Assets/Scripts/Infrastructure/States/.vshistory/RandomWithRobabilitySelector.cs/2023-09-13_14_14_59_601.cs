using UnityEngine;

public static class RandomWithRobabilitySelector
{
    public static int GetRandom(int[] nums, int[] probability)
    {
        int choisesArrayLength = nums.Length;
        
        if (choisesArrayLength != probability.Length)
        {
            return -1;        
        }

        int[] probabilitySum = new int[choisesArrayLength];

        // `prob_sum[i]` содержит сумму всех `probability[j]` для `0 <= j <= i`
        probabilitySum[0] = probability[0];
        int totalProbability = probability[0];
        for (int i = 1; i < choisesArrayLength; i++)
        {
            probabilitySum[i] = probabilitySum[i - 1] + probability[i];
            totalProbability += probability[i];
        }

        // генерируем случайное целое число от 1 до 100 и проверяем, где оно лежит
        // в `prob_sum[]`
        int randomWeight = Random.Range(0, totalProbability);

        // по результату сравнения возвращаем соответствующий
        // элемент из входного списка

        if (randomWeight <= probabilitySum[0])
        {     // обрабатываем 0-й индекс отдельно
            return nums[0];
        }

        for (int i = 1; i < totalProbability; i++)
        {
            if (randomWeight > probabilitySum[i - 1] && randomWeight <= probabilitySum[i])
            {
                return nums[i];
            }
        }

        return -1;
    }
}
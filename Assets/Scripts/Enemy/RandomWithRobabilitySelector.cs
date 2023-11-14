using System;
using UnityEngine;

public static class RandomWithRobabilitySelector
{
    public static T GetRandom<T>(T[] variants, float[] probability)
    {
        int choisesArrayLength = variants.Length;

        if (choisesArrayLength != probability.Length)
        {
            throw new ArgumentException("choisesArrayLength != probability.Length");
        }

        float[] probabilitySum = new float[choisesArrayLength];

        // `prob_sum[i]` содержит сумму всех `probability[j]` для `0 <= j <= i`
        probabilitySum[0] = probability[0];
        float totalProbability = probability[0];
        for (int i = 1; i < choisesArrayLength; i++)
        {
            probabilitySum[i] = probabilitySum[i - 1] + probability[i];
            totalProbability += probability[i];
        }

        // генерируем случайное целое число от 1 до 100 и проверяем, где оно лежит
        // в `prob_sum[]`
        float randomWeight = UnityEngine.Random.Range(0f, totalProbability);

        // по результату сравнения возвращаем соответствующий
        // элемент из входного списка

        if (randomWeight <= probabilitySum[0])
        {     // обрабатываем 0-й индекс отдельно
            return variants[0];
        }

        for (int i = 1; i < totalProbability; i++)
        {
            if (randomWeight > probabilitySum[i - 1] && randomWeight <= probabilitySum[i])
            {
                return variants[i];
            }
        }

        throw new ArgumentException("Cant choose variant");

    }
}
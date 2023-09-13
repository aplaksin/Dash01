using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private List<Vector2> _spawnCoordinaresList = new List<Vector2>();
    private const int ENEMY_Y_SPAWN_POINT = 10;
    private RandomWithRobabilitySelector _randomWithRobabilitySelector;
    public EnemySpawner(IGameFactory gameFactory)
    {
        
        foreach (Vector2 key in gameFactory.BlocksCoords.Keys)
        {
            _spawnCoordinaresList.Add(new Vector2(key.x, ENEMY_Y_SPAWN_POINT));
            _randomWithRobabilitySelector = new RandomWithRobabilitySelector();
        }
        
    }

    public Vector2 GetRandomSpawnPoint()
    {
        return _spawnCoordinaresList[Random.Range(0, _spawnCoordinaresList.Count)];
    }


}

public static class RandomWithRobabilitySelector
{
    public static int GetRandom(int[] nums, int[] probability)
    {
        int choisesArrayLength = nums.Length;
        
        if (choisesArrayLength != probability.Length)
        {
            return -1;        
        }

        int[] prob_sum = new int[choisesArrayLength];

        // `prob_sum[i]` содержит сумму всех `probability[j]` для `0 <= j <= i`
        prob_sum[0] = probability[0];
        int totalProbability = probability[0];
        for (int i = 1; i < choisesArrayLength; i++)
        {
            prob_sum[i] = prob_sum[i - 1] + probability[i];
            totalProbability += probability[i];
        }

        // генерируем случайное целое число от 1 до 100 и проверяем, где оно лежит
        // в `prob_sum[]`
        int randomWeight = Random.Range(0, totalProbability);

        // по результату сравнения возвращаем соответствующий
        // элемент из входного списка

        if (randomWeight <= prob_sum[0])
        {     // обрабатываем 0-й индекс отдельно
            return nums[0];
        }

        for (int i = 1; i < totalProbability; i++)
        {
            if (randomWeight > prob_sum[i - 1] && randomWeight <= prob_sum[i])
            {
                return nums[i];
            }
        }

        return -1;
    }
}
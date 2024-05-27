using System.Linq;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float initialSpawnHeightIncrement = 10f; // Incremento de altura inicial para el próximo conjunto de obstáculos
    public float obstacleSpacing = 5f; // Distancia entre los obstáculos generados
    public GameObject player;
    public float decrementFactor = 0.1f; // Factor de decremento para el incremento de altura

    private GameObject lastSpawnedObstacle;
    private int score;
    private float currentSpawnHeightIncrement;
    private float baseWheelRotation = 100f;
    private float speedDifference = 10f; // Diferencia de velocidad entre las ruedas

    void Start()
    {
        lastSpawnedObstacle = null;
        currentSpawnHeightIncrement = initialSpawnHeightIncrement;
        SpawnObstacles(); // Generar el primer conjunto de obstáculos
    }

    void Update()
    {
        // Generar nuevos obstáculos cuando el jugador haya superado al último obstáculo generado
        if (lastSpawnedObstacle == null || player.transform.position.y >= lastSpawnedObstacle.transform.position.y)
        {
            SpawnObstacles();
        }

        // Eliminar obstáculos que están suficientemente lejos por debajo del jugador
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.y < player.transform.position.y - 15f)
            {
                Destroy(obstacle);
                // Incrementar la puntuación por cada obstáculo destruido
                score++;
                UpdateWheelRotation();
            }
        }
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < 3; i++)
        {
            float spawnHeight = (lastSpawnedObstacle != null ? lastSpawnedObstacle.transform.position.y : transform.position.y) + currentSpawnHeightIncrement + (i * obstacleSpacing);
            lastSpawnedObstacle = SpawnObstacle(spawnHeight);
        }
        currentSpawnHeightIncrement = Mathf.Max(2f, currentSpawnHeightIncrement - decrementFactor); // Reducir el incremento de altura con un límite inferior
    }

    GameObject SpawnObstacle(float targetHeight)
    {
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Vector3 spawnPos = new Vector3(0, targetHeight, 0f); // Fijar la posición en el eje X
        GameObject spawnedObstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Asumiendo que las ruedas son los primeros dos hijos del prefab
        Transform wheel1 = spawnedObstacle.transform.GetChild(0);
        Transform wheel2 = spawnedObstacle.transform.GetChild(1);

        // Configurar la dirección de rotación
        WheelRotation wheelRotation1 = wheel1.GetComponent<WheelRotation>();
        WheelRotation wheelRotation2 = wheel2.GetComponent<WheelRotation>();

        if (wheelRotation1 != null)
        {
            wheelRotation1.rotateClockwise = true; // Rueda 1 en sentido horario
            wheelRotation1.SetWheelRotation(baseWheelRotation, speedDifference); // Configurar velocidad
        }
        if (wheelRotation2 != null)
        {
            wheelRotation2.rotateClockwise = false; // Rueda 2 en sentido antihorario
            wheelRotation2.SetWheelRotation(baseWheelRotation, -speedDifference); // Configurar velocidad
        }

        return spawnedObstacle;
    }

    void UpdateWheelRotation()
    {
        float newWheelRotation = baseWheelRotation + score * 0.1f;
        foreach (GameObject wheel in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            WheelRotation wheelRotationScript = wheel.GetComponent<WheelRotation>();
            if (wheelRotationScript != null)
            {
                wheelRotationScript.SetWheelRotation(newWheelRotation, speedDifference);
            }
        }
    }

}

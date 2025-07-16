using UnityEngine;

public class GrassSpawnerOnCubes : MonoBehaviour
{
    public GameObject grassPrefab;
    public int countPerCube = 1000;
    public GameObject[] cubes;
    public float spawnYPosition = 1.0f;

    void Start()
    {
        if (grassPrefab == null || cubes == null || cubes.Length == 0)
        {
            Debug.LogError("Assign grassPrefab and at least one cube!");
            return;
        }

        int totalSpawned = 0;

        foreach (GameObject cube in cubes)
        {
            if (cube == null) continue;

            BoxCollider box = cube.GetComponent<BoxCollider>();
            if (box == null)
            {
                Debug.LogWarning($"Cube '{cube.name}' needs a BoxCollider!");
                continue;
            }

            Vector3 cubePos = cube.transform.position;
            Vector3 cubeScale = cube.transform.localScale;

            float halfX = box.size.x * cubeScale.x / 2f;
            float halfZ = box.size.z * cubeScale.z / 2f;
            float spawnY = cubePos.y + (box.size.y * cubeScale.y / 2f) + spawnYPosition;

            for (int i = 0; i < countPerCube; i++)
            {
                float xPos = Random.Range(cubePos.x - halfX, cubePos.x + halfX);
                float zPos = Random.Range(cubePos.z - halfZ, cubePos.z + halfZ);
                Vector3 spawnPos = new Vector3(xPos, spawnY, zPos);

                // Always instantiate and parent immediately
                GameObject grass = Instantiate(grassPrefab, spawnPos, Quaternion.Euler(0, Random.Range(0, 360), 0));

                // Parent correctly
                grass.transform.SetParent(cube.transform, worldPositionStays: true);
            }

            totalSpawned += countPerCube;
            Debug.Log($"Spawned {countPerCube} grass on '{cube.name}'");
        }

        Debug.Log("Total grass spawned: " + totalSpawned);
    }
}

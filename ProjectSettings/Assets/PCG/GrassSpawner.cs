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

            int spawnedThisCube = 0;

            for (int i = 0; i < countPerCube; i++)
            {
                float xPos = Random.Range(cubePos.x - halfX, cubePos.x + halfX);
                float zPos = Random.Range(cubePos.z - halfZ, cubePos.z + halfZ);

                Vector3 spawnPos = new Vector3(xPos, spawnY, zPos);

                if (spawnPos.x >= cubePos.x - halfX && spawnPos.x <= cubePos.x + halfX &&
                    spawnPos.z >= cubePos.z - halfZ && spawnPos.z <= cubePos.z + halfZ)
                {
                    GameObject grass = Instantiate(grassPrefab, spawnPos, Quaternion.Euler(0, Random.Range(0, 360), 0));

                    // Parent the grass to the cube so it moves along with it
                    grass.transform.SetParent(cube.transform);

                    spawnedThisCube++;
                }
            }

            Debug.Log($"Spawned {spawnedThisCube} grass on '{cube.name}'");
            totalSpawned += spawnedThisCube;
        }

        Debug.Log("Total grass spawned: " + totalSpawned);
    }
}

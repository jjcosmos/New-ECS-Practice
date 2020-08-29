using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class SpawnerMB : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] int xSize = 10;
    [SerializeField] int ySize = 10;
    [Range(0f, 10f)]
    [SerializeField] float spacing = 1f;
    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager entityManager;
    private BlobAssetStore blobAssetStore;
    private void Awake()
    {
        blobAssetStore = new BlobAssetStore();
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        entityManager = defaultWorld.EntityManager;
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, blobAssetStore);
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(cubePrefab, settings);

        InstantiateEntityGrid(xSize, ySize, spacing);
    }

    private void InstantiateEntityGrid(int dimX, int dimY, float spacing = 1f)
    {
        for(int i = 0; i < dimX; i++)
        {
            for (int j = 0; j < dimY; j++)
            {
                InstantiateEntity(new float3(i * spacing, j * spacing, 0f));
            }
        }
    }

    private void InstantiateEntity(float3 position)
    {
        Entity myEntity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(myEntity, new Translation { Value = position });
    }

    private void OnDisable() { blobAssetStore.Dispose(); }
}

// Use sparingly >:(
using UnityEngine;

public static class Helpers
{
    public static void PlaceDecalOnGround(Vector3 position, GameObject decal)
    {
        position.y = Constants.WorldProperties.GroundLevel + 1f;
        Ray ray = new Ray(position, Vector3.down);
        Physics.Raycast(ray, out RaycastHit hitInfo, 10f, Constants.Layers.Ground);
        decal.transform.position = hitInfo.point;
        decal.transform.forward = hitInfo.normal * -1f;
        decal.transform.rotation = decal.transform.rotation * Quaternion.AngleAxis(Random.Range(0, 360), decal.transform.up);
    }

    public static void Destroy(GameObject gameObject, float duration = 0f)
    {
        if (gameObject.TryGetComponent<PoolObject>(out PoolObject poolObject))
        {
            poolObject.transform.parent = null;
            poolObject.ReturnToPool(duration);
        }
        else
        {
            GameObject.Destroy(gameObject, duration);
        }
    }
}
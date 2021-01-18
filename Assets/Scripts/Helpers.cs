// Use sparingly >:(
using System.Collections.Generic;
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

    public static Vector3 GetVectorBetween(Vector3 pos1, Vector3 pos2)
    {
        Vector3 diffVector = pos1 - pos2;
        diffVector.y = 0;
        return diffVector;
    }

    public static Vector3 GetVectorBetween(GameObject obj1, GameObject obj2)
    {
        return GetVectorBetween(obj1.transform.position, obj2.transform.position);
    }

    public static float GetDistBetweenColliders(Collider c1, Collider c2)
    {
        Vector3 closestC1 = c1.ClosestPoint(c2.transform.position);
        Vector3 closestC2 = c2.ClosestPoint(c1.transform.position);
        return Vector3.Distance(closestC1, closestC2);
    }

    public static float GetDistanceBetweenCharacters(Character c1, Character c2)
    {
        if (c1 == null || c2 == null)
        {
            return float.MaxValue;
        }

        return GetDistBetweenColliders(c1.Body.Collider, c2.Body.Collider);
    }

    public static Quaternion RotationTowards(Character target, Character source)
    {
        return Quaternion.LookRotation(GetVectorBetween(target.gameObject, source.gameObject));
    }

    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }

        return null;
    }
}
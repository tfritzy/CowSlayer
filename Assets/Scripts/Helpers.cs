// Use sparingly >:(
using System.Collections.Generic;
using System.Text;
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
        Vector3 c2Pos = c2.transform.position;
        c2Pos.y = 0;
        Vector3 c1Pos = c1.transform.position;
        c1Pos.y = 0;
        Vector3 closestC1 = c1.ClosestPoint(c2Pos);
        Vector3 closestC2 = c2.ClosestPoint(c1Pos);
        return Vector3.Distance(closestC1, closestC2);
    }

    public static Quaternion RotationTowards(Character target, Character source)
    {
        return Quaternion.LookRotation(GetVectorBetween(target.gameObject, source.gameObject));
    }

    public static Transform FindDeepChild(this Transform aParent, string aName, bool errorIfNotFound = false)
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

        if (errorIfNotFound)
        {
            throw new System.Exception($"Unable to find required transform '{aName}' in hierarchy of '{aParent.name}'");
        }

        return null;
    }

    public static Vector3 PlacePointOnGround(Vector3 point)
    {
        point.y = Constants.WorldProperties.GroundLevel;
        return point;
    }

    public static string GenerateId(string prefix)
    {
        StringBuilder sb = new StringBuilder($"{prefix}_");
        for (int i = 0; i < 20; i++)
        {
            char c = (char)('a' + Random.Range(0, 51));
            sb.Append(c);
        }

        return sb.ToString();
    }
}
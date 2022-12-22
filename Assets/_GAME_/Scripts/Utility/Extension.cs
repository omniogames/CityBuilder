using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

public static class Extension
{

    #region Task Extensions
    public delegate void Task();

    /// <summary>
    /// Runs the given task one time after a delay.
    /// </summary>
    /// <param name="delay">Delay before running the task.</param>
    /// <param name="task">Task to run.</param>
    public static void Run(this MonoBehaviour behaviour, float delay, Task task)
    {
        behaviour.StartCoroutine(RunTask(delay, task));
    }

    private static IEnumerator RunTask(float delay, Task task)
    {
        yield return new WaitForSeconds(delay);
        task.Invoke();
    }
    #endregion

    #region Transform
    /// <summary>
    /// Destroys children of a transform with reverse for loop.
    /// </summary>
    /// <param name="keep">How many children to keep from start</param>
    public static void DestroyChildren(this Transform transform, int keep = 0)
    {
        for (int i = transform.childCount - 1; i >= keep; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
    #endregion
    
    #region Vector3 Extensions
    /// <summary>
    /// Overrides x component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="x">New x component value.</param>
    /// <returns>Vector3 with x component set to "x" value.</returns>
    public static Vector3 OverrideX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    /// <summary>
    /// Overrides y component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="y">New y component value.</param>
    /// <returns>Vector3 with y component set to "y" value.</returns>
    public static Vector3 OverrideY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    /// <summary>
    /// Overrides z component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="z">New z component value.</param>
    /// <returns>Vector3 with z component set to "z" value.</returns>
    public static Vector3 OverrideZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    /// <summary>
    /// Adds a Vector2 to the this Vector3.
    /// </summary>
    /// <param name="addVector">Vector2 to add.</param>
    /// <param name="axes">Which axes to to add to.</param>
    /// <returns></returns>
    public static Vector3 Add(this Vector3 vector, Vector2 addVector, Vector3Axis axes = Vector3Axis.XYZ)
    {
        switch (axes)
        {
            case Vector3Axis.XY:
                return vector + new Vector3(addVector.x, addVector.y, 0f);
            case Vector3Axis.XZ:
                return vector + new Vector3(addVector.x, 0f, addVector.y);
            case Vector3Axis.YZ:
                return vector + new Vector3(0f, addVector.x, addVector.y);
            default:
                Debug.LogWarning($"{axes} is not with Add(Vector2, Vector3Axis)");
                return default;
        }
    }

    /// <summary>
    /// Adds to x component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="x">Value to add to x component.</param>
    /// <returns>Vector3 with "x" value added to x component.</returns>
    public static Vector3 AddX(this Vector3 vector, float x)
    {
        return new Vector3(vector.x + x, vector.y, vector.z);
    }

    /// <summary>
    /// Adds to y component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="y">Value to add to y component.</param>
    /// <returns>Vector3 with "y" value added to y component.</returns>
    public static Vector3 AddY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, vector.y + y, vector.z);
    }

    /// <summary>
    /// Adds to z component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="z">Value to add to z component.</param>
    /// <returns>Vector3 with "z" value added to z component.</returns>
    public static Vector3 AddZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, vector.z + z);
    }

    /// <summary>
    /// Multiplies x component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="x">Value to multiply x component with.</param>
    /// <returns>Vector3 with x component multiplied with "x" value.</returns>
    public static Vector3 MuliplyX(this Vector3 vector, float x)
    {
        return new Vector3(vector.x * x, vector.y, vector.z);
    }

    /// <summary>
    /// Multiplies y component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="y">Value to multiply y component with.</param>
    /// <returns>Vector3 with y component multiplied with "y" value.</returns>
    public static Vector3 MuliplyY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, vector.y * y, vector.z);
    }

    /// <summary>
    /// Multiplies z component of a Vector3 and returns the result.
    /// </summary>
    /// <param name="z">Value to multiply z component with.</param>
    /// <returns>Vector3 with z component multiplied with "z" value.</returns>
    public static Vector3 MuliplyZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, vector.z * z);
    }
    #endregion

    #region Vector2 Extensions
    /// <summary>
    /// Overrides x component of a Vector2 and returns the result.
    /// </summary>
    /// <param name="x">New x component value.</param>
    /// <returns>Vector2 with x component set to "x" value.</returns>
    public static Vector2 OverrideX(this Vector2 vector, float x)
    {
        return new Vector2(x, vector.y);
    }

    /// <summary>
    /// Overrides y component of a Vector2 and returns the result.
    /// </summary>
    /// <param name="y">New y component value.</param>
    /// <returns>Vector2 with y component set to "y" value.</returns>
    public static Vector2 OverrideY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, y);
    }

    /// <summary>
    /// Adds to x component of a Vector2 and returns the result.
    /// </summary>
    /// <param name="x">Value to add to x component.</param>
    /// <returns>Vector2 with "x" value added to x component.</returns>
    public static Vector2 AddX(this Vector2 vector, float x)
    {
        return new Vector2(vector.x + x, vector.y);
    }

    /// <summary>
    /// Adds to y component of a Vector2 and returns the result.
    /// </summary>
    /// <param name="y">Value to add to y component.</param>
    /// <returns>Vector2 with "y" value added to y component.</returns>
    public static Vector2 AddY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, vector.y + y);
    }

    /// <summary>
    /// Multiplies x component of a Vector2 and returns the result.
    /// </summary>
    /// <param name="x">Value to multiply x component with.</param>
    /// <returns>Vector2 with x component multiplied with "x" value.</returns>
    public static Vector2 MuliplyX(this Vector2 vector, float x)
    {
        return new Vector2(vector.x * x, vector.y);
    }

    /// <summary>
    /// Multiplies y component of a Vector2 and returns the result.
    /// </summary>
    /// <param name="y">Value to multiply y component with.</param>
    /// <returns>Vector2 with y component multiplied with "y" value.</returns>
    public static Vector2 MuliplyY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, vector.y * y);
    }
    #endregion

    #region Material Property Block
    /// <summary>
    /// Sets the color of a material.
    /// </summary>
    /// <param name="name">Name of the color property to set.</param>
    /// <param name="color">Color value.</param>
    /// <param name="materialIndex">Index of the material in renderer materials.</param>
    public static void SetColor(this Renderer renderer, string name, Color color, int materialIndex = 0)
    {
        MaterialPropertyBlock prop = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(prop, materialIndex);
        prop.SetColor(name, color);
        renderer.SetPropertyBlock(prop, materialIndex);
    }

    /// <summary>
    /// Sets the float value of a material.
    /// </summary>
    /// <param name="name">Name of the float property to set.</param>
    /// <param name="value">float value.</param>
    /// <param name="materialIndex">Index of the material in renderer materials.</param>
    public static void SetFloat(this Renderer renderer, string name, float value, int materialIndex = 0)
    {
        MaterialPropertyBlock prop = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(prop, materialIndex);
        prop.SetFloat(name, value);
        renderer.SetPropertyBlock(prop, materialIndex);
    }

    /// <summary>
    /// Sets the Vector4 value of a material.
    /// </summary>
    /// <param name="name">Name of the Vector4 property to set.</param>
    /// <param name="vector">Vector4 value.</param>
    /// <param name="materialIndex">Index of the material in renderer materials.</param>
    public static void SetVector(this Renderer renderer, string name, Vector4 vector, int materialIndex = 0)
    {
        MaterialPropertyBlock prop = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(prop, materialIndex);
        prop.SetVector(name, vector);
        renderer.SetPropertyBlock(prop, materialIndex);
    }
    #endregion

    #region List Extension
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        for (var i = 0; i < count - 1; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    /// <summary>
    /// Copies a List<> excluding excludes.
    /// </summary>
    /// <param name="excludes">Exclude these objects from List<></param>
    /// <returns>Copied List<></returns>
    public static List<T> CopyExcluding<T>(this List<T> list, params T[] excludes)
    {
        List<T> copy = new List<T>(list);
        foreach (T exclude in excludes)
        {
            copy.Remove(exclude);
        }
        return copy;
    }
    #endregion

    #region Particle System
    /// <summary>
    /// Sets start color of a particle system.
    /// </summary>
    /// <param name="color">Color value.</param>
    public static void SetStartColor(this MainModule mainModule, Color color)
    {
        mainModule.startColor = color;
    }
    #endregion

    #region Number Extensions
    /// <summary>
    /// Formats the number to be shown as text.
    /// </summary>
    /// <returns>Number as string to show.</returns>
    public static string FormatNumberText(this float number)
    {
        foreach (KeyValuePair<int, string> numberAbbr in Consts.NumberAbbrs)
        {
            if (Mathf.Abs(number) >= numberAbbr.Key)
            {
                float newNumber = number / numberAbbr.Key;
                return $"{newNumber:#.00}{numberAbbr.Value}";
            }
        }
        return number.ToString("#0.00");
    }

    /// <summary>
    /// Remaps the value from from1-to1 to from2-to2.
    /// </summary>
    /// <param name="from1">Start of first interval.</param>
    /// <param name="to1">End of first interval.</param>
    /// <param name="from2">Start of second interval.</param>
    /// <param name="to2">End of second interval.</param>
    /// <returns>Remaped value.</returns>
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    #endregion

    #region Random
    /// <summary>
    /// Generates a random index within start(inclusive) and end(inclusive), excluding given indexes.
    /// </summary>
    /// <param name="start">Start of indexes.</param>
    /// <param name="end">End of indexes (should be bigger than start).</param>
    /// <param name="exclude">Exclude indexes</param>
    /// <returns></returns>
    public static int RandomIndex(int start, int end, params int[] exclude)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i <= end - start; i++)
        {
            indexes.Add(i + start);
        }
        foreach (int ex in exclude)
        {
            if (ex >= start && ex <= end)
            {
                indexes.Remove(ex);
            }
        }
        return indexes[Random.Range(0, indexes.Count)];
    }

    /// <summary>
    /// Returns a random element from the list.
    /// </summary>
    /// <param name="removeElement">If true removes the selected random element form the list. True by default.<s/param>
    /// <typeparam name="T">List element type.</typeparam>
    /// <returns></returns>
    public static T RandomElement<T>(this List<T> list, bool removeElement = true)
    {
        if (list.Count <= 0) return default(T);
        T randomElement = list[Random.Range(0, list.Count)];
        if (removeElement)
        {
            list.Remove(randomElement);
        }
        return randomElement;
    }
    #endregion

    #region NavMesh
    /// <summary>
    /// Gets random position form navmesh.
    /// </summary>
    /// <param name="randomPos">Random position found on navmesh.</param>
    /// <param name="padding">Padding from navmesh bounds. Padding area
    /// is not included on random position search.</param>
    /// <param name="tryCount">How many times to try to get random position. Default 5.</param>
    /// <returns>Bool value indicating if a random position is found on navmesh.</returns>
    public static bool GetRandomPoint(this NavMeshData navMeshData, out Vector3 randomPos, float padding = 0f, int tryCount = 5)
    {
        Vector3 navMeshCenter = navMeshData.sourceBounds.center;
        Vector3 navMeshExtends = navMeshData.sourceBounds.extents;
        float randomX = Random.Range(navMeshCenter.x - navMeshExtends.x + padding,
            navMeshCenter.x + navMeshExtends.x - padding);
        float randomZ = Random.Range(navMeshCenter.z - navMeshExtends.z + padding,
             navMeshCenter.z + navMeshExtends.z - padding);
        Vector3 randomPoint = new Vector3(randomX, navMeshCenter.y, randomZ);
        float maxDistance = Mathf.Sqrt(navMeshExtends.x * navMeshExtends.x + navMeshExtends.y * navMeshExtends.y);

        NavMeshHit hit = new NavMeshHit();
        bool positionFound = false;
        int i = 0;
        while (!positionFound && i < tryCount)
        {
            positionFound = NavMesh.SamplePosition(randomPoint, out hit, maxDistance, NavMesh.AllAreas);
            i++;
        }
        if (!positionFound)
        {
            Debug.LogError("Random position not found!");
        }
        randomPos = hit.position;
        return positionFound;
    }

    /// <summary>
    /// Gets the closestPoint in navmesh.
    /// </summary>
    /// <param name="maxDistance">Max distance to search for from the position.</param>
    /// <param name="closestPoint">Closest point found on navmesh.</param>
    /// <param name="tryCount">How many times to try to get random position. Default 5.</param>
    /// <returns>Bool value indicating if a position is found on navmesh.</returns>
    public static bool ClosestPointOnNavmesh(this Vector3 position, float maxDistance, out Vector3 closestPoint, int tryCount = 5)
    {
        NavMeshHit hit = new NavMeshHit();
        bool positionFound = false;
        int i = 0;
        while (!positionFound && i < tryCount)
        {
            positionFound = NavMesh.SamplePosition(position, out hit, maxDistance, NavMesh.AllAreas);
            i++;
        }
        if (!positionFound)
        {
            Debug.LogError("No position found on navmesh!");
        }
        closestPoint = hit.position;
        return positionFound;
    }
    #endregion

    #region VirtualCamera
    /// <summary>
    /// Sets damping of the transposer of the virtual camera.
    /// </summary>
    /// <param name="damping">Vector3 value for damping.</param>
    public static void SetTransposerDamping(this CinemachineVirtualCamera vcam, Vector3 damping)
    {
        CinemachineTransposer transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_XDamping = damping.x;
        transposer.m_YDamping = damping.y;
        transposer.m_ZDamping = damping.z;
    }
    #endregion
    
    #region Debug
    public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
    }
    #endregion

}

public enum Vector3Axis
{
    None, X, Y, Z, XY, XZ, YZ, XYZ
}

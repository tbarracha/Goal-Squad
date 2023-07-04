
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using StardropTools;
using System.Security;
using Unity.VisualScripting;

/// <summary>
/// Class that contains miscellanious static utilities
/// </summary>
public static class Utilities
{
    static Camera camera;

    public static readonly int[] OneAndNegativeOne = { 1, -1 };

    //public static int RandomOneOrNegativeOne => OneAndNegativeOne.GetRandom();

    #region Debug & Log
    public const string DebugAlert = "<color=orange>Debug: </color>";

    /// <summary>
    /// Logs "Debug:" + message, in default ORANGE color
    /// </summary>
    public static void LogDebug(object message)
    {
        Debug.Log(DebugAlert + message);
    }

    /// <summary>
    /// Logs "Debug:" + message, in CHOSEN color
    /// <para> Colors: red, orange, yellow, white, magenta, cyan, black, gray </para>
    /// </summary>
    public static void LogDebug(string color, object message)
    {
        Debug.Log("<color=" + color + "> Debug: </color>" + message);
    }


    /// <summary>
    /// Logs a message in the Unity Console with CHOSEN Unity color
    /// <para> Colors: red, orange, white, magenta, cyan, black, gray </para>
    /// </summary>
    public static void LogColored(string color, object message)
    {
        Debug.Log("<color=" + color + ">" + message + "</color>");
    }

#if UNITY_EDITOR
    public static void ClearLog() //you can copy/paste this code to the bottom of your script
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
#endif
    #endregion


    public static bool RandomTrueOrFalse() => ConvertIntToBool(Random.Range(0, 2));

    public static int RandomOneOrNegativeOne() => OneAndNegativeOne.GetRandom();

    public static int GetNextIndex<T>(T[] array, int currentIndex)
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogError("Array is null or empty!");
            return -1; // Return an invalid index value
        }

        int nextIndex = (currentIndex + 1) % array.Length;
        return nextIndex;
    }

    /// <summary>
    /// Invokes the InitializeManager() method on an array of IManager
    /// </summary>
    public static void InitializeManagers(IManager[] managers)
    {
        if (managers.Exists())
            for (int i = 0; i < managers.Length; i++)
                managers[i].InitializeManager();
    }

    /// <summary>
    /// Invokes the LateInitializeManager() method on an array of IManager
    /// </summary>
    public static void LateInitializeManagers(IManager[] managers)
    {
        if (managers.Exists())
            for (int i = 0; i < managers.Length; i++)
                managers[i].LateInitializeManager();
    }

    /// <summary>
    /// Loops through an array of BaseManagerUpdateables and calls UpdateManager() on each
    /// </summary>
    public static void UpdateManagers(BaseManagerUpdatable[] updateableManagers)
    {
        if (updateableManagers.Exists())
            for (int i = 0; i < updateableManagers.Length; i++)
                updateableManagers[i].UpdateManager();
    }



    /// <summary>
    /// Invokes the Initialize() method on an Array of BaseComponents
    /// </summary>
    public static void InitializeBaseComponents(BaseComponent[] baseComponents)
    {
        if (baseComponents.Exists())
            for (int i = 0; i < baseComponents.Length; i++)
                baseComponents[i].Initialize();
    }

    /// <summary>
    /// Invokes the Initialize() method on a List of BaseComponents
    /// </summary>
    public static void InitializeBaseComponents(List<BaseComponent> baseComponents)
    {
        if (baseComponents.Exists())
            for (int i = 0; i < baseComponents.Count; i++)
                baseComponents[i].Initialize();
    }



    /// <summary>
    /// Invokes the LateInitialize() method on an Array of BaseComponents
    /// </summary>
    public static void LateInitializeBaseComponents(BaseComponent[] baseComponents)
    {
        if (baseComponents.Exists())
            for (int i = 0; i < baseComponents.Length; i++)
                baseComponents[i].LateInitialize();
    }

    /// <summary>
    /// Invokes the LateInitialize() method on a List of BaseComponents
    /// </summary>
    public static void LateInitializeBaseComponents(List<BaseComponent> baseComponents)
    {
        if (baseComponents.Exists())
            for (int i = 0; i < baseComponents.Count; i++)
                baseComponents[i].LateInitialize();
    }


    /// <summary>
    /// Returns a list of all childrens Transforms of parent transform
    /// </summary>
    public static List<Transform> GetChildren(this Transform parent)
    {
        List<Transform> childrenList = new List<Transform>();

        for (int i = 0; i < parent.childCount; i++)
            childrenList.Add(parent.GetChild(i));

        return childrenList;
    }


    /// <summary>
    /// Returns a list of all childrens GameObjects of parent transform
    /// </summary>
    public static List<GameObject> GetChildrenObjects(this Transform parent)
    {
        List<GameObject> childrenList = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
            childrenList.Add(parent.GetChild(i).gameObject);

        return childrenList;
    }


    /// <summary>
    /// Returns a List of components found ONLY under the parent transform. Doesn't search bellow the Transform parent 
    /// </summary>
    public static List<T> GetListComponentsInChildren<T>(Transform parent)
    {
        if (parent != null && parent.childCount > 0)
        {
            // list to store all detected components
            List<T> componentList = new List<T>();

            // loop to find components
            for (int i = 0; i < parent.childCount; i++)
            {
                T component = parent.GetChild(i).GetComponent<T>();

                if (component == null || component.Equals(null)) // compare for UNITY & compile
                    continue;

                if (componentList.Contains(component) == false)
                    componentList.Add(component);
            }

            // return array of found components
            return componentList;
        }

        else
        {
            Debug.Log("Parent has no children");
            return null;
        }
    }

    /// <summary>
    /// Returns an Array of components found ONLY under the parent transform. Doesn't search bellow the Transform parent 
    /// </summary>
    public static T[] GetArrayComponentsInChildren<T>(Transform parent) => GetListComponentsInChildren<T>(parent).ToArray();


    public static Transform CreateEmpty(string name, Vector3 position, Transform parent)
    {
        Transform point = new GameObject(name).transform;
        point.position = position;
        point.parent = parent;
        return point;
    }



    public static void SetGameObjectsActive(GameObject[] gameObjects, bool value)
    {
        for (int i = 0; i < gameObjects.Length; i++)
            gameObjects[i].SetActive(value);
    }


    public static void SetGameObjectsActive(List<GameObject> gameObjects, bool value)
    {
        for (int i = 0; i < gameObjects.Count; i++)
            gameObjects[i].SetActive(value);
    }



    /// <summary>
    /// 0 - False, 1 - True
    /// </summary>
    public static bool ConvertIntToBool(int id)
    {
        if (id == 0)
            return false;
        else
            return true;
    }

    /// <summary>
    /// 0 - False, 1 - True
    /// </summary>
    public static int ConvertBoolToInt(bool value)
    {
        if (value == false)
            return 0;
        else
            return 1;
    }

    /// <summary>
    /// Set a single Width to line
    /// </summary>
    public static void SetLineWidth(this LineRenderer line, float width)
    {
        line.startWidth = width;
        line.endWidth = width;
    }


    /// <summary>
    /// Sets line point count and points from an array
    /// </summary>
    public static void SetLinePoints(this LineRenderer line, Vector3[] points)
    {
        line.positionCount = points.Length;
        line.SetPositions(points);
    }


    /// <summary>
    /// Sets line point count and points from an array
    /// </summary>
    public static void SetLinePointsWithHeightOffset(this LineRenderer line, Vector3[] points, float heightOffset)
    {
        line.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
            points[i].y = heightOffset;

        line.SetPositions(points);
    }

    /// <summary>
    /// Sets line point count and points from a list
    /// </summary>
    public static void SetLinePoints(this LineRenderer line, List<Vector3> points)
    {
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }

    /// <summary>
    /// Sets 2 points in a line
    /// </summary>
    public static void SetTwoPointLine(this LineRenderer line, Vector3 startPoint, Vector3 endPoint)
    {
        line.positionCount = 2;
        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);
    }

    /// <summary>
    /// Sets a color to Line start and end
    /// </summary>
    public static void SetLineColor(this LineRenderer line, Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }

    /// <summary>
    /// Removes all points from each Trail Renderer
    /// </summary>
    public static void ClearTrails(TrailRenderer[] trailRenderers)
    {
        if (trailRenderers.Exists() == false)
            return;

        for (int i = 0; i < trailRenderers.Length; i++)
            trailRenderers[i].Clear();
    }

    /// <summary>
    /// Removes all particles from each Particle System
    /// </summary>
    public static void ClearParticles(ParticleSystem[] particleSystems)
    {
        if (particleSystems.Exists() == false)
            return;

        for (int i = 0; i < particleSystems.Length; i++)
            particleSystems[i].Clear();
    }

    /// <summary>
    /// Starts the particles for each Particle System
    /// </summary>
    public static void PlayParticles(ParticleSystem[] particleSystems)
    {
        if (particleSystems.Exists() == false)
            return;

        for (int i = 0; i < particleSystems.Length; i++)
            particleSystems[i].Play();
    }

    /// <summary>
    /// Stops the particles for each Particle System
    /// </summary>
    public static void StopParticles(ParticleSystem[] particleSystems)
    {
        if (particleSystems.Exists() == false)
            return;

        for (int i = 0; i < particleSystems.Length; i++)
            particleSystems[i].Stop();
    }



    /// <summary>
    /// Enabled each Trail Renderer
    /// </summary>
    public static void PlayTrails(TrailRenderer[] trailRenderers)
    {
        if (trailRenderers.Exists() == false)
            return;

        for (int i = 0; i < trailRenderers.Length; i++)
            trailRenderers[i].enabled = true;
    }

    /// <summary>
    /// Disables each Trail Renderer
    /// </summary>
    public static void StopTrails(TrailRenderer[] trailRenderers)
    {
        if (trailRenderers.Exists() == false)
            return;

        for (int i = 0; i < trailRenderers.Length; i++)
            trailRenderers[i].enabled = false;
    }


    public static string FirstLetterUppercase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        str.ToLower();
        char[] characters = str.ToCharArray();
        characters[0] = char.ToUpper(characters[0]);

        return new string(characters);
    }

    public static Vector3 ViewportRaycast(LayerMask layerMask)
    {
        if (camera == null)
            camera = Camera.main;

        Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000, layerMask);

        return hit.point;
    }

    public static List<Collider> HorizontalEightDirectionRaycast(Vector3 origin, float rayLength, LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = origin;

        List<Collider> colliders = new List<Collider>();

        // loop through directions
        // start at top and go clockwise
        for (int i = 0; i < 8; i++)
        {
            if (i == 0) // top
                ray.direction = Vector3.forward;

            else if (i == 1) // top Right
                ray.direction = Vector3.forward + Vector3.right;

            else if (i == 2) // right
                ray.direction = Vector3.right;

            else if (i == 3) // bottom Right
                ray.direction = Vector3.back + Vector3.right;

            else if (i == 4) // bottom
                ray.direction = Vector3.back;

            else if (i == 5) // bottom Left
                ray.direction = Vector3.back + Vector3.left;

            else if (i == 6) // left
                ray.direction = Vector3.left;

            else if (i == 7) // top Left
                ray.direction = Vector3.forward + Vector3.left;

            ray.direction *= rayLength;

            if (Physics.Raycast(ray, out hit, mask) && hit.collider != null)
                colliders.Add(hit.collider);
        }

        return colliders;
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    /// <summary>
    /// Puts the string into the Clipboard.
    /// </summary>
    public static void CopyStringToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }

    /// <summary>
    /// Creates new file if there isn't one or adds contents to an existing one, and Returns its path
    /// File name ex: 'logs.txt' (extensions can be whatever ex: .bnb, .cro, etc,.)
    /// </summary>
    public static string CreateOrAddTextToFile(string path, string fileName, string content, int newLineAmount = 0)
    {
        // path to file
        string filePath = path + fileName;

        // add new lines
        if (newLineAmount > 0)
            for (int i = 0; i < newLineAmount; i++)
                content += "\n";

        // create file it if doesnt exist
        if (File.Exists(filePath) == false)
            File.WriteAllText(filePath, content);

        // add content to file
        else
            File.AppendAllText(filePath, content);

        return filePath;
    }

    /// <summary>
    /// Creates new file if there isn't one or adds contents to an existing one, and Returns its path
    /// File name ex: 'logs.txt' (extensions can be whatever ex: .bnb, .cro, etc,.)
    /// </summary>
    public static string CreateOrAddTextToFile(string path, string content)
    {
        // create file it if doesnt exist
        if (File.Exists(path) == false)
            File.WriteAllText(path, content);

        // add content to file
        else
            File.AppendAllText(path, content);

        return path;
    }

    public static void SetImageArrayColor(UnityEngine.UI.Image[] images, Color color)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].color = color;
    }

    public static void SetImageArrayAlpha(UnityEngine.UI.Image[] images, float alpha)
    {
        Color color = Color.black;

        for (int i = 0; i < images.Length; i++)
        {
            color = images[i].color;
            color.a = alpha;

            images[i].color = color;
        }
    }

    public static void SetImagePixelsPerUnit(UnityEngine.UI.Image[] images, float pixelsPerUnit)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].pixelsPerUnitMultiplier = pixelsPerUnit;
    }

    public static void SetTextMeshArrayColor(TMPro.TextMeshProUGUI[] textMeshes, Color color)
    {
        for (int i = 0; i < textMeshes.Length; i++)
            textMeshes[i].color = color;
    }

    public static bool SimpleWait(float waitTime)
    {
        float t = 0;
        while (t < waitTime)
            t += Time.deltaTime;

        return true;
    }

    #region Get Random

    public static T GetRandom<T>(T[] array) => array.GetRandom(); // kinda redundant, isnt it?

    public static T GetRandom<T>(T option1, T option2)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);

        return list.GetRandom();
    }

    public static T GetRandom<T>(T option1, T option2, T option3)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);
        list.Add(option3);

        return list.GetRandom();
    }

    public static T GetRandom<T>(T option1, T option2, T option3, T option4)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);
        list.Add(option3);
        list.Add(option4);

        return list.GetRandom();
    }

    public static T GetRandom<T>(T option1, T option2, T option3, T option4, T option5)
    {
        List<T> list = new List<T>();
        list.Add(option1);
        list.Add(option2);
        list.Add(option3);
        list.Add(option4);
        list.Add(option5);

        return list.GetRandom();
    }

    #endregion // Get Random

    #region Gizmos

    public static void DrawPoint(Vector3 position, Color color, float radius)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(position, radius);
    }

    public static void DrawLine(Vector3 origin, Vector3 target, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(origin, target);
    }

    public static void DrawRay(Vector3 origin, Vector3 direction, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(origin, direction);
    }

    public static void DrawCube(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= cubeTransform;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldGizmosMatrix;
    }

    public static void DrawArrow(Vector3 arrowPosition, Vector3 arrowDirection, float arrowLength = 1f, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20f)
    {
        Vector3 arrowEnd = arrowPosition + arrowDirection.normalized * arrowLength;

        // Draw arrow body
        Gizmos.DrawLine(arrowPosition, arrowEnd);

        // Draw arrow head
        Vector3 right = Quaternion.LookRotation(arrowDirection) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(arrowDirection) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.forward;

        Gizmos.DrawLine(arrowEnd, arrowEnd - (right * arrowHeadLength));
        Gizmos.DrawLine(arrowEnd, arrowEnd - (left * arrowHeadLength));
    }

    public static void DrawString(string text, Vector3 position, Color color)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;

        Handles.Label(position, text, style);
    }
    #endregion // gizmos


#if UNITY_EDITOR
    #region Instantiate Prefabs
    public static GameObject CreatePrefab(GameObject prefab)
        => PrefabUtility.InstantiatePrefab(prefab) as GameObject;

    public static GameObject CreatePrefab(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj;
    }

    public static T CreatePrefab<T>(GameObject prefab)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        return obj.GetComponent<T>();
    }

    public static T CreatePrefab<T>(Object prefab)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        return obj.GetComponent<T>();
    }

    /// <summary>
    /// Path to save ex: "Assets/Resources/SO" 
    /// </summary>
    /// <param name="className">Name of scriptable object class</param>
    /// <param name="path"> Path to save ex: "Assets/Resources/SO" </param>
    public static ScriptableObject CreateScriptableObject(string scriptableClassName, string path, string name)
    {
        ScriptableObject so = ScriptableObject.CreateInstance(scriptableClassName);
        so.name = name;

        AssetDatabase.CreateAsset(so, path);
        AssetDatabase.SaveAssets();
        Selection.activeObject = so;

        return so;
    }

    public static T CreatePrefab<T>(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj.GetComponent<T>();
    }
    #endregion // instantiate prefabs
#endif
}
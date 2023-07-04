
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enum Maker", menuName = "Stardrop / Tools / Enum Maker")]
public class EnumMakerSO : ScriptableObject
{
    [SerializeField] string enumName;
    [SerializeField] string[] enumsToCreate;
    [Space]
    [SerializeField] bool create;

    public void CreateEnums()
    {
        // Don't do anything if array is empty
        if (enumsToCreate.Exists() == false)
        {
            Debug.Log("Enum array is empty!");
            return;
        }

        // Convert to list and check for empty & duplicate values
        else
        {
            var enumList = UtilsArray.ToList(enumsToCreate);

            UtilsArray.RemoveEmpty(enumList);
            UtilsArray.RemoveDuplicates(enumList);

            enumsToCreate = enumList.ToArray();

            Debug.Log("Enums filtered");
        }

        // Create data path
        string path = Application.streamingAssetsPath + "/Enums/";
        Directory.CreateDirectory(path);

        // Create enum class file
        string enumClass = enumName + ".cs";
        string content =
            "\n" +
            "public enum " + enumName + "\n" +
            "{\n";

        // loop through array and add enums
        for (int i = 0; i < enumsToCreate.Length; i++)
            content += "    " + enumsToCreate[i] + ",\n";

        // close class
        content += "}";

        if (File.Exists(path + enumClass))
            File.Delete(path + enumClass);

        File.WriteAllText(path + enumClass, content);
        Debug.Log("Enums created! Minimize editor and reenter to view changes!");
    }

    private void OnValidate()
    {
        if (create)
        {
            CreateEnums();
            create = false;
        }
    }
}

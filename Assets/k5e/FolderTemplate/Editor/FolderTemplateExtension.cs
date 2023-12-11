#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace k5e.FolderTemplate.Editor
{
    public class FolderTemplateExtension
    {
        [MenuItem("Tools/FolderTemplate")]
        public static void OpenWindow()
        {
            var window = EditorWindow.GetWindow<FolderTemplateWindow>();
        }

        public static void CreateFolder(string projectName, string templatePath)
        {
            try
            {
                // Create Folders
                var folderJson = JSONManager.parse(templatePath);
                foreach (var folder in folderJson.folders)
                {
                    var path = $"Assets/{projectName}/{folder}";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                // Create .gitkeep
                if (folderJson.options.git)
                {
                    foreach (var folder in folderJson.folders)
                    {
                        var path = $"Assets/{projectName}/{folder}";
                        if (!Directory.EnumerateFileSystemEntries(path).Any())
                        {
                            File.Create($"{path}/.gitkeep");
                        }
                    }
                }
                
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}
#endif
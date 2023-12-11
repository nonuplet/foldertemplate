#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace k5e.FolderTemplate.Editor
{
    public class FolderTemplateWindow : EditorWindow
    {
        private string _projectName = "";

        private FolderJSON _userTemplate;
        private const string USER_TEMPLATE_PATH = "folder-structure.json";
        
        // Dropdown
        private GUIContent[] dropdownOptions;
        private int dropdownIndex;
        private string[] templates;

        private void OnEnable()
        {
            var scriptPathSplit = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this)).Split("/");
            var configPath = string.Join("/", scriptPathSplit.Take(scriptPathSplit.Length - 2)) + "/Configs";
            templates = Directory.GetFiles(configPath, "*.json").Select(path => path.Replace("\\", "/")).ToArray();

            dropdownOptions = new GUIContent[templates.Length];
            for (var i = 0; i < templates.Length; i++)
            {
                dropdownOptions[i] = new GUIContent(Path.GetFileNameWithoutExtension(templates[i]));
            }
            
            // Load "<Unity Project Root>/folder-structure.json"
            if (File.Exists(USER_TEMPLATE_PATH))
            {
                try
                {
                    _userTemplate = JSONManager.parse(USER_TEMPLATE_PATH);
                    if (_userTemplate.options.project_name != "")
                        _projectName = _userTemplate.options.project_name;
                    dropdownIndex = 0;
                    templates = new[] { USER_TEMPLATE_PATH };
                }
                catch (Exception e)
                {
                    Debug.LogError($"Parse Error: folder-structure.json\n{e}");
                    _userTemplate = null;
                }
            }

            titleContent = new GUIContent("Folder Template");
        }

        private void OnDisable()
        {
            _userTemplate = null;
            dropdownIndex = 0;
        }

        private void OnGUI()
        {
            var largeBold = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize = EditorStyles.largeLabel.fontSize
            };

            EditorGUILayout.Space(5f);
            if (_userTemplate != null)
            {
                EditorGUILayout.LabelField("Found folder-structure.json");
                EditorGUILayout.Space(7f);
            }

            // Project Name
            EditorGUILayout.LabelField("Project Name", largeBold);
            if (_userTemplate == null || _userTemplate.options.project_name == "")
            {
                // Input
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(7f);
                    _projectName = EditorGUILayout.TextField(_projectName);
                    GUILayout.Space(7f);
                }
            }
            else
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.Space(10f);
                    EditorGUILayout.LabelField(_projectName);
                }
            }
            EditorGUILayout.Space(15f);
            
            // Template
            if (_userTemplate == null)
            {
                EditorGUILayout.LabelField("FolderStructure Template", largeBold);
                EditorGUILayout.Popup(dropdownIndex, dropdownOptions);
                EditorGUILayout.Space(15f);
            }

            // Button
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.Space(1f);
                if (GUILayout.Button("Generate"))
                {
                    if (!string.IsNullOrEmpty(_projectName))
                    {
                        Close();
                        FolderTemplateExtension.CreateFolder(_projectName, templates[dropdownIndex]);
                    }
                }
            }
        }
    }
}
#endif
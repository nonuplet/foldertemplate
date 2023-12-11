#if UNITY_EDITOR
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace k5e.FolderTemplate.Editor
{
    [Serializable]
    public class FolderJSON
    {
        public JSONOptions options;
        public string[] folders;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"FolderJSON\n\n[Options]\n{options}\n[Directories]\n");
            foreach (var d in folders)
            {
                builder.Append($"{d}\n");
            }

            return builder.ToString();
        }
    }

    [Serializable]
    public class JSONOptions
    {
        public string project_name = "";
        public bool git = false;

        public override string ToString()
        {
            return $"project_name: {project_name}\ngit: {git}\n";
        }
    }

    public class JSONManager
    {
        public static FolderJSON parse(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<FolderJSON>(json);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            
            return null;
        }
    }
}
#endif
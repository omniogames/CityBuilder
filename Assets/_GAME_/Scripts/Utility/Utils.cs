using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Omnio.Utils
{
    public static class Utils
    {

        #region IO
        public static void SaveToDisk(string s, string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(s);
            sw.Close();
        }

        public static string LoadFromDisk(string path)
        {
            StreamReader sr = new StreamReader(path);
            string fileString = sr.ReadToEnd();
            sr.Close();
            return fileString;
        }
        #endregion

        public static bool IsPointerOverUIElement()
        {
            return EventSystem.current.currentSelectedGameObject != null;
        }

    }
}

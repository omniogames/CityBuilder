using System.Collections.Generic;
using UnityEngine;

public class Consts : MonoBehaviour
{
    
    public static readonly Dictionary<int, string> NumberAbbrs = new Dictionary<int, string>
    {
        {1000000000, "B" },
        {1000000, "M" },
        {1000,"K"}
    };

    public struct FileNames
    {
        public const string LEVELDATA = "level.dat";
    }

    public struct AnalyticsEventNames
    {
        public const string LEVEL_START = "Start";
        public const string LEVEL_SUCCESS = "Success";
        public const string LEVEL_FAIL = "Fail";
    }
    
    public struct PrefKeys
    {
        public const string HAPTIC = "Haptic";
    }
    
    public struct AnalyticsDataName
    {
        public const string LEVEL = "Level";
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EnemyType {
    None,
    Regular,
    Spear,
    HeavyArmor,
    Block,
    Boss,
    Save,
    Flag
}

public struct RoadSegmentInfo {
    private List<List<EnemyType>> enemyInfo;

    public void Init(string[][] data) {
        enemyInfo = new List<List<EnemyType>>();
        for (int i = 0; i < data.Length; i++) {
            enemyInfo.Insert(i, new List<EnemyType>());
            for (int j = 0; j < data[0].Length; j++) {
                var enemy = DataUtility.GetEnemyByString(data[j][i]);
                enemyInfo[i].Insert(j, enemy);
            }
        }
    }

    public List<EnemyType> GetEnemyTypesAt(int roadNumber) {
        return enemyInfo[roadNumber];
    }
}

public static partial class DataUtility {
    public const string LevelResourcesPath = "Levels/";

    private static string ReadLevelResource(int level) {
        return Resources.Load<TextAsset>(LevelResourcesPath + "Level" + level).text;
    }

    private static List<RoadSegmentInfo> ParseLevelInfo(string info) {
        var lines = info.
            Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).
            Where(l => !string.IsNullOrEmpty(l)).ToArray();
        var result = new List<RoadSegmentInfo>();
        for (int i = 0; i < lines.Length; i += 3) {

            var segmentResult = new string[][] {
                lines[i].Split(','),
                lines[i + 1].Split(','),
                lines[i + 2].Split(',')
            };
            
            var newSegmentInfo = new RoadSegmentInfo();
            newSegmentInfo.Init(segmentResult);
            result.Add(newSegmentInfo);
        }

        return result;
    }

    public static List<RoadSegmentInfo> GetLevelInfo(bool getLastLevel = false, bool includeTutorial = false) {
        if (getLastLevel) {
            return ParseLevelInfo(ReadLevelResource(6));
        }
        
        var result = "";
        if(includeTutorial) {
            result += ReadLevelResource(0) + "\n";
        } else {
            for (var i = 1; i < 6; i++) {
                result += ReadLevelResource(i) + "\n";
            }
        }
        
        return ParseLevelInfo(result);
    }

    public static EnemyType GetEnemyByString(string num) {
        return (EnemyType) int.Parse(num);
    }
}
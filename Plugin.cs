using BepInEx;
using HarmonyLib;
using MU3.Data;
using MU3.DataStudio;
using MU3.Game;
using MU3.Notes;
using MU3.SceneObject;
using System.Globalization;
using UnityEngine;

namespace SongLevel
{
    [BepInPlugin("com.yourname.songlevel", "Song Level Display", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static string DisplayText = "Waiting...";

        private void Awake()
        {
            Debug.Log("===== SongLevel: 插件已加载！ =====");
            Harmony.CreateAndPatchAll(typeof(LoadScorePatch));
            Harmony.CreateAndPatchAll(typeof(SetMusicDataPatch));
            Harmony.CreateAndPatchAll(typeof(SetDifficultyPatch));
            Debug.Log("===== 所有补丁已注册 =====");
        }

        void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(-200, Screen.height / 2 + 780, Screen.width, 240), DisplayText, style);
        }

        // 公共更新方法，供所有补丁调用
        public static void UpdateDisplay(MU3.Data.MusicData musicData, FumenDifficulty difficulty)
        {
            if (musicData == null) return;
            int levelIndex = (int)difficulty;
            if (levelIndex < 0 || levelIndex >= musicData.fumenData.Length) return;

            var fumenData = musicData.fumenData[levelIndex];
            float fumenConst = fumenData.fumenConst;

            string levelStr = fumenConst.ToString("0.0", CultureInfo.InvariantCulture);
            DisplayText = $"Song {musicData.id}\n{levelStr}";
            Debug.Log(DisplayText);
        }
    }

    // 补丁1：正式开始游戏时触发
    [HarmonyPatch(typeof(NotesManager), "loadScore")]
    public static class LoadScorePatch
    {
        static void Postfix(SessionInfo sessionInfo, bool isStageDazzling)
        {
            try
            {
                Plugin.UpdateDisplay(sessionInfo.musicData, sessionInfo.musicLevel);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"LoadScore 错误: {ex.Message}");
            }
        }
    }

    // 补丁2：预览时设置歌曲数据触发
    [HarmonyPatch(typeof(ANM_PLY_PlayMusic_00), "set_musicData")]
    public static class SetMusicDataPatch
    {
        static void Postfix(ANM_PLY_PlayMusic_00 __instance, MU3.Data.MusicData value)
        {
            try
            {
                if (__instance.difficulty == FumenDifficulty.Basic && value != null)
                {
                    return;
                }
                Plugin.UpdateDisplay(value, __instance.difficulty);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"SetMusicData 错误: {ex.Message}");
            }
        }
    }

    // 补丁3：预览时设置难度触发
    [HarmonyPatch(typeof(ANM_PLY_PlayMusic_00), "set_difficulty")]
    public static class SetDifficultyPatch
    {
        static void Postfix(ANM_PLY_PlayMusic_00 __instance, FumenDifficulty value)
        {
            try
            {
                if (__instance.musicData == null) return;
                Plugin.UpdateDisplay(__instance.musicData, value);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"SetDifficulty 错误: {ex.Message}");
            }
        }
    }
}

﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ServerNameLimiter {
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        internal static new ManualLogSource Logger;

        private void Awake() {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(LobbyDataEntry), nameof(LobbyDataEntry.SetLobbyData))]
        public static class LobbyDataEntry_SetLobbyData {
            public static void Prefix(ref LobbyDataEntry __instance) {
                __instance._lobbyName = __instance._lobbyName.Replace('\n', ' ');

                if(__instance._lobbyName.Length > 20) {
                    __instance._lobbyName = __instance._lobbyName.Substring(0, 20);
                }
            }
        }
    }
}

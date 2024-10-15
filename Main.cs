using Il2Cpp;
using MelonLoader;
using System;
using UnityEngine;

namespace cheat
{
    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            base.OnInitializeMelon();
            LoggerInstance.Msg("hello from pomoikahack!");
            MelonEvents.OnGUI.Subscribe(DrawPlayerESP, 100);
        }

        private void DrawPlayerESP()
        {
            if (ReferenceHub.AllHubs == null) return;
            foreach (var hub in ReferenceHub.AllHubs)
            {
                if (hub == ReferenceHub.LocalHub) continue;

                var playerCameraPos = hub.PlayerCameraReference.transform.position;
                var projection = Camera.main.WorldToScreenPoint(playerCameraPos);
                if (projection.z >= 0 && hub.nicknameSync != null && hub.roleManager != null && hub.roleManager._curRole != null)
                {
                    var nick = hub.nicknameSync._firstNickname;
                    var dist = Vector3.Distance(playerCameraPos, Camera.main.transform.position);
                    var roleName = hub.roleManager._curRole.RoleName;
                    var roleColor = hub.roleManager._curRole.RoleColor;

                    var style = new GUIStyle();
                    style.normal.textColor = roleColor;
                    GUI.Label(new Rect(projection.x, Screen.height - projection.y, 250f, 200f), $"{nick}\n{roleName}\n{(int)dist}", style);
                }
            }
        }
    }
}

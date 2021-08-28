using System.Numerics;
using ImGuiNET;
using SFML.System;

namespace TileGame.Game
{
    public static class Notifier
    {
        private static readonly Clock MessageTimer;
        private static string _levelToolTip = "";
        public static float MessageUpTime { get; set; } = 3f;

        static Notifier()
        {
            MessageTimer = new Clock();
        }

        public static void SetMessage(string message)
        {
            MessageTimer.Restart();
            MessageUpTime = 3f;
            _levelToolTip = message;
        }

        public static void SetMessage(string message, float displayLength)
        {
            MessageTimer.Restart();
            MessageUpTime = displayLength;
            _levelToolTip = message;
        }

        public static void Display()
        {
            if (MessageTimer.ElapsedTime.AsSeconds() > MessageUpTime) _levelToolTip = "";

            if (_levelToolTip != "")
            {
                ImGui.Begin("Notification",
                    ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoDocking |
                    ImGuiWindowFlags.Tooltip);
                ImGui.SetWindowFontScale(2.5f);
                ImGui.BeginTooltip();
                ImGui.TextColored(new Vector4(255, 0, 0, 255), _levelToolTip);
                ImGui.EndTooltip();
                ImGui.End();
            }
        }
    }
}
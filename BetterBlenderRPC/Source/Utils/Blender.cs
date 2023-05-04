using System.Diagnostics;

namespace BetterBlenderRPC.Source.Utils;

public static class Blender
{
    public static bool Running()
    {
        foreach (Process process in Process.GetProcesses())
            if (process.ProcessName.ToLower() == "blender")
                return true;

        return false;
    }

    public static string GetTitleName()
    {
        var name = string.Empty;
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.ToLower() == "blender")
                name = process.MainWindowTitle;
        }

        return name;
    }

    public static void UpdateBlender()
    {
        if (Running())
        {
            var name = GetTitleName();
            if (name.ToLower() == "blender render")
            {
                Discord.StateUpdate("In render.");
                return;
            }

            var arrayOfNames = name.Split('\\');
            var stateName = arrayOfNames[arrayOfNames.Length - 1];

            if (stateName.ToLower() == "blender")
                Discord.StateUpdate("Untitled Project");
            else
                Discord.StateUpdate(stateName.Remove(stateName.Length - 1));
        }
        else
            Discord.Clear();
    }
}
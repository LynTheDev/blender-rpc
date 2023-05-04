using DiscordRPC;

namespace BetterBlenderRPC.Source.Utils;

public static class Discord
{
    private static DiscordRpcClient client;
    private static string state;
    public static bool isOn = false;

    public static void Initialise()
    {
        client = new DiscordRpcClient(Secret.ClientID);

        client.Initialize();
        isOn = true;

        SetDefaultPresence();
    }

    public static void StateUpdate(string stateString)
    {
        client.UpdateState(stateString);
    }

    public static void Clear()
    {
        client.ClearPresence();
    }

    public static void SetDefaultPresence()
    {
        if (Blender.Running())
        {
            var name = Blender.GetTitleName();
            if (name == "Blender")
                state = "Untitled Project";
            else if (name.ToLower() == "blender render")
                state = "In render.";
            else
            {
                var arrayOfNames = name.Split('\\');
                var stateName = arrayOfNames[arrayOfNames.Length - 1];
                state = stateName.Remove(stateName.Length - 1);
            }
        }
        else
            state = "Not in a project.";

        client.SetPresence(new RichPresence()
        {
            Details = "Modeling..",
            State = state,
            Assets = new Assets()
            {
                LargeImageKey = "blender_logo",
                LargeImageText = "Blender RPC (Non Official)",
            }
        });
    }

    public static void Kill()
    {
        isOn = false;
        client.Dispose();
    }
}
using BetterBlenderRPC.Source.Utils;

namespace BetterBlenderRPC.Source.Handlers;

public static class Handler
{
    public static void OnExit(object sender, EventArgs e)
    {
        RPC.notify.Visible = false;

        Discord.Kill();
        Application.Exit();
    }

    public static void stop_Click(object sender, EventArgs e)
    {
        Discord.Clear();

        RPC.stop.Enabled = false;
        RPC.start.Enabled = true;

        RPC.PerformCheck.Stop();
    }

    public static void start_Click(object sender, EventArgs e)
    {
        Discord.SetDefaultPresence();

        RPC.stop.Enabled = true;
        RPC.start.Enabled = false;

        RPC.PerformCheck.Start();
    }

    public static void PerformCheck_Tick(object sender, EventArgs e)
    {
        if (Discord.ClientExists())
            Blender.UpdateBlender();
    }
}
using BetterBlenderRPC.Source.Utils;
using BetterBlenderRPC.Source.Handlers;

using Timer = System.Windows.Forms.Timer;

namespace BetterBlenderRPC.Source;

public static class RPC
{
    public static ToolStripMenuItem stop;
    public static ToolStripMenuItem start;

    public static NotifyIcon notify;

    public static Timer PerformCheck;

    public static void Main(string[] args)
    {
        Discord.Initialise();

        // Creating Tray
        notify = new NotifyIcon();
        notify.Icon = new Icon("Assets/Blender_Logo.ico");
        notify.Visible = true;
        notify.Text = "Blender RPC";

        // Setting up timer
        PerformCheck = new Timer()
        {
            Interval = 15000,
        };

        PerformCheck.Tick += Handler.PerformCheck_Tick;
        PerformCheck.Start();

        // Creating ContextStrip
        var strip = new ContextMenuStrip()
        {
            BackColor = Color.Black,
            ForeColor = Color.White,
            ShowImageMargin = false
        };

        // Creating Separate items
        stop = new ToolStripMenuItem()
        {
            Name = "stop",
            Text = "Stop RPC",
            Enabled = true
        };

        stop.Click += Handler.stop_Click;

        start = new ToolStripMenuItem()
        {
            Name = "start",
            Text = "Start RPC",
            Enabled = false
        };

        start.Click += Handler.start_Click;

        var exit = new ToolStripMenuItem()
        {
            Name = "exit",
            Text = "Exit"
        };

        exit.Click += Handler.OnExit;

        // ..& Adding said items to it.
        strip.Items.Add(exit);
        strip.Items.Add(new ToolStripSeparator());
        strip.Items.Add(stop);
        strip.Items.Add(start);

        // Adding strip to notifyIcon
        notify.ContextMenuStrip = strip;

        // Finally run the thing
        Application.Run();
    }
}
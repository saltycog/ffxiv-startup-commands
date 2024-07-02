namespace FfxivStartupCommands;

using Dalamud.Game;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;


public class Services
{
    [PluginService]
    public static IDalamudPluginInterface PluginInterface { get; private set; }

    [PluginService]
    public static ICommandManager CommandManager { get; private set; }

    [PluginService]
    public static IClientState ClientState { get; private set; }

    [PluginService]
    public static IChatGui ChatGui { get; private set; }

    [PluginService]
    public static IGameGui GameGui { get; private set; }

    [PluginService]
    public static ISigScanner TargetModuleScanner { get; private set; }


    public static void Initialize(IDalamudPluginInterface dalamudPluginInterface)
    {
        dalamudPluginInterface.Create<Services>();
    }
}
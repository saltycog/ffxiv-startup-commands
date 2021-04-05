namespace FfxivStartupCommands
{
    public enum ChatChannel
    {
        None,
        Say,
        Yell,
        Shout,
        Tell,
        Reply,
        Party,
        Alliance,
        FreeCompany,
        Linkshell1,
        Linkshell2,
        Linkshell3,
        Linkshell4,
        Linkshell5,
        Linkshell6,
        Linkshell7,
        Linkshell8,
        CrossworldLinkshell1,
        CrossworldLinkshell2,
        CrossworldLinkshell3,
        CrossworldLinkshell4,
        CrossworldLinkshell5,
        CrossworldLinkshell6,
        CrossworldLinkshell7,
        CrossworldLinkshell8,
        NoviceNetwork,
        PvPTeam,
        Echo
    }


    public static class ChatChannelExtensions
    {
        public static string ToCommand(this ChatChannel chatChannel)
        {
            switch (chatChannel)
            {
                case ChatChannel.Say:
                    return "/say";
                case ChatChannel.Yell:
                    return "/yell";
                case ChatChannel.Shout:
                    return "/shout";
                case ChatChannel.Tell:
                    return "/tell";
                case ChatChannel.Reply:
                    return "/reply";
                case ChatChannel.Party:
                    return "/party";
                case ChatChannel.Alliance:
                    return "/alliance";
                case ChatChannel.FreeCompany:
                    return "/freecompany";
                case ChatChannel.Linkshell1:
                    return "/linkshell1";
                case ChatChannel.Linkshell2:
                    return "/linkshell2";
                case ChatChannel.Linkshell3:
                    return "/linkshell3";
                case ChatChannel.Linkshell4:
                    return "/linkshell4";
                case ChatChannel.Linkshell5:
                    return "/linkshell5";
                case ChatChannel.Linkshell6:
                    return "/linkshell6";
                case ChatChannel.Linkshell7:
                    return "/linkshell7";
                case ChatChannel.Linkshell8:
                    return "/linkshell8";
                case ChatChannel.CrossworldLinkshell1:
                    return "/cwlinkshell1";
                case ChatChannel.CrossworldLinkshell2:
                    return "/cwlinkshell2";
                case ChatChannel.CrossworldLinkshell3:
                    return "/cwlinkshell3";
                case ChatChannel.CrossworldLinkshell4:
                    return "/cwlinkshell4";
                case ChatChannel.CrossworldLinkshell5:
                    return "/cwlinkshell5";
                case ChatChannel.CrossworldLinkshell6:
                    return "/cwlinkshell6";
                case ChatChannel.CrossworldLinkshell7:
                    return "/cwlinkshell7";
                case ChatChannel.CrossworldLinkshell8:
                    return "/cwlinkshell8";
                case ChatChannel.NoviceNetwork:
                    return "/novice";
                case ChatChannel.PvPTeam:
                    return "/pvpteam";
                case ChatChannel.Echo:
                    return "/echo";
            }
            
            return string.Empty;
        }
        
        public static string ToName(this ChatChannel chatChannel)
        {
            switch (chatChannel)
            {
                case ChatChannel.Say:
                    return "Say";
                case ChatChannel.Yell:
                    return "Yell";
                case ChatChannel.Shout:
                    return "Shout";
                case ChatChannel.Tell:
                    return "Tell";
                case ChatChannel.Reply:
                    return "Reply";
                case ChatChannel.Party:
                    return "Party";
                case ChatChannel.Alliance:
                    return "Alliance";
                case ChatChannel.FreeCompany:
                    return "Free Company";
                case ChatChannel.Linkshell1:
                    return "Linkshell 1";
                case ChatChannel.Linkshell2:
                    return "Linkshell 2";
                case ChatChannel.Linkshell3:
                    return "Linkshell 3";
                case ChatChannel.Linkshell4:
                    return "Linkshell 4";
                case ChatChannel.Linkshell5:
                    return "Linkshell 5";
                case ChatChannel.Linkshell6:
                    return "Linkshell 6";
                case ChatChannel.Linkshell7:
                    return "Linkshell 7";
                case ChatChannel.Linkshell8:
                    return "Linkshell 8";
                case ChatChannel.CrossworldLinkshell1:
                    return "Crossworld Linkshell 1";
                case ChatChannel.CrossworldLinkshell2:
                    return "Crossworld Linkshell 2";
                case ChatChannel.CrossworldLinkshell3:
                    return "Crossworld Linkshell 3";
                case ChatChannel.CrossworldLinkshell4:
                    return "Crossworld Linkshell 4";
                case ChatChannel.CrossworldLinkshell5:
                    return "Crossworld Linkshell 5";
                case ChatChannel.CrossworldLinkshell6:
                    return "Crossworld Linkshell 6";
                case ChatChannel.CrossworldLinkshell7:
                    return "Crossworld Linkshell 7";
                case ChatChannel.CrossworldLinkshell8:
                    return "Crossworld Linkshell 8";
                case ChatChannel.NoviceNetwork:
                    return "Novice Network";
                case ChatChannel.PvPTeam:
                    return "PvP Team";
                case ChatChannel.Echo:
                    return "Echo";
            }

            return "None";
        }
    }
}
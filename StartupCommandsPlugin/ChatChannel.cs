namespace FfxivStartupCommands
{
    public enum ChatChannel
    {
        None = 0,
        Say = 1,
        Yell = 2,
        Shout = 3,
        // Tell = 4,
        // Reply = 5,
        Party = 6,
        Alliance = 7,
        FreeCompany = 8,
        Linkshell1 = 9,
        Linkshell2 = 10,
        Linkshell3 = 11,
        Linkshell4 = 12,
        Linkshell5 = 13,
        Linkshell6 = 14,
        Linkshell7 = 15,
        Linkshell8 = 16,
        CrossworldLinkshell1 = 17,
        CrossworldLinkshell2 = 18,
        CrossworldLinkshell3 = 19,
        CrossworldLinkshell4 = 20,
        CrossworldLinkshell5 = 21,
        CrossworldLinkshell6 = 22,
        CrossworldLinkshell7 = 23,
        CrossworldLinkshell8 = 24,
        NoviceNetwork = 25,
        PvPTeam = 26
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
            }

            return "None";
        }
    }
}
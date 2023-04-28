using DIKUArcade.Utilities;

namespace Breakout;

/// <summary> Class for conatining constants </summary>
/// <remarks> Used for indicating path to assets </remarks>
public static class Constants {
    /// <summary> This is to get the path of the Breakout/Assets </summary>
    /// <remarks> We need this for testing, to make sure we are using the same assets </remarks>
    public static readonly string MAIN_PATH = 
        Path.Combine(Directory.GetParent(FileIO.GetProjectPath())!.FullName,"Breakout");
}
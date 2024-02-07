#if MACOS || TARGET_ALL
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace GnomeStack.Sys;

public static partial class Os
{
    [SupportedOSPlatform("macos")]
    private static OsReleaseInfo GetMacOsReleaseInfo()
    {
        var os = new OsReleaseInfo();
        os.VersionCodename = GetMacOsCodeName();
        os.Id = "macos";
        os.Name = "macOS";
        os.IdLike = "darwin";
        os.VersionId = $"${Environment.Version.Major}.{Environment.Version.Minor}";
        os.VersionLabel = $"{os.VersionId} ${os.VersionCodename}";
        os.PrettyName = $"{os.Name} {os.VersionLabel} ({os.VersionCodename})";
        os.HomeUrl = "https://www.apple.com/macos";
        SetDarwinUrls(os);
        return os;
    }

    private static void SetDarwinUrls(OsReleaseInfo os)
    {
        os.DocumentationUrl = "https://support.apple.com";
        os.SupportUrl = "https://support.apple.com";
        os.BugReportUrl = "https://support.apple.com";
        os.PrivacyPolicyUrl = "https://www.apple.com/legal/privacy";
    }

    private static string GetMacOsCodeName()
    {
        var v = Environment.OSVersion.Version;
        switch (v.Major)
        {
            case 14:
                return "Sonoma";
            case 13:
                return "Ventura";
            case 12:
                return "Monterey";
            case 11:
                return "Big Sur";
            case 10:
                switch (v.Minor)
                {
                    case 15:
                        return "Catalina";
                    case 14:
                        return "Mojave";
                    case 13:
                        return "High Sierra";
                    case 12:
                        return "Sierra";
                    case 11:
                        return "El Capitan";
                    case 10:
                        return "Yosemite";
                    case 9:
                        return "Mavericks";
                    case 8:
                        return "Mountain Lion";
                    case 7:
                        return "Lion";
                    case 6:
                        return "Snow Leopard";
                    case 5:
                        return "Leopard";
                    case 4:
                        return "Tiger";
                    case 3:
                        return "Panther";
                    case 2:
                        return "Jaguar";
                    case 1:
                        return "Puma";
                    case 0:
                        return "Cheetah";
                    default:
                        return "Unknown";
                }

            default:
                return "Unknown";
        }
    }
}
#endif
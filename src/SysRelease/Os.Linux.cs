#if LINUX || TARGET_ALL
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace GnomeStack.Sys;

public static partial class Os
{
    [SupportedOSPlatform("linux")]
    private static OsReleaseInfo GetLinuxOsRelease()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            throw new PlatformNotSupportedException("Unknown Linux platform");
        }

        var result = Os.GetOsReleaseInfo("/etc/os-release");
        if (result is not null)
            return result;

        var os = new OsReleaseInfo
        {
            Id = "linux",
            Name = "GNU/Linux",
            VersionLabel = Environment.OSVersion.VersionString,
            VersionId = Environment.OSVersion.Version.ToString(),
        };

        var desc = RuntimeInformation.OSDescription;
        var parts = desc.Split(" ");
        if (parts.Length is > 0 and > 2 && parts[2].StartsWith("#"))
        {
            var seg = parts[2];
            var index = seg.IndexOf('~');
            if (index > -1)
                seg = seg.Substring(index + 1);
            index = seg.IndexOf('-');
            if (index > -1)
            {
                os.Name = seg.Substring(index + 1);
                os.Id = os.Name.ToLower();
                os.VersionLabel = seg.Substring(0, index);
            }
        }

        os.PrettyName = $"{os.Name} {os.VersionLabel}";
        return os;
    }
}

#endif
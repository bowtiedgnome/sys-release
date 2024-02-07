using System.Runtime.InteropServices;

namespace GnomeStack.Sys;

public static partial class Os
{
    public static OsReleaseInfo GetOsReleaseInfo()
    {
#if WINDOWS || TARGET_ALL
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var programData = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");
            if (!string.IsNullOrEmpty(programData))
            {
                var file = Path.Join(programData, "sys", "etc", "os-release");
                var res = GetOsReleaseInfo(file);
                if (res is not null)
                    return res;
            }

            return Os.GetWindowsOsVersion();
        }
#endif

#if MACOS || TARGET_ALL
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var res = GetOsReleaseInfo("/etc/os-release");
            if (res is not null)
                return res;

            return GetMacOsReleaseInfo();
        }
#endif

#if LINUX || TARGET_ALL
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return GetLinuxOsRelease();
        }
#endif

        if (File.Exists("/etc/os-release"))
        {
            var result = GetOsReleaseInfo("/etc/os-release");
            if (result is not null)
                return result;
        }

        return new OsReleaseInfo();
    }

    public static OsReleaseInfo? GetOsReleaseInfo(string file)
    {
        if (!System.IO.File.Exists(file))
            return null!;

        var os = new OsReleaseInfo();
        var lines = File.ReadAllLines("/etc/os-release");
        foreach (var line in lines)
        {
            Span<char> span = stackalloc char[line.Length];
            line.AsSpan().CopyTo(span);
            var keySpan = span[..span.IndexOf('=')];
            var valueSpan = span[(span.IndexOf('=') + 1)..];
            if (valueSpan.Length > 2)
            {
                if (valueSpan[0] is '\"' or '\'')
                    valueSpan = valueSpan.Slice(1);
                if (valueSpan[^1] is '\"' or '\'')
                    valueSpan = valueSpan[..^1];
            }

            var key = keySpan.ToString();
            var value = valueSpan.ToString();
            switch (key)
            {
                case "ID":
                    os.Id = value;
                    break;
                case "ID_LIKE":
                    os.IdLike = value;
                    break;
                case "NAME":
                    os.Name = value;
                    break;

                case "VERSION":
                    os.VersionLabel = value;
                    break;

                case "VERSION_ID":
                    os.VersionId = value;
                    break;

                case "VERSION_CODENAME":
                    os.VersionCodename = value;
                    break;

                case "PRETTY_NAME":
                    os.PrettyName = value;
                    break;

                case "ANSI_COLOR":
                    os.AnsiColor = value;
                    break;

                case "CPE_NAME":
                    os.CpeName = value;
                    break;

                case "HOME_URL":
                    os.HomeUrl = value;
                    break;

                case "DOCUMENTATION_URL":
                    os.DocumentationUrl = value;
                    break;

                case "SUPPORT_URL":
                    os.SupportUrl = value;
                    break;

                case "BUG_REPORT_URL":
                    os.BugReportUrl = value;
                    break;

                case "PRIVACY_POLICY_URL":
                    os.PrivacyPolicyUrl = value;
                    break;

                case "BUILD_ID":
                    os.BuildId = value;
                    break;

                case "VARIANT":
                    os.Variant = value;
                    break;

                case "VARIANT_ID":
                    os.VariantId = value;
                    break;

                default:
                    os[key] = value;
                    break;
            }
        }

        return os;
    }
}
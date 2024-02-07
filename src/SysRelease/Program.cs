// See https://aka.ms/new-console-template for more information
using GnomeStack.Sys;

var query = new Query();

var isSwitch = true;
var o = string.Empty;
foreach (var arg in args)
{
    if (!isSwitch)
    {
        query.Export = arg.Trim(new char[] { '"', '\'', ' ', '\t', '\n', '\r', '\v' });
        isSwitch = true;
        continue;
    }

    if (arg.StartsWith("--"))
    {
        switch (arg)
        {
            case "--id":
                query.Id = true;
                break;
            case "--all":
                query.All = true;
                break;
            case "--code-name":
                query.CodeName = true;
                break;
            case "--description":
                query.Description = true;
                break;
            case "--release":
                query.Release = true;
                break;
            case "--package-manager":
                query.PackageManager = true;
                break;
            case "--short":
                query.Short = true;
                break;
            case "--upper":
                query.Upper = true;
                break;
            case "--lower":
                query.Lower = true;
                break;
            case "--help":
                query.Help = true;
                break;
            case "--export":
                isSwitch = false;
                o = arg;
                break;
            default:
                Console.Error.WriteLine($"Unknown option: -{arg}");
                return 1;
        }

        continue;
    }

    if (arg.StartsWith("-"))
    {
        var slice = arg.AsSpan().Slice(1);
        foreach (var c in slice)
        {
            switch (c)
            {
                case 'i':
                    query.Id = true;
                    break;
                case 'a':
                    query.All = true;
                    break;
                case 'h':
                    query.Help = true;
                    break;
                case 'c':
                    query.CodeName = true;
                    break;
                case 'd':
                    query.Description = true;
                    break;
                case 'r':
                    query.Release = true;
                    break;
                case 'p':
                    query.PackageManager = true;
                    break;
                case 's':
                    query.Short = true;
                    break;
                case 'u':
                    query.Upper = true;
                    break;
                case 'l':
                    query.Lower = true;
                    break;
                case 'e':
                    isSwitch = false;
                    o = arg;
                    break;
                default:
                    Console.Error.WriteLine($"Unknown option: -{c}");
                    return 1;
            }
        }
    }
}

if (query.Help)
{
    Console.WriteLine("Usage: sys-release [options]");
    Console.WriteLine("Options:");
    Console.WriteLine("  -i --id               Show the ID of the OS.");
    Console.WriteLine("  -c --code-name        Show the code name of the OS.");
    Console.WriteLine("  -d --description      Show the description of the OS.");
    Console.WriteLine("  -r --release          Show the release of the OS.");
    Console.WriteLine("  -s --short            Removes the label(s) and displays the value(s).");
    Console.WriteLine("  -u --upper            Transforms the output to uppercase, unless exporting.");
    Console.WriteLine("  -l --lower            Transformer the output to lowercase, unless exporting.");
    Console.WriteLine("  -e --export <format>  Export the result in the specified format to standard out. Supported formats: json, dotenv");
    Console.WriteLine("  -h --help             Show this help message");
    return 50;
}

var os = Os.GetOsReleaseInfo();
if (query.Export is not null)
{
    if (query.Export == "json")
    {
        var json = $$"""
        {
            "id": "{{os.Id}}",
            "codeName": "{{os.VersionCodename}}",
            "description": "{{os.PrettyName}}",
            "release": "{{os.VersionLabel}}",
            "short": "{{os.VersionId}}",
            "buildId": "{{os.BuildId}}",
            "variant": "{{os.Variant}}",
            "variantId": "{{os.VariantId}}",
            "homeUrl": "{{os.HomeUrl}}",
            "documentationUrl": "{{os.DocumentationUrl}}",
            "supportUrl": "{{os.SupportUrl}}",
            "bugReportUrl": "{{os.BugReportUrl}}",
            "privacyPolicyUrl": "{{os.PrivacyPolicyUrl}}",
        }
        """;

        Console.WriteLine(json);
        return 0;
    }
    else if (query.Export == "dotenv" || query.Export == ".env")
    {
        foreach (var (key, value) in os)
        {
            Console.Write(key);
            Console.Write("=\"");
            Console.Write(value);
            Console.WriteLine('"');
        }

        return 0;
    }

    Console.Error.WriteLine($"Unknown export format: {query.Export}");
    return 1;
}

if (query.All)
{
    if (query.Short)
    {
        if (query.Upper)
        {
            Console.WriteLine(os.Name.ToUpper());
            Console.WriteLine(os.PrettyName.ToUpper());
            Console.WriteLine(os.VersionId.ToUpper());
            Console.WriteLine(os.VersionCodename.ToLower());
        }
        else if (query.Lower)
        {
            Console.WriteLine(os.Name.ToLower());
            Console.WriteLine(os.PrettyName.ToLower());
            Console.WriteLine(os.VersionId.ToLower());
            Console.WriteLine(os.VersionCodename.ToLower());
        }
        else
        {
            Console.WriteLine(os.Name);
            Console.WriteLine(os.PrettyName);
            Console.WriteLine(os.VersionId);
            Console.WriteLine(os.VersionCodename);
        }
    }
    else
    {
        if (query.Upper)
        {
            Console.Write("DISTRIBUTOR ID: ");
            Console.WriteLine(os.Name.ToUpper());
            Console.Write("DESCRIPTION:    ");
            Console.WriteLine(os.PrettyName.ToUpper());
            Console.Write("RELEASE:        ");
            Console.WriteLine(os.VersionId.ToUpper());
            Console.Write("CODENAME:       ");
            Console.WriteLine(os.VersionCodename.ToUpper());
        }
        else if (query.Lower)
        {
            Console.Write("distributor id: ");
            Console.WriteLine(os.Name.ToLower());
            Console.Write("description: ");
            Console.WriteLine(os.PrettyName.ToLower());
            Console.Write("release:        ");
            Console.WriteLine(os.VersionId.ToLower());
            Console.Write("codename:        ");
            Console.WriteLine(os.VersionCodename.ToLower());
        }
        else
        {
            Console.Write("Distributor ID: ");
            Console.WriteLine(os.Name);
            Console.Write("Description:    ");
            Console.WriteLine(os.PrettyName);
            Console.Write("Release:        ");
            Console.WriteLine(os.VersionId);
            Console.Write("Codename:       ");
            Console.WriteLine(os.VersionCodename);
        }
    }
}

if (query.Id)
{
    if (query.Short)
    {
        if (query.Upper)
        {
            Console.WriteLine(os.Name.ToUpper());
        }
        else if (query.Lower)
        {
            Console.WriteLine(os.Name.ToLower());
        }
        else
        {
            Console.WriteLine(os.Name);
        }
    }
    else
    {
        if (query.Upper)
        {
            Console.Write("DISTRIBUTOR ID: ");
            Console.WriteLine(os.Id.ToUpper());
        }
        else if (query.Lower)
        {
            Console.Write("distributor id: ");
            Console.WriteLine(os.Id.ToLower());
        }
        else
        {
            Console.Write("Distributor ID: ");
            Console.WriteLine(os.Id);
        }
    }
}

if (query.Description)
{
    if (query.Short)
    {
        if (query.Upper)
        {
            Console.WriteLine(os.PrettyName.ToUpper());
        }
        else if (query.Lower)
        {
            Console.WriteLine(os.PrettyName.ToLower());
        }
        else
        {
            Console.WriteLine(os.PrettyName);
        }
    }
    else
    {
        if (query.Upper)
        {
            Console.Write("DESCRIPTION: ");
            Console.WriteLine(os.PrettyName.ToUpper());
        }
        else if (query.Lower)
        {
            Console.Write("description: ");
            Console.WriteLine(os.PrettyName.ToLower());
        }
        else
        {
            Console.Write("Description: ");
            Console.WriteLine(os.PrettyName);
        }
    }
}

if (query.Release)
{
    if (query.Short)
    {
        if (query.Upper)
        {
            Console.WriteLine(os.VersionId.ToUpper());
        }
        else if (query.Lower)
        {
            Console.WriteLine(os.VersionId.ToLower());
        }
        else
        {
            Console.WriteLine(os.VersionId);
        }
    }
    else
    {
        if (query.Upper)
        {
            Console.Write("RELEASE: ");
            Console.WriteLine(os.VersionId.ToUpper());
        }
        else if (query.Lower)
        {
            Console.Write("release: ");
            Console.WriteLine(os.VersionId.ToLower());
        }
        else
        {
            Console.Write("Release: ");
            Console.WriteLine(os.VersionId);
        }
    }
}

if (query.CodeName)
{
    if (query.Short)
    {
        if (query.Upper)
        {
            Console.WriteLine(os.VersionCodename.ToUpper());
        }
        else if (query.Lower)
        {
            Console.WriteLine(os.VersionCodename.ToLower());
        }
        else
        {
            Console.WriteLine(os.VersionCodename);
        }
    }
    else
    {
        if (query.Upper)
        {
            Console.Write("CODENAME: ");
            Console.WriteLine(os.VersionCodename.ToUpper());
        }
        else if (query.Lower)
        {
            Console.Write("codename: ");
            Console.WriteLine(os.VersionCodename.ToLower());
        }
        else
        {
            Console.Write("Codename: ");
            Console.WriteLine(os.VersionCodename);
        }
    }
}

return 0;
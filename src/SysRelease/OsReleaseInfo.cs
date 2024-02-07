using System.Collections;

namespace GnomeStack.Sys;

public class OsReleaseInfo : IEnumerable<KeyValuePair<string, string>>
{
    private readonly Dictionary<string, string> props = new(StringComparer.OrdinalIgnoreCase);

    public string Id { get; internal set; } = string.Empty;

    public string IdLike { get; internal set; } = string.Empty;

    public string Name { get; internal set; } = string.Empty;

    public Version Version { get; internal set; } = new(0, 0);

    public string VersionLabel { get; internal set; } = string.Empty;

    public string VersionCodename { get; internal set; } = string.Empty;

    public string VersionId { get; internal set; } = string.Empty;

    public string PrettyName { get; set; } = string.Empty;

    public string AnsiColor { get; set; } = string.Empty;

    public string CpeName { get; set; } = string.Empty;

    public string HomeUrl { get; set; } = string.Empty;

    public string DocumentationUrl { get; set; } = string.Empty;

    public string SupportUrl { get; set; } = string.Empty;

    public string BugReportUrl { get; set; } = string.Empty;

    public string PrivacyPolicyUrl { get; set; } = string.Empty;

    public string BuildId { get; set; } = string.Empty;

    public string Variant { get; set; } = string.Empty;

    public string VariantId { get; set; } = string.Empty;

    public string? this[string key]
    {
        get => this.props.TryGetValue(key, out var value) ? value : null;
        set
        {
            if (value is null)
            {
                this.props.Remove(key);
            }
            else
            {
                this.props[key] = value;
            }
        }
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        if (!string.IsNullOrWhiteSpace(this.PrettyName))
            yield return new KeyValuePair<string, string>("VERSION", this.PrettyName);

        if (!string.IsNullOrWhiteSpace(this.Name))
            yield return new KeyValuePair<string, string>("NAME", this.Name);

        if (!string.IsNullOrWhiteSpace(this.VersionId))
            yield return new KeyValuePair<string, string>("VERSION_ID", this.VersionId);

        if (!string.IsNullOrWhiteSpace(this.VersionLabel))
            yield return new KeyValuePair<string, string>("VERSION", this.VersionLabel);

        if (!string.IsNullOrWhiteSpace(this.VersionCodename))
            yield return new KeyValuePair<string, string>("VERSION_CODENAME", this.VersionCodename);

        if (!string.IsNullOrWhiteSpace(this.Id))
            yield return new KeyValuePair<string, string>("ID", this.Id);

        if (!string.IsNullOrWhiteSpace(this.IdLike))
            yield return new KeyValuePair<string, string>("ID_LIKE", this.IdLike);

        if (!string.IsNullOrWhiteSpace(this.BuildId))
            yield return new KeyValuePair<string, string>("BUILD_ID", this.BuildId);

        if (!string.IsNullOrWhiteSpace(this.Variant))
            yield return new KeyValuePair<string, string>("VARIANT", this.Variant);

        if (!string.IsNullOrWhiteSpace(this.VariantId))
            yield return new KeyValuePair<string, string>("VARIANT_ID", this.VariantId);

        if (!string.IsNullOrWhiteSpace(this.HomeUrl))
            yield return new KeyValuePair<string, string>("HOME_URL", this.HomeUrl);

        if (!string.IsNullOrWhiteSpace(this.DocumentationUrl))
            yield return new KeyValuePair<string, string>("DOCUMENTATION_URL", this.DocumentationUrl);

        if (!string.IsNullOrWhiteSpace(this.SupportUrl))
            yield return new KeyValuePair<string, string>("SUPPORT_URL", this.SupportUrl);

        if (!string.IsNullOrWhiteSpace(this.BugReportUrl))
            yield return new KeyValuePair<string, string>("BUG_REPORT_URL", this.BugReportUrl);

        if (!string.IsNullOrWhiteSpace(this.PrivacyPolicyUrl))
            yield return new KeyValuePair<string, string>("PRIVACY_POLICY_URL", this.PrivacyPolicyUrl);

        foreach (var item in this.props)
        {
            if (!string.IsNullOrWhiteSpace(item.Value))
                yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}
namespace GnomeStack.Sys;

public class Query
{
    public bool CodeName { get; set; }

    public bool Id { get; set; }

    public bool Description { get; set; }

    public bool Release { get; set; }

    public bool PackageManager { get; set; }

    public bool Short { get; set; }

    public bool Upper { get; set; }

    public bool Lower { get; set; }

    public bool All { get; set; }

    public bool Help { get; set; }

    public string? Export { get; set; }
}
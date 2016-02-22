public class UIRegistryManager : BaseRegistryManager
{
    private static UIRegistryManager instance;

    private readonly string themeRegistrySubKeyName = "theme";

    private readonly string shouldOpenDropDownOnHoverRegistrySubKeyName = "shouldOpenDropDrownOnHover";

    private readonly string titlePromptDialogRegistrySubKeyName = "titlePromptDialog";

    public UIRegistryManager(string mainRegistrySubKey)
    {
        this.MainRegistrySubKey = mainRegistrySubKey;
    }

    public static UIRegistryManager Instance
    {
        get
        {
            if (instance == null)
            {
                string mainRegistrySubKey = ConfigurationManager.AppSettings["mainUIRegistrySubKey"];
                instance = new UIRegistryManager(mainRegistrySubKey);
            }
            return instance;
        }
    }

    public void WriteCurrentTheme(string theme)
    {
        this.Write(this.GenerateMergedKey(this.themeRegistrySubKeyName), theme);
    }

    public void WriteTitleTitlePromtDialog(string title)
    {
        this.Write(this.GenerateMergedKey(this.titlePromptDialogRegistrySubKeyName, this.titleTitlePromptDialogIsCanceledRegistrySubKeyName), title);
    }

    public bool ReadIsCheckboxDialogSubmitted()
    {
        return this.ReadBool(this.GenerateMergedKey(this.checkboxPromptDialogIsSubmittedRegistrySubKeyName));
    }

    public string ReadTheme()
    {
        return this.ReadStr(this.GenerateMergedKey(this.themeRegistrySubKeyName));
    }
}
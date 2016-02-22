using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using AAngelov.Utilities.UI.Core;
using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Presentation;

namespace AAngelov.Utilities.UI.ViewModels
{
    /// <summary>
    /// Contains methods and properties related to the Settings View
    /// </summary>
    public class BaseSettingsViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The selected accent color
        /// </summary>
        private Color selectedAccentColor;

        /// <summary>
        /// The themes collection
        /// </summary>
        private readonly LinkCollection themes = new LinkCollection();

        /// <summary>
        /// The selected theme
        /// </summary>
        private Link selectedTheme;

        /// <summary>
        /// The hover behavior drop down- can the drop downs open on hover
        /// </summary>
        private bool hoverBehaviorDropDown;

        /// <summary>
        /// The accent colors
        /// </summary>
        private readonly Color[] accentColors = new Color[]
        {
            // 9 accent colors from metro design principles
            Color.FromRgb(0x33, 0x99, 0xff), // blue
            Color.FromRgb(0x00, 0xab, 0xa9), // teal
            Color.FromRgb(0x33, 0x99, 0x33), // green
            Color.FromRgb(0x8c, 0xbf, 0x26), // lime
            Color.FromRgb(0xf0, 0x96, 0x09), // orange
            Color.FromRgb(0xff, 0x45, 0x00), // orange red
            Color.FromRgb(0xe5, 0x14, 0x00), // red
            Color.FromRgb(0xff, 0x00, 0x97), // magenta
            Color.FromRgb(0xa2, 0x00, 0xff), // purple      

            // 20 accent colors from Windows Phone 8
            Color.FromRgb(0xa4, 0xc4, 0x00), // lime
            Color.FromRgb(0x60, 0xa9, 0x17), // green
            Color.FromRgb(0x00, 0x8a, 0x00), // emerald
            Color.FromRgb(0x00, 0xab, 0xa9), // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2), // cyan
            Color.FromRgb(0x00, 0x50, 0xef), // cobal
            Color.FromRgb(0x6a, 0x00, 0xff), // indigo
            Color.FromRgb(0xaa, 0x00, 0xff), // violet
            Color.FromRgb(0xf4, 0x72, 0xd0), // pink
            Color.FromRgb(0xd8, 0x00, 0x73), // magenta
            Color.FromRgb(0xa2, 0x00, 0x25), // crimson
            Color.FromRgb(0xe5, 0x14, 0x00), // red
            Color.FromRgb(0xfa, 0x68, 0x00), // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a), // amber
            Color.FromRgb(0xe3, 0xc8, 0x00), // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c), // brown
            Color.FromRgb(0x6d, 0x87, 0x64), // olive
            Color.FromRgb(0x64, 0x76, 0x87), // steel
            Color.FromRgb(0x76, 0x60, 0x8a), // mauve
            Color.FromRgb(0x87, 0x79, 0x4e), // taupe
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public BaseSettingsViewModel()
        {
            // add the default themes
            this.themes.Add(new Link { DisplayName = "dark", Source = AppearanceManager.DarkThemeSource });
            this.themes.Add(new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource });

            // add additional themes
            this.themes.Add(new Link { DisplayName = "hello kitty", Source = new Uri("Assets/ModernUI.HelloKitty.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "love", Source = new Uri("Assets/ModernUI.Love.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "snowflakes", Source = new Uri("Assets/ModernUI.Snowflakes.xaml", UriKind.Relative) });

            this.SetPreviousHoverBehaviorDropDown();
            AppearanceManager.Current.PropertyChanged += this.OnAppearanceManagerPropertyChanged;
            this.SetPreviousAppereanceSettings();            
        }

        /// <summary>
        /// Gets or sets a value indicating whether [the drop downs should open on hover].
        /// </summary>
        /// <value>
        /// <c>true</c> if [hover behavior drop down]; otherwise, <c>false</c>.
        /// </value>
        public bool HoverBehaviorDropDown
        {
            get 
            { 
                return this.hoverBehaviorDropDown; 
            }

            set
            {
                if (this.hoverBehaviorDropDown != value)
                {
                    this.hoverBehaviorDropDown = value;
                    log.InfoFormat("Change HoverBehaviorDropDownto: {0}", this.hoverBehaviorDropDown);
                    UIRegistryManager.Instance.WriteDropDownBehavior(value);
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the themes.
        /// </summary>
        /// <value>
        /// The themes.
        /// </value>
        public LinkCollection Themes
        {
            get
            {
                return this.themes; 
            }
        }

        /// <summary>
        /// Gets the accent colors.
        /// </summary>
        /// <value>
        /// The accent colors.
        /// </value>
        public Color[] AccentColors
        {
            get 
            { 
                return this.accentColors;
            }
        }

        /// <summary>
        /// Gets or sets the selected theme.
        /// </summary>
        /// <value>
        /// The selected theme.
        /// </value>
        public Link SelectedTheme
        {
            get 
            { 
                return this.selectedTheme;
            }

            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    this.NotifyPropertyChanged();
                    log.InfoFormat("Change Selected Theme to: {0}", this.selectedTheme);
                    UIRegistryManager.Instance.WriteCurrentTheme(value.DisplayName);
                    
                    // and update the actual theme
                    AppearanceManager.Current.ThemeSource = value.Source;
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the selected accent.
        /// </summary>
        /// <value>
        /// The color of the selected accent.
        /// </value>
        public Color SelectedAccentColor
        {
            get 
            { 
                return this.selectedAccentColor; 
            }

            set
            {
                if (this.selectedAccentColor != value)
                {
                    this.selectedAccentColor = value;
                    this.NotifyPropertyChanged();
                    log.InfoFormat("Change Selected Color to: {0}", this.selectedAccentColor);
                    UIRegistryManager.Instance.WriteCurrentColors(value.R, value.G, value.B);
                    AppearanceManager.Current.AccentColor = value;
                }
            }
        }

        /// <summary>
        /// Sets the previous hover behavior drop down.
        /// </summary>
        private void SetPreviousHoverBehaviorDropDown()
        {
            this.HoverBehaviorDropDown = UIRegistryManager.Instance.GetDropDownBehavior();
        }

        /// <summary>
        /// Sets the previous appereance settings.
        /// </summary>
        private void SetPreviousAppereanceSettings()
        {
            string previouslySelectedTheme = UIRegistryManager.Instance.GetTheme();
            string[] colors = UIRegistryManager.Instance.GetColors();
            if (colors != null && colors.Length == 3 && previouslySelectedTheme != string.Empty)
            {
                Color currentColor = default(Color);
                currentColor = Color.FromRgb(byte.Parse(colors[0]), byte.Parse(colors[1]), byte.Parse(colors[2]));
                this.SyncThemeAndColor(previouslySelectedTheme, currentColor);
            }
            else
            {
                this.SyncThemeAndColor();
            }
        }

        /// <summary>
        /// Sync the color and the theme
        /// </summary>
        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = AppearanceManager.Current.AccentColor;
        }

        /// <summary>
        /// Sync the color and the theme
        /// </summary>
        /// <param name="themeName">Name of the theme.</param>
        /// <param name="currentColor">Color of the current.</param>
        private void SyncThemeAndColor(string themeName, Color currentColor)
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.DisplayName.Equals(themeName));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = currentColor;
        }

        /// <summary>
        /// Called when [appearance manager property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                this.SyncThemeAndColor();
            }
        }
    }
}
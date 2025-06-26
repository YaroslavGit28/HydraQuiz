using System.Drawing;
using System.Windows.Forms;

namespace QuizApp.WinForms.Theming
{
    public static class ThemeManager
    {
        public static ThemeColors LightTheme { get; }
        public static ThemeColors DarkTheme { get; }
        
        public static ThemeColors CurrentTheme { get; private set; }
        private static bool _isDarkTheme;

        static ThemeManager()
        {
            LightTheme = new ThemeColors
            {
                FormBackground = SystemColors.Control,
                TextColor = SystemColors.ControlText,
                ButtonBackground = SystemColors.Control,
                ButtonForeground = SystemColors.ControlText,
                TextBoxBackground = SystemColors.Window,
                PanelBackground = SystemColors.Control,
                DataGridViewBackground = SystemColors.ControlDark,
                DataGridViewCellBackground = SystemColors.Window,
                DataGridViewHeaderBackground = SystemColors.Control,
            };

            DarkTheme = new ThemeColors
            {
                FormBackground = Color.FromArgb(45, 45, 48),
                TextColor = Color.White,
                ButtonBackground = Color.FromArgb(63, 63, 70),
                ButtonForeground = Color.White,
                TextBoxBackground = Color.FromArgb(63, 63, 70),
                PanelBackground = Color.FromArgb(45, 45, 48),
                DataGridViewBackground = Color.FromArgb(45, 45, 48),
                DataGridViewCellBackground = Color.FromArgb(63, 63, 70),
                DataGridViewHeaderBackground = Color.FromArgb(80, 80, 80),
            };

            CurrentTheme = LightTheme; // Default theme
        }
        
        public static void ToggleTheme()
        {
            _isDarkTheme = !_isDarkTheme;
            CurrentTheme = _isDarkTheme ? DarkTheme : LightTheme;
        }

        public static void ApplyTheme(Form form)
        {
            form.BackColor = CurrentTheme.FormBackground;
            form.ForeColor = CurrentTheme.TextColor;
            ApplyThemeToControls(form.Controls);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Panel || control is GroupBox)
                {
                    control.BackColor = CurrentTheme.PanelBackground;
                    control.ForeColor = CurrentTheme.TextColor;
                }
                else if (control is Button button)
                {
                    button.BackColor = CurrentTheme.ButtonBackground;
                    button.ForeColor = CurrentTheme.ButtonForeground;
                    button.FlatStyle = _isDarkTheme ? FlatStyle.Flat : FlatStyle.Standard;
                }
                else if (control is Label || control is RadioButton || control is CheckBox)
                {
                    control.BackColor = Color.Transparent;
                    control.ForeColor = CurrentTheme.TextColor;
                }
                else if (control is TextBox || control is ComboBox)
                {
                    control.BackColor = CurrentTheme.TextBoxBackground;
                    control.ForeColor = CurrentTheme.TextColor;
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = CurrentTheme.DataGridViewBackground;
                    dgv.GridColor = CurrentTheme.TextColor;
                    
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = CurrentTheme.DataGridViewHeaderBackground;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = CurrentTheme.TextColor;
                    dgv.EnableHeadersVisualStyles = false;

                    dgv.DefaultCellStyle.BackColor = CurrentTheme.DataGridViewCellBackground;
                    dgv.DefaultCellStyle.ForeColor = CurrentTheme.TextColor;
                    dgv.DefaultCellStyle.SelectionBackColor = CurrentTheme.ButtonBackground;
                    dgv.DefaultCellStyle.SelectionForeColor = CurrentTheme.ButtonForeground;

                    dgv.AlternatingRowsDefaultCellStyle.BackColor = CurrentTheme.DataGridViewCellBackground;
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = CurrentTheme.TextColor;
                }

                if (control.Controls.Count > 0)
                {
                    ApplyThemeToControls(control.Controls);
                }
            }
        }
    }
} 
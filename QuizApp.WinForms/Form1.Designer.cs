namespace QuizApp.WinForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        // 
        // Start Panel
        // 
        _startPanel = new Panel();
        _startPanel.Dock = DockStyle.Fill;
        _startPanel.SuspendLayout();
        
        _playerNameLabel = new Label();
        _playerNameLabel.Text = "Имя игрока:";
        _playerNameLabel.Location = new System.Drawing.Point(200, 90);
        _playerNameLabel.Font = new System.Drawing.Font("Segoe UI", 11F);

        _playerNameTextBox = new TextBox();
        _playerNameTextBox.Location = new System.Drawing.Point(200, 115);
        _playerNameTextBox.Size = new System.Drawing.Size(400, 29);
        _playerNameTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);

        _groupLabel = new Label();
        _groupLabel.Text = "Группа:";
        _groupLabel.Location = new System.Drawing.Point(200, 160);
        _groupLabel.Font = new System.Drawing.Font("Segoe UI", 11F);

        _groupComboBox = new ComboBox();
        _groupComboBox.Location = new System.Drawing.Point(200, 185);
        _groupComboBox.Size = new System.Drawing.Size(400, 30);
        _groupComboBox.Font = new System.Drawing.Font("Segoe UI", 12F);
        _groupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        _themeLabel = new Label();
        _themeLabel.Text = "Тема:";
        _themeLabel.Location = new System.Drawing.Point(200, 230);
        _themeLabel.Font = new System.Drawing.Font("Segoe UI", 11F);

        _themeComboBox = new ComboBox();
        _themeComboBox.Location = new System.Drawing.Point(200, 255);
        _themeComboBox.Size = new System.Drawing.Size(400, 30);
        _themeComboBox.Font = new System.Drawing.Font("Segoe UI", 12F);
        _themeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        _startButton = new Button();
        _startButton.Text = "Начать викторину";
        _startButton.Location = new System.Drawing.Point(200, 320);
        _startButton.Size = new System.Drawing.Size(400, 45);
        _startButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);

        _leadersButton = new Button();
        _leadersButton.Text = "Таблица лидеров";
        _leadersButton.Location = new System.Drawing.Point(200, 375);
        _leadersButton.Size = new System.Drawing.Size(400, 45);
        _leadersButton.Font = new System.Drawing.Font("Segoe UI", 12F);

        _themeSwitchCheckBox = new CheckBox();
        _themeSwitchCheckBox.Text = "Тёмная тема";
        _themeSwitchCheckBox.Location = new System.Drawing.Point(20, 20);
        _themeSwitchCheckBox.Font = new System.Drawing.Font("Segoe UI", 10F);
        _themeSwitchCheckBox.AutoSize = true;

        _startPanel.Controls.AddRange(new Control[] {
            _playerNameLabel, _playerNameTextBox, _groupLabel, _groupComboBox,
            _themeLabel, _themeComboBox, _startButton, _leadersButton, _themeSwitchCheckBox
        });
        _startPanel.ResumeLayout(false);

        // 
        // Quiz Panel
        // 
        _quizPanel = new Panel();
        _quizPanel.Dock = DockStyle.Fill;
        _quizPanel.Visible = false;
        _quizPanel.SuspendLayout();

        _questionLabel = new Label();
        _questionLabel.Location = new System.Drawing.Point(50, 50);
        _questionLabel.Size = new System.Drawing.Size(700, 100);
        _questionLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);

        _answersGroupBox = new GroupBox();
        _answersGroupBox.Location = new System.Drawing.Point(50, 160);
        _answersGroupBox.Size = new System.Drawing.Size(700, 200);
        _answersGroupBox.Font = new System.Drawing.Font("Segoe UI", 12F);

        _exitQuizButton = new Button();
        _exitQuizButton.Text = "Выход";
        _exitQuizButton.Location = new System.Drawing.Point(700, 10);
        _exitQuizButton.Size = new System.Drawing.Size(80, 30);
        _exitQuizButton.Font = new System.Drawing.Font("Segoe UI", 9F);

        _nextButton = new Button();
        _nextButton.Text = "Следующий вопрос";
        _nextButton.Location = new System.Drawing.Point(600, 380);
        _nextButton.Size = new System.Drawing.Size(150, 40);

        _quizPanel.Controls.AddRange(new Control[] { _questionLabel, _answersGroupBox, _nextButton, _exitQuizButton });
        _quizPanel.ResumeLayout(false);

        // 
        // Result Panel
        // 
        _resultPanel = new Panel();
        _resultPanel.Dock = DockStyle.Fill;
        _resultPanel.Visible = false;
        _resultPanel.SuspendLayout();

        _resultLabel = new Label();
        _resultLabel.Location = new System.Drawing.Point(50, 30);
        _resultLabel.Size = new System.Drawing.Size(700, 150);
        _resultLabel.Font = new System.Drawing.Font("Segoe UI", 14F);
        _resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        _historyButton = new Button();
        _historyButton.Text = "Моя история";
        _historyButton.Font = new System.Drawing.Font("Segoe UI", 12F);
        _historyButton.Location = new System.Drawing.Point(200, 250);
        _historyButton.Size = new System.Drawing.Size(200, 45);
        
        _statsButton = new Button();
        _statsButton.Text = "Статистика";
        _statsButton.Font = new System.Drawing.Font("Segoe UI", 12F);
        _statsButton.Location = new System.Drawing.Point(420, 250);
        _statsButton.Size = new System.Drawing.Size(200, 45);
        
        _reviewButton = new Button();
        _reviewButton.Text = "Показать разбор";
        _reviewButton.Font = new System.Drawing.Font("Segoe UI", 12F);
        _reviewButton.Location = new System.Drawing.Point(300, 320);
        _reviewButton.Size = new System.Drawing.Size(200, 45);

        _restartButton = new Button();
        _restartButton.Text = "Начать заново";
        _restartButton.Location = new System.Drawing.Point(300, 450);
        _restartButton.Size = new System.Drawing.Size(200, 45);
        _restartButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);

        _resultPanel.Controls.AddRange(new Control[] { _resultLabel, _historyButton, _statsButton, _reviewButton, _restartButton });
        _resultPanel.ResumeLayout(false);

        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.ClientSize = new System.Drawing.Size(800, 550);
        this.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.Text = "Викторина";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Controls.AddRange(new Control[] { _startPanel, _quizPanel, _resultPanel });
        
        _startPanel.ResumeLayout(false);
        _quizPanel.ResumeLayout(false);
        _resultPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private Panel _startPanel;
    private Label _playerNameLabel;
    private TextBox _playerNameTextBox;
    private Label _groupLabel;
    private ComboBox _groupComboBox;
    private Label _themeLabel;
    private ComboBox _themeComboBox;
    private Button _startButton;
    private Button _leadersButton;
    private CheckBox _themeSwitchCheckBox;

    private Panel _quizPanel;
    private Label _questionLabel;
    private GroupBox _answersGroupBox;
    private Button _nextButton;
    private Button _exitQuizButton;

    private Panel _resultPanel;
    private Label _resultLabel;
    private Button _restartButton;
    private Button _historyButton;
    private Button _statsButton;
    private Button _reviewButton;
}

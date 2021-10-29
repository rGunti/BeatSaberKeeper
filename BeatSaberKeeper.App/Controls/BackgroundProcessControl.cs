using BeatSaberKeeper.App.Utils;
using System;
using System.Data;
using System.Windows.Forms;

namespace BeatSaberKeeper.App.Controls
{
    public partial class BackgroundProcessControl : Form, IStatusDialog
    {
        private readonly Action<IStatusDialog> _action;
        private readonly Action _completion;

        private readonly TimeSpan _throttle;

        private DateTime _lastExecution = DateTime.MinValue;

        private string _currentStatus;
        private int _currentValue;
        private int _maxValue;

        private Timer _timer = new();

        public BackgroundProcessControl(
            string title,
            Action<IStatusDialog> action,
            Action completion = null,
            TimeSpan? uiThrottle = null)
        {
            InitializeComponent();
            Text = title;
            _action = action;
            _completion = completion;
            _throttle = uiThrottle ?? TimeSpan.Zero;
            if (_throttle.Milliseconds > 0)
                _timer.Interval = _throttle.Milliseconds;
            _timer.Tick += (_, _) => UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            StatusLabel.Text = _currentStatus;
            if (_currentValue < 0)
            {
                ProgressBar.Style = ProgressBarStyle.Marquee;

                PercentageLabel.Visible = false;
            }
            else
            {
                ProgressBar.Style = ProgressBarStyle.Continuous;
                ProgressBar.Maximum = Math.Max(_maxValue, 0);
                ProgressBar.Value = Math.Min(_currentValue, _maxValue);

                double pctValue = Math.Floor((double)_currentValue / _maxValue * 100);
                PercentageLabel.Text = @$"{pctValue:0} %";
                PercentageLabel.Visible = true;
            }
        }

        private void BackgroundProcessControl_Load(object sender, EventArgs e)
        {
            if (_throttle.Milliseconds > 0)
            {
                _timer.Start();
            }

            this.RunInBackgroundThread(() =>
            {
                _action(this);
            }, () =>
            {
                _completion?.Invoke();
                this.Close();
            });
        }

        public void SetStatus(string status)
        {
            this.RunInUiThread(() => { StatusLabel.Text = status; });
        }

        public void SetStatus(string status, int value, int maxValue = 100)
        {
            this.RunInUiThread(() =>
            {
                _currentStatus = status;
                _currentValue = value;
                _maxValue = maxValue;

                if (_throttle.Milliseconds == 0)
                    UpdateDisplay();
            });
        }
    }
}

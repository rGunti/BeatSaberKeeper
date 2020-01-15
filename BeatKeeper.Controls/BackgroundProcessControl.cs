using System;
using System.Windows.Forms;
using BeatKeeper.Controls.Utils;

namespace BeatKeeper.Controls
{
    public partial class BackgroundProcessControl : Form, IStatusDialog
    {
        private readonly Action<IStatusDialog> _action;
        private readonly Action _completion;

        private readonly TimeSpan _throttle;

        private DateTime _lastExecution = DateTime.MinValue;

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
        }

        private void BackgroundProcessControl_Load(object sender, EventArgs e)
        {
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
            if (_throttle > TimeSpan.Zero
                && _lastExecution + _throttle > DateTime.Now
                && ProgressBar.Maximum == maxValue)
            {
                return;
            }

            this.RunInUiThread(() =>
            {
                StatusLabel.Text = status;
                if (value < 0)
                {
                    ProgressBar.Style = ProgressBarStyle.Marquee;

                    PercentageLabel.Visible = false;
                }
                else
                {
                    ProgressBar.Style = ProgressBarStyle.Continuous;
                    ProgressBar.Maximum = Math.Max(maxValue, 0);
                    ProgressBar.Value = Math.Min(value, maxValue);

                    PercentageLabel.Text = $"{((double)value / maxValue) * 100:0} %";
                    PercentageLabel.Visible = true;
                }

                if (_throttle > TimeSpan.Zero)
                {
                    _lastExecution = DateTime.Now;
                }
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BeatKeeper.Utils
{
    public class LoadingCursor : IDisposable
    {
        public LoadingCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = Cursors.Default;
        }
    }
}

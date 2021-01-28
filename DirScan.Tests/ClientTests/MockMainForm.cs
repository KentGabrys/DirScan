using System;
using DirScan.Client;

namespace DirScan.Tests
{
    public class MockMainForm : MainForm
    {
        private bool _statusResizeHasBeenCalled;
        private bool _setModelLoggerHasBeenCalled;

        public bool StatusResizeHasBeenCalled
        {
            get => _statusResizeHasBeenCalled;
            set => _statusResizeHasBeenCalled = value;
        }

        public override void status_Resize( object sender, EventArgs e )
        {
            _statusResizeHasBeenCalled = true;
        }


        public bool SetModelLoggerHasBeenCalled
        {
            get => _setModelLoggerHasBeenCalled;
            set => _setModelLoggerHasBeenCalled = value;
        }

        public override void SetModelLogger()
        {
            _setModelLoggerHasBeenCalled = true;
        }
    }
}
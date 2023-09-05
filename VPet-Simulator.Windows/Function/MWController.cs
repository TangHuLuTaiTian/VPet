﻿using System.Windows.Forms;
using System.Windows.Interop;
using System.Drawing;
using VPet_Simulator.Core;

namespace VPet_Simulator.Windows
{
    /// <summary>
    /// 窗体控制器实现
    /// </summary>
    public class MWController : IController
    {
        readonly MainWindow mw;
        public MWController(MainWindow mw)
        {
            this.mw = mw;
        }

        private static Rectangle _screenBorder;
        private static bool _isPrimaryScreen = true;
        public static bool IsPrimaryScreen
        {
            get
            {
                return _isPrimaryScreen;
            }
        }
        public static Rectangle ScreenBorder
        {
            get
            {
                return _screenBorder;
            }
            set
            {
                _screenBorder = value;
                _isPrimaryScreen = false;
            }
        }
        public static void ResetScreenBorder()
        {
            _isPrimaryScreen = true;
        }

        public double GetWindowsDistanceLeft()
        {
            return mw.Dispatcher.Invoke(() =>
            {
                if (IsPrimaryScreen) return mw.Left;
                return mw.Left - ScreenBorder.X;
            });
        }

        public double GetWindowsDistanceUp()
        {
            return mw.Dispatcher.Invoke(() =>
            {
                if (IsPrimaryScreen) return mw.Top;
                return mw.Top - ScreenBorder.Y;
            });
        }

        public double GetWindowsDistanceRight()
        {
            return mw.Dispatcher.Invoke(() =>
            {
                if (IsPrimaryScreen) return System.Windows.SystemParameters.PrimaryScreenWidth - mw.Left - mw.Width;
                return ScreenBorder.Width + ScreenBorder.X - mw.Left - mw.Width;
            });
        }

        public double GetWindowsDistanceDown()
        {
            return mw.Dispatcher.Invoke(() =>
            {
                if (IsPrimaryScreen) return System.Windows.SystemParameters.PrimaryScreenHeight - mw.Top - mw.Height;
                return ScreenBorder.Height + ScreenBorder.Y - mw.Top - mw.Height;
            });
        }

        public void MoveWindows(double X, double Y)
        {
            mw.Dispatcher.Invoke(() =>
            {
                mw.Left += X * ZoomRatio;
                mw.Top += Y * ZoomRatio;
            });
        }

        public void ShowSetting()
        {
            mw.Topmost = false;
            mw.ShowSetting();
        }

        public void ShowPanel()
        {
            var panelWindow = new winCharacterPanel();
            panelWindow.ShowDialog();
        }

        public double ZoomRatio => mw.Set.ZoomLevel;

        public int PressLength => mw.Set.PressLength;

        public bool EnableFunction => mw.Set.EnableFunction;

        public int InteractionCycle => mw.Set.InteractionCycle;

    }
}

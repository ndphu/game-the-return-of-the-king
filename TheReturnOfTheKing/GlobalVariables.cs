using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheReturnOfTheKing
{
    public static class GlobalVariables
    {
        private static float _dX;

        public static float dX
        {
            get { return GlobalVariables._dX; }
            set { GlobalVariables._dX = value; }
        }

        private static float _dY;

        public static float dY
        {
            get { return GlobalVariables._dY; }
            set { GlobalVariables._dY = value; }
        }

        private static float _offset;

        public static float Offset
        {
            get { return GlobalVariables._offset; }
            set { GlobalVariables._offset = value; }
        }

        static int _screenWidth;

        public static int ScreenWidth
        {
            get { return GlobalVariables._screenWidth; }
            set { GlobalVariables._screenWidth = value; }
        }

        static int _screenHeight;

        public static int ScreenHeight
        {
            get { return GlobalVariables._screenHeight; }
            set { GlobalVariables._screenHeight = value; }
        }

        static ButtonManger _btm;

        public static ButtonManger Btm
        {
            get { return GlobalVariables._btm; }
            set { GlobalVariables._btm = value; }
        }

        static MenuManager _mnm;

        public static MenuManager Mnm
        {
            get { return GlobalVariables._mnm; }
            set { GlobalVariables._mnm = value; }
        }

        static MouseObserver _mouseObserver;

        public static MouseObserver MouseObserver
        {
            get { return GlobalVariables._mouseObserver; }
            set { GlobalVariables._mouseObserver = value; }
        }

        static KeyboardObserver _keyboardObserver;

        public static KeyboardObserver KeyboardObserver
        {
            get { return GlobalVariables._keyboardObserver; }
            set { GlobalVariables._keyboardObserver = value; }
        }
    }
}

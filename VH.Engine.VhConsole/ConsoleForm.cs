﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VH.Engine.Configuration;

namespace VH.Engine.VhConsole {

    public partial class ConsoleForm : Form {

        #region delegates

        public delegate void writeDelegate();

        #endregion

        #region constants

        private const int WIDTH = 80;
        private const int HEIGHT = 50;
        private const char NEWLINE = '\r';

        #endregion

        #region fields
        //
        volatile string inputBuffer = "";
        volatile string outputBuffer;
        volatile private bool ready = false;

        StringBuilder screenBuffer = new StringBuilder();

        int cursorX = 0;
        int cursorY = 0;
        int fontWidth = 14;
        int fontHeight = 20;
        int xOffset = 0;

        Bitmap bitmap;
        Graphics graphics;
        Font font = new Font(Options.Instance["font"], 10, FontStyle.Bold);
        SolidBrush brush = new SolidBrush(Color.LightGreen);
        Brush deleteBrush = new SolidBrush(Color.Black);
        ConsoleColor foregroundColor;

        ManualResetEvent mri = new ManualResetEvent(false);
        ManualResetEvent mri2 = new ManualResetEvent(false);

        bool echo = false;
        bool cursorVisible = true;
        

        #endregion

        #region constructors

        public ConsoleForm() {
            InitializeComponent();
            BackColor = Color.Black;
            Size = Screen.PrimaryScreen.Bounds.Size;
            this.Location = new Point(0, 0);
        }

        #endregion

        #region properties

        public bool Ready {
            get {
                return ready;
            }
        }

        public bool Echo {
            get { return echo; }
            set { echo = value; }
        }

        public bool ShowCursor {
            get { return cursorVisible; }
            set { cursorVisible = value; }
        }

        public int CursorX {
            get { return cursorX; }
            set { cursorX = value; }
        }

        public int CursorY {
            get { return cursorY; }
            set { cursorY = value; }
        }

        /// <summary>
        /// Gets or sets the number of columns of this IConsole
        /// </summary>
        public int ConsoleWidth {
            get { return WIDTH; }
            set { }
        }

        /// <summary>
        /// Gets or sets the number of rows of this IConsole
        /// </summary>
        public int ConsoleHeight {
            get { return HEIGHT; }
            set { }
        }

        /// <summary>
        /// Indicates the maximum possible number of columns of this IConsole
        /// </summary>
        int MaximnumWidth {
            get { return WIDTH; }
        }

        /// <summary>
        /// Indicates the maximum possible number of rows of this IConsole
        /// </summary>
        int MaximumHeight {
            get { return HEIGHT; }
        }

        /// <summary>
        /// Gets or sets the color with which the next write operation will use as the foreground color
        /// </summary>
        public ConsoleColor ForegroundColor {
            get { return foregroundColor; }
            set {
                foregroundColor = value;
                brush.Color = toColor(foregroundColor);
            }
        }

        /// <summary>
        /// Gets or sets the color with which the next write operation will use as the background color
        /// </summary>
        ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cursor is visible
        /// </summary>
        public bool CursorVisible {
            get { return cursorVisible; }
            set { cursorVisible = value; }
        }

        #endregion

        #region public methods

        public void GoTo(int x, int y) {
            CursorX = x;
            CursorY = y;
        }

        public void FeedLine() {
            GoTo(0, cursorY + 1);
        }

        public void WriteLine(string s) {
            s += "\r";
            outputBuffer = s;
            Invoke(new writeDelegate(writeLine));
        }

        public void Write(char c, int x, int y) {
            GoTo(x, y);
            Write(c);
        }

        public void RefreshConsole() {
            Invoke(new writeDelegate(Refresh));
        }

        public void Write(string s) {
            outputBuffer = s;
            Invoke(new writeDelegate(write));
        }

        public void Write(char c) {
            outputBuffer = "" + c;
            if (c == '\r') FeedLine();
            else Invoke(new writeDelegate(write));
        }

        public char ReadKey() {
            if (inputBuffer.Length == 0) {
                mri.Reset();
                mri.WaitOne();
            }
            mri.Reset();
            int key = inputBuffer[0];
            inputBuffer = inputBuffer.Substring(1);
            return (char)key;
        }

        public string ReadLine() {
            int i = inputBuffer.IndexOf(NEWLINE);
            if (i < 0) mri2.WaitOne();
            mri2.Reset();
            i = inputBuffer.IndexOf(NEWLINE);
            if (i < 0) return "";
            string line = inputBuffer.Substring(0, i);
            inputBuffer = inputBuffer.Substring(i + 1);
            return line;
        }

        /// <summary>
        /// Clears the buffer of this IConsole.
        /// </summary>
        public void ClearBuffer() {
            screenBuffer.Clear();
        }

        /// <summary>
        /// Clears the whole IConsole
        /// </summary>
        public void Clear() {
            graphics.Clear(Color.Black);
        }

        public void Clear(int x, int y, int width, int height) {
            x = x * fontWidth + xOffset;
            y *= fontHeight;
            width *= fontWidth;
            height *= fontHeight;
            Rectangle rec = new Rectangle(x, y, width, height);
            graphics.FillRectangle(deleteBrush, rec);
        }

        #endregion

        #region private methods

        private void writeLine() {
            Point p = new Point(cursorX * fontWidth, cursorY * fontHeight);
            for (int i = 0; i < outputBuffer.Length; ++i) {
                p.X = cursorX * fontWidth + xOffset;
                p.Y = cursorY * fontHeight;
                graphics.DrawString("" + outputBuffer[i], font, brush, p);
                GoTo(cursorX + 1, CursorY);
            }
            FeedLine();
            Refresh();
        }

        private void write() {
            Point p = new Point(cursorX * fontWidth + xOffset, cursorY * fontHeight);
            Rectangle rec = new Rectangle(p.X + 1, p.Y + 1, fontWidth * outputBuffer.Length + 1, fontHeight);
            graphics.FillRectangle(deleteBrush, rec);
            graphics.DrawString("" + outputBuffer, font, brush, p);
            GoTo(cursorX + outputBuffer.Length, cursorY);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\b') inputBuffer = inputBuffer.Substring(0, inputBuffer.Length - 1); // support for backspace
            else inputBuffer += e.KeyChar;
            mri.Set();
            if (e.KeyChar == NEWLINE) {
                mri2.Set();
            }
            if (echo) {
                Write(e.KeyChar);
                Refresh();
            }
        }

        private void ConsoleForm_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
        }

        private Color toColor(ConsoleColor color) {
            switch (color) {
                case ConsoleColor.Black: return Color.Black;
                case ConsoleColor.Blue: return Color.Blue;
                case ConsoleColor.Cyan: return Color.Cyan;
                case ConsoleColor.DarkBlue: return Color.DarkBlue;
                case ConsoleColor.DarkCyan: return Color.DarkCyan;
                case ConsoleColor.DarkGray: return Color.FromArgb(100, 100, 100);
                case ConsoleColor.DarkGreen: return Color.DarkGreen;
                case ConsoleColor.DarkMagenta: return Color.DarkMagenta;
                case ConsoleColor.DarkRed: return Color.DarkRed;
                case ConsoleColor.DarkYellow: return Color.SaddleBrown;
                case ConsoleColor.Gray: return Color.Gray;
                case ConsoleColor.Green: return Color.Green;
                case ConsoleColor.Magenta: return Color.Magenta;
                case ConsoleColor.Red: return Color.Red;
                case ConsoleColor.White: return Color.White;
                case ConsoleColor.Yellow: return Color.Yellow;
                default: return Color.LightGray;
            }
        }

        private void ConsoleForm_Load(object sender, EventArgs e) {
            int hRes = ClientRectangle.Width;
            int vRes = ClientRectangle.Height;
            bitmap = new Bitmap(hRes, vRes, PixelFormat.Format24bppRgb);
            graphics = Graphics.FromImage(bitmap);

            string s = "12345678";

            int fontSize = 1;
            SizeF stringSize;
            do {
                Font font = new Font(Options.Instance["font"], fontSize, FontStyle.Bold);
                stringSize = graphics.MeasureString(s, font);
                fontSize++;
            } while (stringSize.Width * 10 <= hRes && stringSize.Height * 50 <= vRes);
            fontSize--;
            this.font = new Font(Options.Instance["font"], fontSize, FontStyle.Bold);
            stringSize = graphics.MeasureString(s, this.font);
            this.fontWidth = (int)(stringSize.Width / 8.0f);
            this.fontHeight = (int)stringSize.Height - 2;

            xOffset = (int)(hRes - stringSize.Width * 10) / 2;

            Text = Application.ProductName;
            ready = true;
        }

        #endregion


    }
}

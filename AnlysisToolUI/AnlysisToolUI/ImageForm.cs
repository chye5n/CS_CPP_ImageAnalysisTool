using DevExpress.Drawing;
using DevExpress.Utils.Animation;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Export.Pdf.Compression;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace AnlysisToolUI
{
    public partial class ImageForm : Form
    {
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]    //정적 링크 방식
        private static extern void ImageLoad(string imagePath, out IntPtr imageData, out int width, out int height, out int channel, out int index);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreeData(IntPtr imageData);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ImageBoxSize(int formWidth, int formHeight, out int width, out int height);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void MagOrShr(int nIndex, out IntPtr OutData, out int newWidth, out int newHeight, double nRate);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr HistogramData(int nIndex, int rectX, int rectY, int width, int height, int channels, double nRate);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr HistogramRGBData(int nIndex, int rectX, int rectY, int width, int height, int channels, int color, double nRate);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreeHistogramData(IntPtr data);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void threshHoldRed(int nIndex, out IntPtr OutData, int minGV, int maxGV, int width, int height);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void threshHoldBW(int nIndex, out IntPtr OutData, int minGV, int maxGV, int width, int height);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void threshHoldOverUnder(int nIndex, out IntPtr OutData, int minGV, int maxGV, int width, int height);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int threshHoldOtsu(int nIndex, out IntPtr OutData, out int width, out int height);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void resetThres(int nIndex, out IntPtr OutData, out int width, out int height);
        [DllImport("AnalysisTool.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr BlobInform(int nIndex, int minGV, int maxGV, out int count, out IntPtr OutData, out int width, out int height, int classify, int lowValue, int highValue);

        private string ImageName;   //이미지의 제목
        private string SelectItem;  //선택되어있는 아이템 정보

        private Rectangle rectangle; // 그려진 사각형 영역
        private Point startPoint; // 사각형의 시작점
        private Point endPoint; // 사각형의 끝점
        private Point ScrollPoint;  //스크롤
        private Point offset; // 마우스 클릭 시 사각형의 왼쪽 상단과 클릭 위치의 차이를 저장하는 변수

        private bool drawingRectangle = false; // 사각형을 그리는 중인지 여부를 나타내는 플래그
        private bool movingRectangle = false; // 사각형을 이동하는 중인지 여부를 나타내는 플래그
        private bool resizingRectangle = false; // 사각형을 조절하는 중인지 여부를 나타내는 플래그

        private RectPos rectPos = RectPos.None;

        private int index;      //dll vector에 사진이 저장되어있는 인덱스 번호
        private int rate = 0;   //확대 비율
        private int nChannels;  //이미지의 채널 수
        public int rgbData = 0; //히스토그램을 사용할 경우 R, G, B, 흑백 데이터 구분

        private HistogramForm histogramForm;
        public bool IsHistogram = false;        //히스토그램 폼 확인
        private bool IsThres = false;           //threshold 폼 확인
        private int SelectedIndex = 0;          //threshold 폼에서 선택된 인덱스 번호
        private int minGV = 0;
        private int maxGV = 0;

        Form1 toolForm;

        private enum RectPos
        {
            TopLeft, TopMiddle, TopRight,
            MiddleLeft, MiddleRight,
            BottomLeft, BottomMiddle, BottomRight,

            None
        };

        public ImageForm(Form1 form1, string fileName = "", string selectTool = "")
        {
            InitializeComponent();
            bmpImageLoad(fileName);
            string[] parts = fileName.Split('\\');
            ImageName = parts[parts.Length - 1];
            this.Text = ImageName;
            pictureBox.Size = pictureBox.Image.Size;
            panel.Size = pictureBox.Size;
            ClientSize = new Size(panel.Size.Width + 10, panel.Size.Height + 25);

            toolForm = form1;
            SelectItem = selectTool;
        }

        public void SetMenuItem(string menuItem)
        {
            SelectItem = menuItem;
        }

        private void bmpImageLoad(string fileName)    //이미지 불러오기
        {
            IntPtr imageData;
            int width, height;
            ImageLoad(fileName, out imageData, out width, out height, out nChannels, out index);
            Bitmap bitmap = ConvertBMP(imageData, width, height, nChannels);
            string strFormat;
            if (nChannels == 1) { strFormat = "8bit"; }
            else if (nChannels == 3) { strFormat = "RGB"; }
            else if (nChannels == 4) { strFormat = "ARGB"; }
            else { strFormat = ""; }
            lb_ImageInform.Text = width.ToString() + "*" + height.ToString() + ", Channel: " + nChannels.ToString() + ", " + strFormat;
            pictureBox.Image = bitmap;
            FreeData(imageData);
        }

        /*IntPtr to Bitmap*/
        private Bitmap ConvertBMP(IntPtr imageData, int width, int height, int channel) //이미지를 불러올 경우
        {
            byte[] imageBytes = new byte[width * height * channel]; // 3은 RGB 채널 수에 해당합니다
            Marshal.Copy(imageData, imageBytes, 0, imageBytes.Length);
            Bitmap bitmap;

            if (channel == 1)
            {
                bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                ColorPalette grayscalePalette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    grayscalePalette.Entries[i] = Color.FromArgb(i, i, i);
                }
                bitmap.Palette = grayscalePalette;
            }
            else if (channel == 3) { bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb); }
            else { bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb); }

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int stride = bitmapData.Stride;
            int bytesPerPixel = Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width * bytesPerPixel; x++)
                {
                    Marshal.WriteByte(bitmapData.Scan0, y * stride + x, imageBytes[y * width * channel + x]);
                }
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        private void ImageForm_Resize(object sender, EventArgs e)   //폼 크기 변환
        {
            int width, height;
            ImageBoxSize(ClientSize.Width, ClientSize.Height, out width, out height);

            pictureBox.SizeMode = PictureBoxSizeMode.Normal;
            panel.Size = new Size(width, height);
            if (pictureBox.Width <= panel.Width) { pictureBox.Left = (panel.Width - pictureBox.Width) / 2; }
            if (pictureBox.Height <= panel.Height) { pictureBox.Top = (panel.Height - pictureBox.Height) / 2; }
        }

        /*확대 축소*/
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (SelectItem == "Magnify")
            {
                if (e.Button == MouseButtons.Left) { ImageMagShr(true); }
                else if (e.Button == MouseButtons.Right) { ImageMagShr(false); }
            }
        }

        private void ImageMagShr(bool IsLeft)
        {
            if ((rate > 3) || (rate < -3)) { return; }
            drawingRectangle = true;
            if (IsLeft)
            {
                rate++;
                startPoint.X *= 2;
                startPoint.Y *= 2;
                endPoint.X *= 2;
                endPoint.Y *= 2;
            }
            else
            {
                rate--;
                startPoint.X /= 2;
                startPoint.Y /= 2;
                endPoint.X /= 2;
                endPoint.Y /= 2;
            }
            IntPtr OutData;
            int newWidth, newHeight;
            MagOrShr(index, out OutData, out newWidth, out newHeight, Math.Pow(2, rate));
            Bitmap changebmp = ConvertBMP(OutData, newWidth, newHeight, nChannels); ;
            // 이미지 처리 함수 호출
            if (IsThres)
            {
                if (SelectedIndex == 0) { thresholdRed(minGV, maxGV, newWidth, newHeight); }
                else if (SelectedIndex == 1) { thresholdBW(minGV, maxGV, newWidth, newHeight); }
                else if (SelectedIndex == 2) { thresholdOverUnder(minGV, maxGV, newWidth, newHeight); }
            }
            else { pictureBox.Image = changebmp; }
            //changebmp = ConvertBMP(OutData, newWidth, newHeight, nChannels);
            pictureBox.Size = changebmp.Size;
            if (pictureBox.Width > panel.Width) { pictureBox.Left = 0; }
            if (pictureBox.Height > panel.Height) { pictureBox.Top = 0; }
            if (rate < 2)
            {
                panel.Size = changebmp.Size;
                ClientSize = new Size(pictureBox.Width + 10, pictureBox.Height + 25);
            }

            this.Text = ImageName + "(" + 100 * Math.Pow(2, rate) + "%)";
            rectangle = GetRectangle(startPoint, endPoint); // 그려진 사각형 영역 설정
            pictureBox.Refresh();
            FreeData(OutData);
        }

        /*그리기 이벤트*/
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox.Image != null)
            {
                if (drawingRectangle || movingRectangle || resizingRectangle)
                {
                    Pen pen = new Pen(Color.Blue, 2);
                    Pen blackPen = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(pen, rectangle);   // 사각형 그리기

                    foreach (var corner in GetCirclePoints(rectangle))
                    {
                        e.Graphics.DrawEllipse(blackPen, new Rectangle(corner.X - 3, corner.Y - 3, 6, 6));
                        e.Graphics.FillEllipse(Brushes.White, new Rectangle(corner.X - 3, corner.Y - 3, 6, 6));
                    }
                }
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectItem == "Rect")
            {
                foreach (var pos in Enum.GetValues(typeof(RectPos)).Cast<RectPos>())
                {
                    if (GetCorner(rectangle, pos).Contains(e.Location))
                    {
                        rectPos = pos;
                        break;
                    }
                }

                if (rectPos == RectPos.None)
                {
                    if (rectangle.Contains(e.Location))
                    {
                        movingRectangle = true;
                        offset = new Point(e.X - rectangle.X, e.Y - rectangle.Y);
                    }
                    else
                    {
                        startPoint = e.Location; // 시작점 설정
                        drawingRectangle = true;
                    }
                }
                else
                {
                    resizingRectangle = true;
                    endPoint = e.Location;
                }
            }
            else if (SelectItem == "Scroll")
            {
                if ((panel.Width < pictureBox.Width) || (panel.Height < pictureBox.Height))
                {
                    movingRectangle = true;
                    ScrollPoint = e.Location;
                }
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            toolForm.lb_inform.Text = "x = " + e.X + ", Y = " + e.Y;
            switch (SelectItem)
            {
                case "Rect":
                    if (resizingRectangle)
                    {
                        resizeRect(e);
                        endPoint = e.Location;
                        pictureBox.Refresh();
                    }
                    else if (movingRectangle)
                    {
                        int newX = e.X - offset.X;
                        int newY = e.Y - offset.Y;

                        newX = Math.Max(0, Math.Min(pictureBox.Width - rectangle.Width, newX));
                        newY = Math.Max(0, Math.Min(pictureBox.Height - rectangle.Height, newY));

                        rectangle.Location = new Point(newX, newY);
                        pictureBox.Refresh();
                    }
                    else if (drawingRectangle)
                    {
                        endPoint = e.Location; // 끝점 설정
                        rectangle = GetRectangle(startPoint, endPoint); // 그려진 사각형 영역 설정
                        pictureBox.Refresh();
                    }
                    if (IsHistogram) { Histogram(); }
                    return;
                case "Scroll":
                    if (movingRectangle)
                    {
                        int deltaX = e.X - ScrollPoint.X;
                        int deltaY = e.Y - ScrollPoint.Y;

                        // pictureBox의 위치를 이동 거리에 따라 조절
                        pictureBox.Left += deltaX;
                        pictureBox.Top += deltaY;

                        // 시작 위치를 현재 위치로 업데이트
                        ScrollPoint = e.Location;
                    }
                    return;
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (resizingRectangle) { resizingRectangle = false; }
            else if (movingRectangle) { movingRectangle = false; }
            else if (drawingRectangle)
            {
                drawingRectangle = false;
                if (SelectItem == "Rect") { endPoint = e.Location; } // 마우스 놓은 위치를 끝점으로 설정
                rectangle = GetRectangle(startPoint, endPoint); // 그려진 사각형 영역 설정
            }
            rectPos = RectPos.None;
        }

        private Rectangle GetRectangle(Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(start.X - end.X);
            int height = Math.Abs(start.Y - end.Y);
            return new Rectangle(x, y, width, height);
        }

        private Point[] GetCirclePoints(Rectangle rect) //
        {
            return new Point[]
            {
                new Point(rect.Left, rect.Top),                         //왼쪽 위
                new Point((rect.Left + rect.Right) / 2, rect.Top),      //가운데 위
                new Point(rect.Right, rect.Top),                        //오른쪽 위
                new Point(rect.Left, (rect.Top + rect.Bottom) / 2),     //왼쪽 가운데
                new Point(rect.Right, (rect.Top + rect.Bottom) / 2),    //오른쪽 가운데
                new Point(rect.Left, rect.Bottom),                      //왼쪽 아래
                new Point((rect.Left + rect.Right) / 2, rect.Bottom),   //가운데 아래
                new Point(rect.Right, rect.Bottom)                      //오른쪽 아래
            };
        }

        private Rectangle GetCorner(Rectangle rect, RectPos pos)
        {
            switch (pos)
            {
                case RectPos.TopLeft:
                    return new Rectangle(rect.Left - 3, rect.Top - 3, 6, 6);
                case RectPos.TopMiddle:
                    return new Rectangle((rect.Left + rect.Right) / 2 - 3, rect.Top - 3, 6, 6);
                case RectPos.TopRight:
                    return new Rectangle(rect.Right - 3, rect.Top - 3, 6, 6);
                case RectPos.MiddleLeft:
                    return new Rectangle(rect.Left - 3, (rect.Top + rect.Bottom) / 2 - 3, 6, 6);
                case RectPos.MiddleRight:
                    return new Rectangle(rect.Right - 3, (rect.Top + rect.Bottom) / 2 - 3, 6, 6);
                case RectPos.BottomLeft:
                    return new Rectangle(rect.Left - 3, rect.Bottom - 3, 6, 6);
                case RectPos.BottomMiddle:
                    return new Rectangle((rect.Left + rect.Right) / 2 - 3, rect.Bottom - 3, 6, 6);
                case RectPos.BottomRight:
                    return new Rectangle(rect.Right - 3, rect.Bottom - 3, 6, 6);
                default:
                    return Rectangle.Empty;
            }
        }

        private void resizeRect(MouseEventArgs e)
        {
            int newWidth, newHeight;
            switch (rectPos)
            {
                case RectPos.TopLeft:
                    newWidth = rectangle.Right - e.X;
                    newHeight = rectangle.Bottom - e.Y;
                    rectangle.X = e.X;
                    rectangle.Y = e.Y;
                    rectangle.Width = newWidth;
                    rectangle.Height = newHeight;
                    return;
                case RectPos.TopMiddle:
                    newHeight = rectangle.Bottom - e.Y;
                    rectangle.Y = e.Y;
                    rectangle.Height = newHeight;
                    return;
                case RectPos.TopRight:
                    newWidth = e.X - rectangle.Left;
                    newHeight = rectangle.Bottom - e.Y;
                    rectangle.Y = e.Y;
                    rectangle.Width = newWidth;
                    rectangle.Height = newHeight;
                    return;
                case RectPos.MiddleLeft:
                    newWidth = rectangle.Right - e.X;
                    rectangle.X = e.X;
                    rectangle.Width = newWidth;
                    return;
                case RectPos.MiddleRight:
                    rectangle.Width = e.X - rectangle.Left;
                    return;
                case RectPos.BottomLeft:
                    newWidth = rectangle.Right - e.X;
                    rectangle.X = e.X;
                    rectangle.Width = newWidth;
                    rectangle.Height = e.Y - rectangle.Top;
                    return;
                case RectPos.BottomMiddle:
                    rectangle.Height = e.Y - rectangle.Top;
                    return;
                case RectPos.BottomRight:
                    rectangle.Width = e.X - rectangle.Left;
                    rectangle.Height = e.Y - rectangle.Top;
                    return;
            }
        }

        public void Histogram()
        {
            IntPtr nValuesPtr;
            switch (rgbData)
            {
                case 1:
                case 2:
                case 3:
                    if (rectangle.Width != 0 && rectangle.Height != 0) { nValuesPtr = HistogramRGBData(index, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, nChannels, rgbData - 1, Math.Pow(2, rate)); }
                    else { nValuesPtr = HistogramRGBData(index, 0, 0, pictureBox.Width, pictureBox.Height, nChannels, rgbData - 1, Math.Pow(2, rate)); }
                    break;
                default:
                    if (rectangle.Width != 0 && rectangle.Height != 0) { nValuesPtr = HistogramData(index, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, nChannels, Math.Pow(2, rate)); }
                    else { nValuesPtr = HistogramData(index, 0, 0, pictureBox.Width, pictureBox.Height, nChannels, Math.Pow(2, rate)); }
                    break;
            }
            int[] nValues = new int[256];
            Marshal.Copy(nValuesPtr, nValues, 0, 256);

            if (histogramForm == null)
            {
                histogramForm = new HistogramForm(nValues, this, nChannels);
                histogramForm.Show();
            }
            else 
            {
                histogramForm.AddChart(nValues);
                if (IsMove()) 
                {
                    Thread setlist = new Thread(histogramForm.GetListData);
                    setlist.Start();
                    //histogramForm.GetListData(); 
                }
            }
            FreeHistogramData(nValuesPtr);
        }

        public void HistogramArray()/**/
        {
            IntPtr nGrayValuesPtr, nBlueValuesPtr = (IntPtr)0, nGreenValuesPtr = (IntPtr)0, nRedValuesPtr = (IntPtr)0;
            if (rectangle.Width != 0 && rectangle.Height != 0)
            {
                nGrayValuesPtr = HistogramData(index, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, nChannels, Math.Pow(2, rate));
                if(nChannels != 1)
                {
                    nBlueValuesPtr = HistogramRGBData(index, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, nChannels, 0, Math.Pow(2, rate));
                    nGreenValuesPtr = HistogramRGBData(index, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, nChannels, 1, Math.Pow(2, rate));
                    nRedValuesPtr = HistogramRGBData(index, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, nChannels, 2, Math.Pow(2, rate));
                }
            }
            else
            {
                nGrayValuesPtr = HistogramData(index, 0, 0, pictureBox.Width, pictureBox.Height, nChannels, Math.Pow(2, rate));
                if(nChannels != 1) 
                {
                    nBlueValuesPtr = HistogramRGBData(index, 0, 0, pictureBox.Width, pictureBox.Height, nChannels, 0, Math.Pow(2, rate));
                    nGreenValuesPtr = HistogramRGBData(index, 0, 0, pictureBox.Width, pictureBox.Height, nChannels, 1, Math.Pow(2, rate));
                    nRedValuesPtr = HistogramRGBData(index, 0, 0, pictureBox.Width, pictureBox.Height, nChannels, 2, Math.Pow(2, rate));
                }

            }
            int[] nGrayValues = new int[256];
            int[] nBlueValues = new int[256];
            int[] nGreenValues = new int[256];
            int[] nRedValues = new int[256];
            Marshal.Copy(nGrayValuesPtr, nGrayValues, 0, 256);
            FreeHistogramData(nGrayValuesPtr);

            if (nChannels != 1)
            {
                Marshal.Copy(nBlueValuesPtr, nBlueValues, 0, 256);
                Marshal.Copy(nGreenValuesPtr, nGreenValues, 0, 256);
                Marshal.Copy(nRedValuesPtr, nRedValues, 0, 256);

                FreeHistogramData(nBlueValuesPtr);
                FreeHistogramData(nGreenValuesPtr);
                FreeHistogramData(nRedValuesPtr);
            }
            if (histogramForm != null)
            {
                histogramForm.strGrayValues = Array.ConvertAll(nGrayValues, n => n.ToString());
                histogramForm.strBlueValues = Array.ConvertAll(nBlueValues, n => n.ToString());
                histogramForm.strGreenValues = Array.ConvertAll(nGreenValues, n => n.ToString());
                histogramForm.strRedValues = Array.ConvertAll(nRedValues, n => n.ToString());
            }
        }

        public int[] thresholdChart()
        {
            IntPtr nValuesPtr = HistogramData(index, 0, 0, pictureBox.Width, pictureBox.Height, nChannels, Math.Pow(2, rate));
            int[] nValues = new int[256];
            Marshal.Copy(nValuesPtr, nValues, 0, 256);
            FreeHistogramData(nValuesPtr);
            return nValues;
        }

        public void thresholdRed(int minGV, int maxGV, int width = 0, int height = 0)
        {
            IntPtr OutData;
            if (width == 0 || height == 0) { width = pictureBox.Width; height = pictureBox.Height; }
            threshHoldRed(index, out OutData, minGV, maxGV, width, height);
            Bitmap changebmp = ConvertBMP(OutData, width, height, 3);
            pictureBox.Image = changebmp;
            FreeData(OutData);
            IsThres = true;
            SelectedIndex = 0;
            this.minGV = minGV;
            this.maxGV = maxGV;
        }

        public void thresholdBW(int minGV, int maxGV, int width = 0, int height = 0)
        {
            IntPtr OutData;
            if (width == 0 || height == 0) { width = pictureBox.Width; height = pictureBox.Height; }
            threshHoldBW(index, out OutData, minGV, maxGV, width, height);
            Bitmap changebmp = ConvertBMP(OutData, width, height, 1);
            pictureBox.Image = changebmp;
            FreeData(OutData);
            IsThres = true;
            SelectedIndex = 1;
            this.minGV = minGV;
            this.maxGV = maxGV;
        }

        public void thresholdOverUnder(int minGV, int maxGV, int width = 0, int height = 0)
        {
            IntPtr OutData;
            if (width == 0 || height == 0) { width = pictureBox.Width; height = pictureBox.Height; }
            threshHoldOverUnder(index, out OutData, minGV, maxGV, width, height);
            Bitmap changebmp = ConvertBMP(OutData, width, height, 3);
            pictureBox.Image = changebmp;
            FreeData(OutData);
            IsThres = true;
            SelectedIndex = 2;
            this.minGV = minGV;
            this.maxGV = maxGV;
        }

        public int thresholdOtsu()
        {
            IntPtr OutData;
            int width, height;
            int dThres = threshHoldOtsu(index, out OutData, out width, out height);
            Bitmap changebmp = ConvertBMP(OutData, width, height, 1);
            pictureBox.Image = changebmp;
            FreeData(OutData);
            return dThres;
        }

        public void ResetImage()
        {
            IntPtr OutData;
            int width, height;
            resetThres(index, out OutData, out width, out height);
            Bitmap changebmp = ConvertBMP(OutData, width, height, nChannels);
            pictureBox.Image = changebmp;
            FreeData(OutData);
            IsThres = false;
        }

        public int[] Blob(int minGV, int maxGV, int classify, int lowValue, int highValue, out Bitmap bmpOutData)
        {
            int nCount, width, height;
            IntPtr OutData;
            IntPtr BlobData = BlobInform(index, minGV, maxGV, out nCount, out OutData, out width, out height, classify, lowValue, highValue);
            bmpOutData = ConvertBMP(OutData, width, height, 3);
            int[] nBlobValues = new int[nCount];
            Marshal.Copy(BlobData, nBlobValues, 0, nCount);
            FreeHistogramData(BlobData);
            FreeData(OutData);

            return nBlobValues;
        }

        /*커서 변경*/
        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (SelectItem == "Rect") { Cursor = Cursors.Cross; }
            else if (SelectItem == "Magnify") { Cursor = Cursors.Hand; }
            else if (SelectItem == "Scroll") { Cursor = Cursors.SizeAll; }
        }

        /*저장할 이미지 반환*/
        public Image GetImage()
        {
            return pictureBox.Image;
        }

        public void CloseHistogramForm()
        {
            histogramForm = null;
            IsHistogram = false;
        }

        private void ImageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            histogramForm?.Dispose();
        }

        private void ImageForm_Activated(object sender, EventArgs e)
        {
            toolForm.ActivateForm = this.Text;
        }

        private void ImageForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.H) { Histogram(); }
            else if (e.Control && e.Shift && e.KeyCode == Keys.T) { toolForm.OpenthresholdForm(); }
        }

        public int GetnChannels()
        {
            return nChannels;
        }

        public bool IsMove()
        {
            return movingRectangle || resizingRectangle;
        }
    }
}

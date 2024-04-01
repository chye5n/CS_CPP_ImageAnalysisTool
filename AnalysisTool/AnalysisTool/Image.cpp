#include "pch.h"
#include "Image.h"

#define EXPORTDLL extern "C" __declspec(dllexport)

vector<Mat> img;

void ImageLoad(const char* imagePath, unsigned char** imageData, int* width, int* height, int* channel, int* index) 
{
	Mat mtImage = cv::imread(imagePath, IMREAD_UNCHANGED);
	if (mtImage.empty()) { return; }
    img.push_back(mtImage);
    *index = (int)img.size() - 1;
	*width = mtImage.cols;
	*height = mtImage.rows;
	*channel = mtImage.channels();

    // 이미 메모리가 할당되어 있는 경우 해제
    if (*imageData != nullptr) {
        delete[] * imageData;
    }

	int imageSize = mtImage.cols * mtImage.rows * mtImage.channels();
	*imageData = new unsigned char[imageSize];
	memcpy(*imageData, mtImage.data, imageSize);
}

void FreeData(unsigned char* imageData) { delete[] imageData; }

void ImageBoxSize(int formWidth, int formHeight, int* width, int* height)
{
	*width = formWidth - 10;
	*height = formHeight - 25;
}

void MagOrShr(int nIndex, unsigned char** OutData, int* newWidth, int* newHeight, double nRate)
{
    Mat src = img[nIndex];
    *newWidth = (int)(src.cols * nRate);
    *newHeight = (int)(src.rows * nRate);

    Mat dst;
    resize(src, dst, Size(*newWidth, *newHeight));

    int imageSize = dst.cols * dst.rows * dst.channels();
    *OutData = new unsigned char[imageSize];
    memcpy(*OutData, dst.data, imageSize);
}

int* HistogramData(int nIndex, int rectX, int rectY, int width, int height, int channels, double nRate)
{
    Mat src = img[nIndex];
    resize(src, src, Size((int)(src.cols * nRate), (int)(src.rows * nRate)));
    Rect rect = Rect(rectX, rectY, width, height); //x,y, width, height
    src = src(rect);
    if (src.empty()) { cout << "img Failed" << endl; return nullptr; }
    Mat dst;
    if (channels == 3) { cvtColor(src, dst, COLOR_BGR2GRAY); }
    else if (channels == 4) { cvtColor(src, dst, COLOR_BGRA2GRAY); }
    else { dst = src.clone(); }
    int* nValues = new int[256] {0};
    for (int y = 0; y < dst.rows; y++)
    {
        for (int x = 0; x < dst.cols; x++)
        {
            nValues[dst.at<uchar>(y, x)]++;
        }
    }
    return nValues;
}

int* HistogramRGBData(int nIndex, int rectX, int rectY, int width, int height, int channels, int color, double nRate)
{
    Mat src = img[nIndex];
    resize(src, src, Size((int)(src.cols * nRate), (int)(src.rows * nRate)));
    Rect rect = Rect(rectX, rectY, width, height); //x,y, width, height
    src = src(rect);
    if (src.empty()) { cout << "img Failed" << endl; return nullptr; }
    int* nValues = new int[256] {0};

    for (int y = 0; y < src.rows; y++)
    {
        for (int x = 0; x < src.cols; x++)
        {
            int nBGR = src.at<Vec3b>(y, x)[color];
            nValues[nBGR]++;
        }
    }
    return nValues;
}

void FreeHistogramData(int* data) { delete[] data; }

void threshHoldRed(int nIndex, unsigned char** OutData, int minGV, int maxGV, int width, int height)
{
    Mat mask;
    Mat src = img[nIndex];
    resize(src, src, Size(width, height));

    Mat dst = src.clone();
    cvtColor(dst, dst, COLOR_GRAY2BGR);
    inRange(src, minGV, maxGV, mask);
    dst.setTo(Scalar(0, 0, 255), mask);
    int imageSize = dst.cols * dst.rows * dst.channels();
    *OutData = new unsigned char[imageSize];
    memcpy(*OutData, dst.data, imageSize);
}

void threshHoldBW(int nIndex, unsigned char** OutData, int minGV, int maxGV, int width, int height)
{
    Mat src = img[nIndex];
    resize(src, src, Size(width, height));

    Mat dst = src.clone();
    inRange(src, minGV, maxGV, dst);
    bitwise_not(dst, dst);

    int imageSize = dst.cols * dst.rows * dst.channels();
    *OutData = new unsigned char[imageSize];
    memcpy(*OutData, dst.data, imageSize);
}

void threshHoldOverUnder(int nIndex, unsigned char** OutData, int minGV, int maxGV, int width, int height)
{
    Mat mask;
    Mat src = img[nIndex];
    resize(src, src, Size(width, height));

    Mat dst = src.clone();
    cvtColor(dst, dst, COLOR_GRAY2BGR);
    inRange(src, 0, minGV, mask);
    dst.setTo(Scalar(255, 0, 0), mask);
    inRange(src, maxGV, 255, mask);
    dst.setTo(Scalar(0, 255, 0), mask);
    int imageSize = dst.cols * dst.rows * dst.channels();
    *OutData = new unsigned char[imageSize];
    memcpy(*OutData, dst.data, imageSize);
}

int threshHoldOtsu(int nIndex, unsigned char** OutData, int* width, int* height)
{
    Mat src = img[nIndex];
    *width = src.cols;
    *height = src.rows;
    Mat dst = src.clone();
    double otsu_thresh_val = threshold(src, dst, 0, 255, THRESH_BINARY | THRESH_OTSU);
    threshold(src, dst, otsu_thresh_val, 255, THRESH_BINARY);

    int imageSize = dst.cols * dst.rows * dst.channels();
    *OutData = new unsigned char[imageSize];
    memcpy(*OutData, dst.data, imageSize);
    return (int)otsu_thresh_val;
}

void resetThres(int nIndex, unsigned char** OutData, int* width, int* height)
{
    Mat src = img[nIndex];
    *width = src.cols;
    *height = src.rows;

    int imageSize = src.cols * src.rows * src.channels();
    *OutData = new unsigned char[imageSize];
    memcpy(*OutData, src.data, imageSize);
}

int* BlobInform(int nIndex, int minGV, int maxGV, int* count, unsigned char** OutData, int* width, int* height, int classify, int lowValue, int highValue)
{
    Mat src = img[nIndex];
    Mat dst = src.clone();
    Mat MtOutput;
    cvtColor(src, MtOutput, COLOR_GRAY2BGR);

    inRange(dst, minGV, maxGV, dst);
    bitwise_not(dst, dst);

    dilate(dst, dst, Mat::ones(Size(5, 5), CV_8UC1));
    erode(dst, dst, Mat::ones(Size(5, 5), CV_8UC1));

    vector<vector<Point>> contours;	//윤곽선의 각 점의 좌표 저장
    findContours(dst, contours, RETR_TREE, CHAIN_APPROX_NONE);

    int nCount = 0;
    int* nValues = new int[contours.size() * 7] {0};

    for (int i = 0; i < contours.size(); i++)
    {
        int length = (int)arcLength(contours[i], true);
        if(classify == Length)
        {
            if(highValue == -1){ if ((length < lowValue)) { continue; } }
            else { if ((length < lowValue) || (length > highValue)) { continue; } }
        }
        if ((length < 10) || (length > (src.rows + src.cols) * 2 - 40)) { continue; } 


        Mat mask = Mat::zeros(dst.size(), CV_8UC1);
        drawContours(mask, contours, i, Scalar(255), FILLED);

        // Blob 영역에 대한 최대값, 최소값, 평균값 계산
        double minVal, maxVal;
        Point minPos, maxPos;
        Scalar Mean, stdDev;
        minMaxLoc(src, &minVal, &maxVal, &minPos, &maxPos, mask);
        Mean = mean(src, mask);

        if (classify == Min) { if ((minVal < lowValue) || (minVal > highValue)) { continue; } }
        if (classify == Max) { if ((maxVal < lowValue) || (maxVal > highValue)) { continue; } }
        if (classify == Avg) { if ((Mean[0] < lowValue) || (Mean[0] > highValue)) { continue; } }

        RotatedRect rot_rect = minAreaRect(contours[i]);
        Point2f pts[4];
        rot_rect.points(pts);

        if (rot_rect.angle <= -45) { rot_rect.angle += 90; }
        else if (rot_rect.angle > 45) { rot_rect.angle -= 90; }
        if (classify == Angle) { if ((rot_rect.angle < lowValue) || (rot_rect.angle > highValue)) { continue; } }

        // Blob의 모멘트 계산
        Moments moment = moments(contours[i]);
        // Contour의 모멘트를 사용하여 무게중심점 계산
        Point center(static_cast<int>(moment.m10 / moment.m00), static_cast<int>(moment.m01 / moment.m00));

        double minLength = 500, maxLength = 0;
        Point2f minstartPoint, minendPoint, maxstartPoint, maxendPoint;
        int s = (int)(contours[i].size() - 1);
        for (size_t k = 0; k < contours[i].size(); ++k)
        {
            Point2f startPoint = contours[i][k];
            Point2f point;

            for (size_t j = k + 1; j < contours[i].size(); ++j)
            {
                double len = norm(contours[i][k] - contours[i][j]);
                if (classify == LongSide) 
                {
                    if (highValue == -1) { if ((len < lowValue)) { continue; } }
                    else { if ((len < lowValue) || (len > highValue)) { continue; } }
                }
                if (len > maxLength)
                {
                    bool IsWhite = true;
                    maxendPoint = contours[i][j];

                    LineIterator it(mask, startPoint, maxendPoint);
                    for (int i = 0; i < it.count; i++, ++it)
                    {
                        if (mask.at<uchar>(it.pos()) != 255) { IsWhite = false; }
                    }

                    if (IsWhite) { maxLength = len; maxstartPoint = startPoint; }
                }
            }

            if (k < (s / 2)) { point = contours[i][s / 2 + k]; }
            else { point = contours[i][k - s / 2]; }

            double len2 = norm(contours[i][k] - Point(point));
            if (classify == ShortSide) 
            {
                if (highValue == -1) { if ((len2 < lowValue)) { continue; } }
                else { if ((len2 < lowValue) || (len2 > highValue)) { continue; } }
            }
            if (len2 < minLength)
            {
                minLength = len2;
                minstartPoint = startPoint;
                minendPoint = point;
            }
        }
        
        nValues[nCount * 7] = (int)Mean[0];
        nValues[nCount * 7 + 1] = (int)minVal;
        nValues[nCount * 7 + 2] = (int)maxVal;
        nValues[nCount * 7 + 3] = length;
        nValues[nCount * 7 + 4] = (int)maxLength;
        nValues[nCount * 7 + 5] = (int)minLength;
        nValues[nCount * 7 + 6] = (int)rot_rect.angle;

        nCount++;
        putText(MtOutput, to_string(nCount), Point((center.x - 5), (center.y + 5)), FONT_HERSHEY_SIMPLEX, 0.6, Scalar(0, 0, 255));
    }
    int imageSize = MtOutput.cols * MtOutput.rows * MtOutput.channels();
    *OutData = new unsigned char[imageSize];
    memcpy(*OutData, MtOutput.data, imageSize);

    *count = nCount * 7;
    *width = MtOutput.cols;
    *height = MtOutput.rows;
    return nValues;
}

//int* BlobInform(int nIndex, int minGV, int maxGV, int* count, unsigned char** OutData, int* width, int* height)
//{
//    Mat src = img[nIndex];
//    Mat dst = src.clone();
//    Mat MtOutput;
//    cvtColor(src, MtOutput, COLOR_GRAY2BGR);
//
//    inRange(dst, minGV, maxGV, dst);
//    bitwise_not(dst, dst);
//
//    dilate(dst, dst, Mat::ones(Size(5, 5), CV_8UC1));
//    erode(dst, dst, Mat::ones(Size(5, 5), CV_8UC1));
//
//    vector<vector<Point>> contours;	//윤곽선의 각 점의 좌표 저장
//    findContours(dst, contours, RETR_TREE, CHAIN_APPROX_NONE);
//
//    int nCount = 0;
//    int* nValues = new int[contours.size() * 7] {0};
//
//    for (int i = 0; i < contours.size(); i++)
//    {
//        int length = (int)arcLength(contours[i], true);
//        if ((length < 30) || (length > (src.rows + src.cols) * 2 - 40)) { continue; }
//
//
//        Mat mask = Mat::zeros(dst.size(), CV_8UC1);
//        drawContours(mask, contours, i, Scalar(255), FILLED);
//
//        // Blob 영역에 대한 최대값, 최소값, 평균값 계산
//        double minVal, maxVal;
//        Point minPos, maxPos;
//        Scalar Mean, stdDev;
//        minMaxLoc(src, &minVal, &maxVal, &minPos, &maxPos, mask);
//        Mean = mean(src, mask);
//
//        RotatedRect rot_rect = minAreaRect(contours[i]);
//        Point2f pts[4];
//        rot_rect.points(pts);
//
//        if (rot_rect.angle <= -45) { rot_rect.angle += 90; }
//        else if (rot_rect.angle > 45) { rot_rect.angle -= 90; }
//
//        // Blob의 모멘트 계산
//        Moments moment = moments(contours[i]);
//        // Contour의 모멘트를 사용하여 무게중심점 계산
//        Point center(static_cast<int>(moment.m10 / moment.m00), static_cast<int>(moment.m01 / moment.m00));
//
//        double minLength = 500, maxLength = 0;
//        Point2f minstartPoint, minendPoint, maxstartPoint, maxendPoint;
//        int s = (int)(contours[i].size() - 1);
//        for (size_t k = 0; k < contours[i].size(); ++k)
//        {
//            Point2f startPoint = contours[i][k];
//            Point2f point;
//
//            for (size_t j = k + 1; j < contours[i].size(); ++j)
//            {
//                double len = norm(contours[i][k] - contours[i][j]);
//                if (len > maxLength)
//                {
//                    bool IsWhite = true;
//                    maxendPoint = contours[i][j];
//
//                    LineIterator it(mask, startPoint, maxendPoint);
//                    for (int i = 0; i < it.count; i++, ++it)
//                    {
//                        if (mask.at<uchar>(it.pos()) != 255) { IsWhite = false; }
//                    }
//
//                    if (IsWhite) { maxLength = len; maxstartPoint = startPoint; }
//                }
//            }
//            
//            if (k < (s / 2)) { point = contours[i][s / 2 + k]; }
//            else { point = contours[i][k - s / 2]; }
//
//            double len2 = norm(contours[i][k] - Point(point));
//            if (len2 < minLength)
//            {
//                minLength = len2;
//                minstartPoint = startPoint;
//                minendPoint = point;
//            }
//        }
//
//        nValues[nCount * 7] = (int)Mean[0];
//        nValues[nCount * 7 + 1] = (int)minVal;
//        nValues[nCount * 7 + 2] = (int)maxVal;
//        nValues[nCount * 7 + 3] = length;
//        nValues[nCount * 7 + 4] = (int)maxLength;
//        nValues[nCount * 7 + 5] = (int)minLength;
//        nValues[nCount * 7 + 6] = (int)rot_rect.angle;
//
//        nCount++;
//        putText(MtOutput, to_string(nCount), Point((center.x - 5), (center.y + 5)), FONT_HERSHEY_SIMPLEX, 0.6, Scalar(0, 0, 255));
//    }
//    int imageSize = MtOutput.cols * MtOutput.rows * MtOutput.channels();
//    *OutData = new unsigned char[imageSize];
//    memcpy(*OutData, MtOutput.data, imageSize);
//
//    *count = nCount * 7;
//    *width = MtOutput.cols;
//    *height = MtOutput.rows;
//    return nValues;
//}

//Mat charTomat(unsigned char* imageData, int width, int height, int channels)
//{
//    Mat src(height, width, CV_MAKETYPE(CV_8U, channels));
//    if (channels == 1) { src.data = imageData; }
//    else if (channels == 3)
//    {
//        for (int y = 0; y < height; y++)
//        {
//            for (int x = 0; x < width; x++)
//            {
//                int step = y * width * 4 + x * 4;
//
//                src.at<Vec3b>(y, x)[0] = imageData[step];     // Blue
//                src.at<Vec3b>(y, x)[1] = imageData[step + 1]; // Green
//                src.at<Vec3b>(y, x)[2] = imageData[step + 2]; // Red
//            }
//        }
//    }
//    else if (channels == 4)
//    {
//        for (int y = 0; y < height; y++)
//        {
//            for (int x = 0; x < width; x++)
//            {
//                int step = y * width * 4 + x * 4;
//                src.at<Vec4b>(y, x)[0] = imageData[step];     // Blue
//                src.at<Vec4b>(y, x)[1] = imageData[step + 1]; // Green
//                src.at<Vec4b>(y, x)[2] = imageData[step + 2]; // Red
//                src.at<Vec4b>(y, x)[3] = imageData[step + 3]; // Alpha
//            }
//        }
//    }
//    return src;
//}
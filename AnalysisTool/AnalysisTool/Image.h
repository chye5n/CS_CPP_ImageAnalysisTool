#pragma once
#include "opencv2/opencv.hpp"
#include <comutil.h>

#define None 0
#define Avg 1
#define Min 2
#define Max 3
#define Length 4
#define LongSide 5
#define ShortSide 6
#define Angle 7

using namespace std;
using namespace cv;

extern "C"
{
	__declspec(dllexport) void ImageLoad(const char* imagePath, unsigned char** imageData, int* width, int* height, int* channel, int* index);
	__declspec(dllexport) void FreeData(unsigned char* imageData);
	__declspec(dllexport) void ImageBoxSize(int formWidth, int formHeight, int* width, int* height);
	__declspec(dllexport) void MagOrShr(int nIndex, unsigned char** OutData, int* newWidth, int* newHeight, double nRate);
	__declspec(dllexport) int* HistogramData(int nIndex, int rectX, int rectY, int width, int height, int channels, double nRate);
	__declspec(dllexport) int* HistogramRGBData(int nIndex, int rectX, int rectY, int width, int height, int channels, int color, double nRate);
	__declspec(dllexport) void FreeHistogramData(int* data);
	__declspec(dllexport) void threshHoldRed(int nIndex, unsigned char** OutData, int minGV, int maxGV, int width, int height);
	__declspec(dllexport) void threshHoldBW(int nIndex, unsigned char** OutData, int minGV, int maxGV, int width, int height);
	__declspec(dllexport) void threshHoldOverUnder(int nIndex, unsigned char** OutData, int minGV, int maxGV, int width, int height);
	__declspec(dllexport) int threshHoldOtsu(int nIndex, unsigned char** OutData, int* width, int* height);
	__declspec(dllexport) void resetThres(int nIndex, unsigned char** OutData, int* width, int* height);
	__declspec(dllexport) int* BlobInform(int nIndex, int minGV, int maxGV, int* count, unsigned char** OutData, int* width, int* height, int classify, int lowValue, int highValue);
	//__declspec(dllexport) int* BlobInform(int nIndex, int minGV, int maxGV, int* count, unsigned char** OutData, int* width, int*height);
	//__declspec(dllexport) int* ClassifyInform(int nIndex, int minGV, int maxGV, int* count, unsigned char** OutData, int* width, int* height, int classify, int lowValue, int highValue);
	//__declspec(dllexport) Mat charTomat(unsigned char* imageData, int width, int height, int channels);
};
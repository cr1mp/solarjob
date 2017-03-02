using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using AForge.Video.FFMPEG;

namespace ForFun
{
	class Program
	{
		static string[] imgs = {
				"SbU-PYfOs_4.jpg",
				"MO-Tybfq-qw.jpg",
				"CRk3cxWHoRs.jpg",
				"M8zLpTCKJrY.jpg",
				"C#.png",
				"images.jpg",
			};

		static int i = -1;

		static void Main(string[] args)
		{
			var neuro = new MyClass();

			Thread.Sleep(10000);

			VideoShow(neuro);

			//SlideShow(neuro);

			Console.ReadLine();
		}

		private static void VideoShow(MyClass neuro)
		{
			// create instance of video reader
			var reader = new VideoFileReader();
			// open video file
			reader.Open("LITTLE BIG - BIG DICK (2).mov");
			// read 100 video frames out of it
			for (int i = 0; i < int.MaxValue; i++)
			{
				Bitmap videoFrame = reader.ReadVideoFrame();
				if (videoFrame != null && i%10==0)
				{
					ShowBitmap(videoFrame,neuro);

					//videoFrame.Save(i + ".bmp");

					// dispose the frame when it is no longer required
					videoFrame.Dispose();
				}
				
				
			}
			reader.Close();
		}

		private static void SlideShow(MyClass neuro)
		{
			while (true)
			{
				var img = GetImg();

				ShowBitmap(img, neuro);

				//color = GetColor(color);

				Thread.Sleep(4000);
				Console.Clear();
			}
		}

		private static void ShowBitmap(Bitmap img, MyClass neuro)
		{
			for (int h = 0; h < img.Height; h++)
			{
				for (int w = 0; w < img.Width; w++)
				{
					Color pixel = img.GetPixel(w, h);

					var result = neuro.Network.Compute(new[]
					{
						GetDouble(pixel.R),
						GetDouble(pixel.G),
						GetDouble(pixel.B)
					});

					var color = Array.IndexOf(result, result.Max());

					Console.BackgroundColor = (ConsoleColor)color;
					Console.Write(" ");
				}
				Console.WriteLine();
			}
		}

		private static Bitmap GetImg()
		{
			if (i < imgs.Length - 1)
			{
				i++;
			}
			else
			{
				i = 0;
			}
			return new Bitmap($"Imgs/{imgs[i]}");
		}

		private static int GetColor(int color)
		{
			if (color < 15)
			{
				color++;
			}
			else
			{
				color = 0;
			}
			return color;
		}

		static double GetDouble(byte b)
		{
			return b / (double)255;
		}
	}
}

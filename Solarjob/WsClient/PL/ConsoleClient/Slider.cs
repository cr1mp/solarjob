using System;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace WsClient.PL.ConsoleClient
{
	public class Slider
	{

		private bool IsStopped;
		private readonly NeuralNetwork _network;
		private readonly string[] _imgs = {
			"1.png",
			"2.jpg",
			"3.jpg",
			"4.jpg",
			"5.jpg",
			"6.jpg",
		};
		private int i = -1;

		public Slider(NeuralNetwork network)
		{
			_network = network;
		}

		public void SlideShow()
		{

			while (!IsStopped)
			{
				var img = GetImg();

				ShowBitmap(img);

				Thread.Sleep(4000);
				Console.Clear();
			}
		}

		private Bitmap GetImg()
		{
			if (i < _imgs.Length - 1)
			{
				i++;
			}
			else
			{
				i = 0;
			}
			return new Bitmap($"Imgs/{_imgs[i]}");
		}

		private void ShowBitmap(Bitmap img)
		{
			for (int h = 0; h < img.Height; h++)
			{
				for (int w = 0; w < img.Width; w++)
				{
					Color pixel = img.GetPixel(w, h);

					var result = _network.Network.Compute(new[]
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

		double GetDouble(byte b)
		{
			return b / (double)255;
		}

		public void Stop()
		{
			IsStopped = true;
		}
	}
}
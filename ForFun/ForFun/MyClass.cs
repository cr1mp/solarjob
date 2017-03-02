using System;
using System.Collections;
using System.Globalization;
using System.IO;
using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ForFun
{
	class MyClass
	{
		private const string fn = "LearningNetwork";

		private int sampleCount = 0;
		private double[,] data = null;
		private int[] classes = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
		private int outputCount = 16;
		private int inputCount = 3;

		private int[] samplesPerClass = null;

		public MyClass()
		{
			if (File.Exists(fn))
			{
				Network = (ActivationNetwork)AForge.Neuro.Network.Load(fn);
			}
			else
			{
				data = new double[sampleCount, inputCount];
				Network = new ActivationNetwork(new SigmoidFunction(), inputCount, outputCount);
				Train();
			}
		}

		public ActivationNetwork Network { get; set; }

		void Train()
		{
			LoadTrainData();

			// prepare learning data
			double[][] input = new double[sampleCount][];
			double[][] output = new double[sampleCount][];

			for (int i = 0; i < sampleCount; i++)
			{
				input[i] = new double[inputCount];
				output[i] = new double[outputCount];

				// set input
				input[i][0] = data[i, 0];
				input[i][1] = data[i, 1];
				input[i][2] = data[i, 2];
				// set output
				output[i][classes[i]] = 1;
			}

			var teacher = new BackPropagationLearning(Network);
			while (true)
			{
				double error = teacher.RunEpoch(input, output);

				if (error == 0)
				{
					break;
				}
			}

			Network.Save(fn);
		}

		private void LoadTrainData()
		{

			// data file format:
			// R, G, B, class

			// load maximum 16 classes !

			// show file selection dialog

			StreamReader reader = null;

			// temp buffers (for 200 samples only)
			double[,] tempData = new double[200, 3];
			int[] tempClasses = new int[200];

			// min and max X values
			double minX = double.MaxValue;
			double maxX = double.MinValue;

			// samples count
			sampleCount = 0;
			// classes count
			outputCount = 0;
			samplesPerClass = new int[16];

			try
			{
				string str = null;

				// open selected file
				reader = File.OpenText("TrainingData.csv");

				// read the data
				while ((sampleCount < 200) && ((str = reader.ReadLine()) != null))
				{
					// split the string
					string[] strs = str.Split(';');
					if (strs.Length == 1)
						strs = str.Split(',');

					// check tokens count
					if (strs.Length != 4)
						throw new ApplicationException("Invalid file format");

					// parse tokens
					tempData[sampleCount, 0] = double.Parse(strs[0], CultureInfo.InvariantCulture);
					tempData[sampleCount, 1] = double.Parse(strs[1], CultureInfo.InvariantCulture);
					tempData[sampleCount, 2] = double.Parse(strs[2], CultureInfo.InvariantCulture);
					tempClasses[sampleCount] = int.Parse(strs[3]);

					// skip classes over 16, except only first 16 classes
					if (tempClasses[sampleCount] >= 16)
						continue;

					// count the amount of different classes
					if (tempClasses[sampleCount] >= outputCount)
						outputCount = tempClasses[sampleCount] + 1;
					// count samples per class
					samplesPerClass[tempClasses[sampleCount]]++;

					// search for min value
					if (tempData[sampleCount, 0] < minX)
						minX = tempData[sampleCount, 0];
					// search for max value
					if (tempData[sampleCount, 0] > maxX)
						maxX = tempData[sampleCount, 0];

					sampleCount++;
				}

				// allocate and set data
				data = Copy2d(tempData, sampleCount);
				//Array.Copy(tempData, 0, data, 0, sampleCount);
				classes = Copy(tempClasses, sampleCount);
				//Array.Copy(tempClasses,  classes,  sampleCount);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				// close file
				if (reader != null)
					reader.Close();
			}
		}

		static T[,] Copy2d<T>(T[,] array,int l)
		{
			T[,] newArray = new T[l, array.GetLength(1)];
			for (int i = 0; i < l; i++)
				for (int j = 0; j < array.GetLength(1); j++)
					newArray[i, j] = array[i, j];
			return newArray;
		}

		static T[] Copy<T>(T[] array, int l)
		{
			T[] newArray = new T[l];
			for (int i = 0; i < l; i++)
					newArray[i] = array[i];
			return newArray;
		}
	}
}
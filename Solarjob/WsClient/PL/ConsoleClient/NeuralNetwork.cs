using System;
using System.IO;
using AForge.Neuro;

namespace WsClient.PL.ConsoleClient
{
	public class NeuralNetwork
	{
		private const string fn = "LearningNetwork";

		public NeuralNetwork()
		{
			if (File.Exists(fn))
			{
				Network = (ActivationNetwork)AForge.Neuro.Network.Load(fn);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public ActivationNetwork Network { get; set; }
	}
}
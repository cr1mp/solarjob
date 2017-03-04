using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WsClient.PL.ConsoleClient
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ConsoleFont
	{
		public uint Index;
		public short SizeX, SizeY;
	}

	public static class ConsoleHelper
	{
		[DllImport("kernel32")]
		public static extern bool SetConsoleIcon(IntPtr hIcon);

		public static bool SetConsoleIcon(Icon icon)
		{
			return SetConsoleIcon(icon.Handle);
		}

		//[DllImport("kernel32")]
		//private static extern bool SetConsoleFont(IntPtr hOutput, uint index);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern int SetConsoleFont(IntPtr hOut,uint dwFontNum);

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetStdHandle(int dwType);

		private enum StdHandle
		{
			OutputHandle = -11
		}

		[DllImport("kernel32")]
		private static extern IntPtr GetStdHandle(StdHandle index);

		public static bool SetConsoleFont(uint index)
		{
			SetConsoleFont(GetStdHandle(StdHandle.OutputHandle), index);
			return true;
		}

		[DllImport("kernel32")]
		private static extern bool GetConsoleFontInfo(IntPtr hOutput, [MarshalAs(UnmanagedType.Bool)]bool bMaximize,
			uint count, [MarshalAs(UnmanagedType.LPArray), Out] ConsoleFont[] fonts);

		[DllImport("kernel32")]
		private static extern uint GetNumberOfConsoleFonts();

		public static uint ConsoleFontsCount
		{
			get
			{
				return GetNumberOfConsoleFonts();
			}
		}

		public static ConsoleFont[] ConsoleFonts
		{
			get
			{
				ConsoleFont[] fonts = new ConsoleFont[GetNumberOfConsoleFonts()];
				if (fonts.Length > 0)
					GetConsoleFontInfo(GetStdHandle(StdHandle.OutputHandle), false, (uint)fonts.Length, fonts);
				return fonts;
			}
		}

	}
}

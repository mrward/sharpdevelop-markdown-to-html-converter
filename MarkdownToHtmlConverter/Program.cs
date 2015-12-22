using System;

namespace MarkdownToHtmlConverter
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0) {
				PrintUsage();
			} else {
				RunConversion(args[0]);
			}
		}
		
		static void PrintUsage()
		{
			Console.WriteLine("MarkdownToHtmlConverter filename");
		}
		
		static void RunConversion(string fileName)
		{
			var converter = new Converter(fileName);
			string html = converter.ToHtml();
			Console.WriteLine(html);
		}
	}
}

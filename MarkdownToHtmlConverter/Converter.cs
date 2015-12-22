using System;
using System.Collections.Generic;
using System.IO;
using MarkdownSharp;

namespace MarkdownToHtmlConverter
{
	public class Converter
	{
		string markdownText;
		Markdown markdown = new Markdown(true);
		
		public Converter(string fileName)
		{
			markdownText = File.ReadAllText(fileName);
		}

		public string ToHtml()
		{
			string innerHtml = markdown.Transform(markdownText);
			string html = AddOuterHtml(innerHtml);
			return TweakForCommunityServerBlog(html);
		}
		
		string AddOuterHtml(string innerHtml)
		{
			return String.Format("<root>{0}</root>", innerHtml);
		}
		
		string TweakForCommunityServerBlog(string html)
		{
			try {
				var converter = new CommunityServerBlogConverter(html);
				return converter.Convert();
			} catch (Exception ex) {
				return ex.Message + "\r\n\r\n" + html;
			}
		}
	}
}

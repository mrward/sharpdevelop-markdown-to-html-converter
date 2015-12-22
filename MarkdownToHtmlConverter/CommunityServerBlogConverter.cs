// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.IO;
using System.Xml;

namespace MarkdownToHtmlConverter
{
	public class CommunityServerBlogConverter
	{
		XmlDocument doc = new XmlDocument();
			
		public CommunityServerBlogConverter(string html)
		{
			doc.XmlResolver = null;
			doc.LoadXml(html);
		}
		
		public string Convert()
		{
			XmlElement body = (XmlElement)doc.SelectSingleNode("//root");
			if (body != null) {
				foreach (XmlElement image in body.SelectNodes("//img")) {
					string imageFileName = image.GetAttribute("src");
					if (!String.IsNullOrEmpty(imageFileName)) {
						imageFileName = Path.GetFileNameWithoutExtension(imageFileName);
						string imageUrl = HtmlTemplate.BaseImageUrl + Path.ChangeExtension(imageFileName, ".aspx");
						image.SetAttribute("src", imageUrl);
					}
				}
				foreach (XmlElement anchor in body.SelectNodes("//a")) {
					string url = anchor.GetAttribute("href");
					if (!String.IsNullOrEmpty(url)) {
						if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)) {
							string webPageFileName = Path.GetFileNameWithoutExtension(url);
							url = HtmlTemplate.BaseUrl + webPageFileName + ".aspx";
							anchor.SetAttribute("href", url);
						}
					}
				}
				return body.InnerXml;
			} 
			return "No body element found.";
		}
	}
}

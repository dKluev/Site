using System;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.Utils;
using Specialist.Entities.Common.Const;

namespace Specialist.Entities.Common.Logic {
	public class TextWithInfoTags {
		private string[] _textParts
			= new string[0];

		public TextWithInfoTags(string text, 
			string formatTag = SimplePageConst.Menu) {
			if (text.IsEmpty())
				return;
			text = ProcessTags(text);
			_textParts = text.Split(new[] {formatTag},
				StringSplitOptions.RemoveEmptyEntries);
		}

		public bool HasTag
		{
			get { return _textParts.Length > 1; }
		}

		public string FirstPart {
			get { return GetPart(0); }
		}

		public string SecondPart {
			get { return GetPart(1); }
		}

		private string sendFormTo = @"\[SendFormTo=(.*?)\]";
		private string sendFormToEnd = "[/SendFormTo]";
		string ProcessTags(string str) {
			if (str.Contains(sendFormToEnd)) {
				str = Regex.Replace(str, sendFormTo, 
					"<form method='post' action='/page/sendformto'><input type='hidden' name='sendformemail' value='$1'/>");
				str = str.Replace(sendFormToEnd,  "<input type='submit' value='Отправить'/></form>");
			}
			return str;
		}

		private string GetPart(int index) {
			if (_textParts.Length > index)
				return _textParts[index];
			return string.Empty;
		}
	}
}
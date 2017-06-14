using System.IO;
using System.Text;

namespace Specialist.Web.Common.Mvc.ActionResults {
	public class StringWriterWithEncoding : StringWriter
{
Encoding encoding;

public StringWriterWithEncoding (StringBuilder builder, Encoding encoding)
:base(builder)
{
this.encoding = encoding;
}

public override Encoding Encoding
{
get { return encoding; }
}
}
}
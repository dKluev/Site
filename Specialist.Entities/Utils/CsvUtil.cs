using System;
using System.Collections.Generic;
using System.Text;

namespace Specialist.Entities.Utils {
	public static class CsvUtil
{
    public static string Escape( string s )
    {
       // if ( s.Contains( QUOTE ) )
	    s = s ?? string.Empty;
        s = s.Replace( QUOTE, ESCAPED_QUOTE );

//        if ( s.IndexOfAny( CHARACTERS_THAT_MUST_BE_QUOTED ) > -1 )
        s = QUOTE + s + QUOTE;

        return s;
    }

	public static string Render(IEnumerable<IEnumerable<string>> strings, string separator = ";") {
		var builder = new StringBuilder();
		foreach (var stringList in strings) {
			foreach (var s in stringList) {
				builder.Append(Escape(s)).Append(separator);
			}
			builder.Append(Environment.NewLine);
		}
		return builder.ToString();
	}

    public static string Unescape( string s )
    {
        if ( s.StartsWith( QUOTE ) && s.EndsWith( QUOTE ) )
        {
            s = s.Substring( 1, s.Length - 2 );

            if ( s.Contains( ESCAPED_QUOTE ) )
                s = s.Replace( ESCAPED_QUOTE, QUOTE );
        }

        return s;
    }


    private const string QUOTE = "\"";
    private const string ESCAPED_QUOTE = "\"\"";
  //  private static char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };
}

}
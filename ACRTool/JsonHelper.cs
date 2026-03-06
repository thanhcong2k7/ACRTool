/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 06-Mar-26
 * Time: 3:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;

namespace ACRTool
{
	/// <summary>
	/// Description of JsonHelper.
	/// </summary>
	
	public static class JsonHelper
	{
	    /// <summary>
	    /// Formats a JSON string with standard indentation and newlines.
	    /// Compatible with .NET Framework 4.0+.
	    /// </summary>
	    /// <param name="json">The input JSON string.</param>
	    /// <param name="indentation">The indentation string (default is 4 spaces).</param>
	    /// <returns>A formatted JSON string.</returns>
	    public static string Beautify(string json, string indentation = "    ")
	    {
	        if (string.IsNullOrEmpty(json))
	            return string.Empty;
	
	        var sb = new StringBuilder();
	        int indentLevel = 0;
	        bool inQuote = false;
	
	        for (int i = 0; i < json.Length; i++)
	        {
	            char ch = json[i];
	
	            // 1. Handle Strings (ignore formatting inside quotes)
	            if (inQuote)
	            {
	                sb.Append(ch);
	                // Handle escaped characters (e.g. \" or \\)
	                if (ch == '\\' && i < json.Length - 1)
	                {
	                    sb.Append(json[++i]); // Append the escaped character immediately
	                }
	                else if (ch == '"')
	                {
	                    inQuote = false;
	                }
	                continue;
	            }
	
	            // 2. Start String
	            if (ch == '"')
	            {
	                inQuote = true;
	                sb.Append(ch);
	                continue;
	            }
	
	            // 3. Handle Formatting Characters
	            switch (ch)
	            {
	                case '{':
	                case '[':
	                    sb.Append(ch);
	                    sb.AppendLine();
	                    indentLevel++;
	                    AppendIndent(sb, indentLevel, indentation);
	                    break;
	
	                case '}':
	                case ']':
	                    sb.AppendLine();
	                    indentLevel = Math.Max(0, indentLevel - 1);
	                    AppendIndent(sb, indentLevel, indentation);
	                    sb.Append(ch);
	                    break;
	
	                case ',':
	                    sb.Append(ch);
	                    sb.AppendLine();
	                    AppendIndent(sb, indentLevel, indentation);
	                    break;
	
	                case ':':
	                    sb.Append(ch);
	                    sb.Append(" "); // Add a space after the colon for readability
	                    break;
	
	                default:
	                    // Skip existing whitespace if it's outside of quotes
	                    // This allows the function to "re-beautify" already formatted JSON
	                    if (!char.IsWhiteSpace(ch))
	                    {
	                        sb.Append(ch);
	                    }
	                    break;
	            }
	        }
	
	        return sb.ToString();
	    }
	
	    // Helper to append the indentation string N times
	    private static void AppendIndent(StringBuilder sb, int level, string indentStr)
	    {
	        for (int i = 0; i < level; i++)
	        {
	            sb.Append(indentStr);
	        }
	    }
	}
}

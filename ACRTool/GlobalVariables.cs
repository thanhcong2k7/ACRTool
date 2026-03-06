/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 06-Mar-26
 * Time: 5:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ACRTool
{
	/// <summary>
	/// Description of GlobalVariables.
	/// </summary>
	public static class GlobalVariables
	{
		public static IniHelper ini = new IniHelper("config.ini");
		public static string host = "";
		public static string accesskey = "";
		public static string accesssecret = "";
	}
}

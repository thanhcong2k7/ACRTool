/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 06-Mar-26
 * Time: 5:52 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ACRTool
{
	/// <summary>
	/// Description of IniHelper.
	/// </summary>
    public class IniHelper
    {
        // Property to hold the file path
        public string FilePath { get; private set; }

        // Import the Write function from kernel32.dll
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        // Import the Read function from kernel32.dll
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Initializes a new instance of the IniHelper class.
        /// If the file does not exist, it will be created automatically when you first Write to it.
        /// </summary>
        /// <param name="path">The full path to the .ini file.</param>
        public IniHelper(string path)
        {
            // Ensure full path is used to avoid issues with relative paths in WinAPI
            FilePath = new FileInfo(path).FullName;
        }

        /// <summary>
        /// Writes a value to the INI file.
        /// </summary>
        /// <param name="section">The section name (e.g. "Settings").</param>
        /// <param name="key">The key name (e.g. "Volume").</param>
        /// <param name="value">The value to store.</param>
        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, FilePath);
        }

        /// <summary>
        /// Reads a value from the INI file.
        /// </summary>
        /// <param name="section">The section name.</param>
        /// <param name="key">The key name.</param>
        /// <param name="defaultValue">Value to return if key is missing.</param>
        /// <returns>The value from the INI file or the default value.</returns>
        public string Read(string section, string key, string defaultValue = "")
        {
            var retVal = new StringBuilder(2048); // Buffer size
            GetPrivateProfileString(section, key, defaultValue, retVal, 2048, FilePath);
            return retVal.ToString();
        }

        /// <summary>
        /// Deletes a specific key from a section.
        /// </summary>
        public void DeleteKey(string section, string key)
        {
            Write(section, key, null);
        }

        /// <summary>
        /// Deletes an entire section.
        /// </summary>
        public void DeleteSection(string section)
        {
            Write(section, null, null);
        }

        /// <summary>
        /// Checks if a key exists in a section.
        /// </summary>
        public bool KeyExists(string section, string key)
        {
            return Read(section, key).Length > 0;
        }
    }
}

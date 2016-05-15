using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;


namespace SerialTerminal
{
    internal static class Utilities
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }



        /// <summary>
        /// Replaces hexa tags in format <0x0D>, <0xFF>, <0xB1> etc with char by its value
        /// </summary>
        /// <param name="textToParse"></param>
        /// <returns></returns>
        public static string ReplaceHexTagsByItsValue(string textToParse)
        {
            return Regex.Replace(textToParse, "<0x..>", new MatchEvaluator(MatchToHexConverter));
        }



        private static string MatchToHexConverter(Match m)
        {
            byte HexValue = 0;
            string HexString = m.ToString().Substring(3, 2);
            try
            {
                HexValue = Convert.ToByte(HexString, 16);
            }
            catch
            {
            }

            return Convert.ToString(Convert.ToChar(HexValue));
        }



        /// <summary>
        /// Replaces all occurances of non printable characters by ascii hex tag <0x..> (dots represent actual hex value of non printable character.
        /// </summary>
        /// <param name="textToParse"></param>
        /// <returns></returns>
        public static string ReplaceNonPrintableCharsByHexTag(string textToParse)
        {
            StringBuilder ModifiedText = new StringBuilder(4096);

            foreach (char c in textToParse)
            {
                //If character is printable
                if (c > 0x20 && c < 0x7F)
                {
                    //just print it
                    ModifiedText.Append(c);
                }
                else
                {
                    //else char is non printable, print it as <0x??>
                    ModifiedText.Append(string.Format("<0x{0:X2}>", Convert.ToUInt32(c)));
                }
            }
            return ModifiedText.ToString();
        }




        public static string SelectFile()
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.RestoreDirectory = true;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                return FileDialog.FileName;
            }
            else
            {
                return null;
            }
        }


        public static string GetFileName(string filePath)
        {
            string[] Tokens = filePath.Split('\\');
            return Tokens[Tokens.Length - 1];    //last token contains name and extension
        }


        public static void ClearFileContent(string filePath)
        {
            try
            {
                FileStream OutputFile = File.Open(filePath, FileMode.Create);
                OutputFile.Flush();
                OutputFile.Close();
            }
            catch
            {
                throw new ApplicationException("Invalid log file path has been specified");
            }
        }
    }
}

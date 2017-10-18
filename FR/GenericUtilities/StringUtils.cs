using System;
using System.Collections.Generic;
using System.Text;

namespace FR.GenericUtilities
{
    public class StringUtils
    {
        public enum CompactStringBehavior
        {
            removeEnd = 1,
            removeMiddle = 2
        }

        public static string CompactString(string value, int maxLength, CompactStringBehavior behavior)
        {
            return CompactString(value, maxLength, behavior, "...");
        }

        public static string CompactString(string value, int maxLength, CompactStringBehavior behavior, string snipIndicator)
        {
            string compacted = string.Empty;
            int lenghtAdjustment = 0;
            snipIndicator = " " + snipIndicator;
            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException("maxLength", maxLength, "Value must be greater than zero (0).");
            }
            if (behavior == 0)
            {
                throw new ArgumentException("behavior must contain a value other than 0.", "behavior");
            }
            if (value.Length <= maxLength)
            {
                return value;
            }
            if (behavior == CompactStringBehavior.removeEnd)
            {
                compacted = value.Substring(0, maxLength - snipIndicator.Length) + snipIndicator;
            }
            else if (behavior == CompactStringBehavior.removeMiddle)
            {
                if (Math.IEEERemainder(maxLength, 2) != 0)
                {
                    lenghtAdjustment += 1;
                }
                compacted = value.Substring(0, System.Convert.ToInt32(Math.Floor(maxLength / 2.0)) - snipIndicator.Length - lenghtAdjustment) + snipIndicator;
                compacted += value.Substring(value.Length - System.Convert.ToInt32(Math.Floor(maxLength / 2.0)), System.Convert.ToInt32(Math.Floor(maxLength / 2.0)));
            }
            else
            {
                throw new ArgumentException(string.Format("Unrecognized CompactStringBehavior '{0}'.", behavior.ToString()));
            }
            if (compacted.Length > maxLength)
            {
                throw new Exception(string.Format("Compact of string resulted in a string exceeding "
                    + "the requested maximum length. Expected length: {1}, actual: {2}. "
                    + "String value: \"{0}\".",
                    value,
                    maxLength.ToString(),
                    compacted.Length.ToString()));
            }
            return compacted;
        }

        public static string ReplaceAll(string input, char target, List<char> filter)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                if (filter.Contains(input[i]))
                {
                    sb.Append(input[i]);
                }
                else
                {
                    sb.Append(target);
                }
            }

            return sb.ToString();
        }

        public static string HidePhoneNo(string input, char target)
        {
            //input  = 012 234 1234 
            //output = 012XXX2324
            string strNoWhiteSpace = RemoveWhitespace(input);
            StringBuilder sb = new StringBuilder(strNoWhiteSpace.Length);
            int totalLength = sb.Length;
            string first3Char = strNoWhiteSpace.Substring(0, 3);
            string last4Char = strNoWhiteSpace.Substring((strNoWhiteSpace.Length - 4), 4);

            if (strNoWhiteSpace.Length > 7)
            {
                for (int i = 0; i < (strNoWhiteSpace.Length - 7); i++)
                {
                    sb.Append(target);
                }
            }


            return first3Char + sb.ToString() + last4Char;
        }

        public static string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}

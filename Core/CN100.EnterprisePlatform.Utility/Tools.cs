using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using log4net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CN100.EnterprisePlatform.Utility
{
    public class Tools
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// To convert a string to DateTime,
        /// in order to put into db can use prepared statement or
        /// {DateTime}.ToString("yyyy/MM/dd")
        /// </summary>
        /// <param name="value">A string in dd/MM/yyyy</param>
        /// <returns>Converted DateTime</returns>
        public static object ToDate(string value, string[] from)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DateTimeFormatInfo dtfi = new CultureInfo("en-US", false).DateTimeFormat;
                dtfi.SetAllDateTimePatterns(from, 'd');
                return Convert.ToDateTime(value, dtfi);
            }
            return null;
        }

        /// <summary>
        /// To get the DateTime formate instead of string format
        /// </summary>
        /// <param name="value">The string that want to convert</param>
        /// <param name="from">String array of the initial DateTime format</param>
        /// <param name="db">If the DateTime used in database</param>
        /// <returns>Converted DateTime</returns>
        public static object ToDate(object value, string[] from, Boolean db)
        {
            if (db && value is DBNull)
                return null;

            if (value.GetType() == typeof(string) && !string.IsNullOrEmpty((string)value))
            {
                return ((DateTime)ToDate((string)value, from));
            }
            else if (value.GetType() == typeof(DateTime))
            {
                return ((DateTime)value);
            }

            if (db)
                return DBNull.Value;
            else
                return null;
        }

        /// <summary>
        /// To convert a string from [from] format to [to] format
        /// </summary>
        /// <param name="value">The string that want to convert</param>
        /// <param name="from">String array of the initial DateTime format</param>
        /// <param name="to">Target fromat</param>
        /// <param name="db">If the string used in database</param>
        /// <returns>Converted DataTime string</returns>
        public static object ToDateString(object value, string[] from, string to, Boolean db)
        {
            if (db && value is DBNull)
                return null;
            if (value == null)
                if (db)
                    return DBNull.Value;
                else
                    return null;

            if (value.GetType() == typeof(string) && !string.IsNullOrEmpty((string)value))
                return ((DateTime)ToDate((string)value, from)).ToString(to, new CultureInfo("en-US", false).DateTimeFormat);
            else if (value.GetType() == typeof(DateTime))
                return ((DateTime)value).ToString(to, new CultureInfo("en-US", false).DateTimeFormat);

            if (db)
                return DBNull.Value;
            else
                return null;
        }

        /// <summary>
        /// To convert an ArrayList to string with ',' seperate
        /// </summary>
        /// <param name="value">ArrayList want to convert</param>
        /// <returns>A string with ',' seperate</returns>
        public static string ToString(ArrayList value)
        {
            string result = "";

            foreach (object obj in value)
            {
                result += Convert.ToString(obj) + ",";
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        /// <summary>
        /// To convert a DataSet into ArrayList
        /// </summary>
        /// <param name="set">DataSet want to convert</param>
        /// <param name="tableName">The table name, null -> the first table in the DataSet</param>
        /// <param name="colName">The colName of the record, null -> the first column in the DataRow</param>
        /// <returns>The converted ArrayList</returns>
        public static ArrayList DataSet2ArrayList(DataSet set, string tableName, string colName)
        {
            ArrayList list = new ArrayList();
            DataTable table = (string.IsNullOrEmpty(tableName)) ? set.Tables[0] : set.Tables[tableName];
            foreach (DataRow row in table.Rows)
            {
                list.Add((colName == null) ? row[0].ToString() : row[colName].ToString());
            }
            return list;
        }

        /// <summary>
        /// To convert a DataSet into array
        /// </summary>
        /// <param name="set">DataSet want to convert</param>
        /// <param name="tableName">The table name, null -> the first table in the DataSet</param>
        /// <param name="colName">The colName of the record, null -> the first column in the DataRow</param>
        /// <returns>The converted array</returns>
        public static T[] DataSet2Array<T>(DataSet set, string tableName, string colName)
        {
            return (T[])DataSet2ArrayList(set, tableName, colName).ToArray(typeof(T));
        }

        /// <summary>
        /// To convert t a DataSet into Hashtable
        /// </summary>
        /// <param name="set">DataSet want to convert</param>
        /// <param name="tableName">The table name, null -> the first table in the DataSet</param>
        /// <param name="key">The column name of the key, null -> the first column in the DataRow</param>
        /// <param name="value">The column name of the value, null -> the first column in the DataRow</param>
        /// <returns>The converted Hashtable</returns>
        public static Hashtable DataSet2Hashtable(DataSet set, string tableName, string key, string value)
        {
            Hashtable table = new Hashtable();
            DataTable dtable = (string.IsNullOrEmpty(tableName)) ? set.Tables[0] : set.Tables[tableName];
            foreach (DataRow row in dtable.Rows)
            {
                table.Add((key == null) ? row[0] : row[key], (value == null) ? row[0] : row[value]);
            }
            return table;
        }

        #region isDecimal
        /// <summary>
        /// To validate if a string is decimal
        /// </summary>
        /// <param name="theValue">The string which want to validate</param>
        /// <returns>True if the string can be change into decimal otherwise false</returns>
        public static bool IsDecimal(string theValue)
        {
            return IsDecimal(theValue, null, null);
        }

        /// <summary>
        /// To validate if a string can be change into decimal
        /// with the length of integer part and length of decimal part
        /// </summary>
        /// <param name="theValue">The string which want to validate</param>
        /// <param name="integerPart">The length of the integer part, null if no limit</param>
        /// <param name="decimalPart">The length of the deciaml part, null if no limit</param>
        /// <returns>True if pass validation, otherwise false</returns>
        public static bool IsDecimal(string theValue, int? integerPart, int? decimalPart)
        {
            try
            {
                Convert.ToDecimal(theValue);
                string[] temp = theValue.Split('.');
                if (decimalPart != null && temp.Length > 1)
                {
                    if (temp[1].Length > decimalPart)
                        return false;
                }
                if (integerPart != null)
                {
                    if (temp[0].Length > integerPart)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// To validate if a string can be change into integer
        /// </summary>
        /// <param name="theValue">The string which want to validate</param>
        /// <returns>True if pass validation, otherwise false</returns>
        public static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Append a list of objects or arrays to the given array
        /// </summary>
        /// <typeparam name="T">The type of array that you want to have</typeparam>
        /// <param name="array">The original array, null means create a new array</param>
        /// <param name="value">Objects that you want to append, 
        /// all the object that have different type than T will be ignored</param>
        /// <returns>Result array with type T, element is sort by the order you pass in</returns>
        public static T[] Append<T>(object[] array, params object[] value)
        {
            int length = 0;
            int index = 0;
            if (array != null)
                length = array.Length;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].GetType().BaseType == typeof(Array))
                    length += ((Array)value[i]).Length;
                else if (value[i].GetType() == typeof(T))
                    length++;
            }

            T[] result = new T[length];
            if (array != null)
            {
                array.CopyTo(result, index);
                index = array.Length;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].GetType().BaseType == typeof(Array))
                {
                    ((Array)value[i]).CopyTo(result, index);
                    index += ((Array)value[i]).Length;
                }
                else if (value[i].GetType() == typeof(T))
                {
                    result[index] = (T)value[i];
                    index++;
                }
            }

            return result;
        }

        /// <summary>
        /// To get the maximum value of a list of value
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="value">Value you have</param>
        /// <returns>Maximum value</returns>
        public static T Max<T>(params T[] value)
        {
            if ((typeof(T) != typeof(byte) && typeof(T) != typeof(decimal) &&
                typeof(T) != typeof(double) && typeof(T) != typeof(float) &&
                typeof(T) != typeof(int) && typeof(T) != typeof(long) &&
                typeof(T) != typeof(sbyte) && typeof(T) != typeof(short) &&
                typeof(T) != typeof(ulong) && typeof(T) != typeof(uint) &&
                typeof(T) != typeof(ushort)) || value == null || value.Length == 0)
                throw new ArgumentException();

            if (value.Length == 1)
                return value[0];

            T max = value[0];
            for (int i = 1; i < value.Length; i++)
            {
                #region Compare
                if (typeof(T) == typeof(byte))
                {
                    if (Math.Max((byte)Convert.ChangeType(max, typeof(T)), (byte)Convert.ChangeType(value[i], typeof(T))) == (byte)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(decimal))
                {
                    if (Math.Max((decimal)Convert.ChangeType(max, typeof(T)), (decimal)Convert.ChangeType(value[i], typeof(T))) == (decimal)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(double))
                {
                    if (Math.Max((double)Convert.ChangeType(max, typeof(T)), (double)Convert.ChangeType(value[i], typeof(T))) == (double)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(float))
                {
                    if (Math.Max((float)Convert.ChangeType(max, typeof(T)), (float)Convert.ChangeType(value[i], typeof(T))) == (float)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(int))
                {
                    if (Math.Max((int)Convert.ChangeType(max, typeof(T)), (int)Convert.ChangeType(value[i], typeof(T))) == (int)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(long))
                {
                    if (Math.Max((long)Convert.ChangeType(max, typeof(T)), (long)Convert.ChangeType(value[i], typeof(T))) == (long)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(sbyte))
                {
                    if (Math.Max((sbyte)Convert.ChangeType(max, typeof(T)), (sbyte)Convert.ChangeType(value[i], typeof(T))) == (sbyte)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(short))
                {
                    if (Math.Max((short)Convert.ChangeType(max, typeof(T)), (short)Convert.ChangeType(value[i], typeof(T))) == (short)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(uint))
                {
                    if (Math.Max((uint)Convert.ChangeType(max, typeof(T)), (uint)Convert.ChangeType(value[i], typeof(T))) == (uint)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(ulong))
                {
                    if (Math.Max((ulong)Convert.ChangeType(max, typeof(T)), (ulong)Convert.ChangeType(value[i], typeof(T))) == (ulong)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else if (typeof(T) == typeof(ushort))
                {
                    if (Math.Max((ushort)Convert.ChangeType(max, typeof(T)), (ushort)Convert.ChangeType(value[i], typeof(T))) == (ushort)Convert.ChangeType(value[i], typeof(T)))
                        max = value[i];
                }
                else
                    throw new ArgumentException();
                #endregion
            }

            return max;
        }

        /// <summary>
        /// To get the minimum value of a list of value
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="value">Value you have</param>
        /// <returns>Minimum value</returns>
        public static T Min<T>(params T[] value)
        {
            if ((typeof(T) != typeof(byte) && typeof(T) != typeof(decimal) &&
                typeof(T) != typeof(double) && typeof(T) != typeof(float) &&
                typeof(T) != typeof(int) && typeof(T) != typeof(long) &&
                typeof(T) != typeof(sbyte) && typeof(T) != typeof(short) &&
                typeof(T) != typeof(ulong) && typeof(T) != typeof(uint) &&
                typeof(T) != typeof(ushort)) || value == null || value.Length == 0)
                throw new ArgumentException();

            if (value.Length == 1)
                return value[0];

            T min = value[0];
            for (int i = 1; i < value.Length; i++)
            {
                #region Compare
                if (typeof(T) == typeof(byte))
                {
                    if (Math.Min((byte)Convert.ChangeType(min, typeof(T)), (byte)Convert.ChangeType(value[i], typeof(T))) == (byte)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(decimal))
                {
                    if (Math.Min((decimal)Convert.ChangeType(min, typeof(T)), (decimal)Convert.ChangeType(value[i], typeof(T))) == (decimal)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(double))
                {
                    if (Math.Min((double)Convert.ChangeType(min, typeof(T)), (double)Convert.ChangeType(value[i], typeof(T))) == (double)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(float))
                {
                    if (Math.Min((float)Convert.ChangeType(min, typeof(T)), (float)Convert.ChangeType(value[i], typeof(T))) == (float)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(int))
                {
                    if (Math.Min((int)Convert.ChangeType(min, typeof(T)), (int)Convert.ChangeType(value[i], typeof(T))) == (int)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(long))
                {
                    if (Math.Min((long)Convert.ChangeType(min, typeof(T)), (long)Convert.ChangeType(value[i], typeof(T))) == (long)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(sbyte))
                {
                    if (Math.Min((sbyte)Convert.ChangeType(min, typeof(T)), (sbyte)Convert.ChangeType(value[i], typeof(T))) == (sbyte)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(short))
                {
                    if (Math.Min((short)Convert.ChangeType(min, typeof(T)), (short)Convert.ChangeType(value[i], typeof(T))) == (short)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(uint))
                {
                    if (Math.Min((uint)Convert.ChangeType(min, typeof(T)), (uint)Convert.ChangeType(value[i], typeof(T))) == (uint)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(ulong))
                {
                    if (Math.Min((ulong)Convert.ChangeType(min, typeof(T)), (ulong)Convert.ChangeType(value[i], typeof(T))) == (ulong)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else if (typeof(T) == typeof(ushort))
                {
                    if (Math.Min((ushort)Convert.ChangeType(min, typeof(T)), (ushort)Convert.ChangeType(value[i], typeof(T))) == (ushort)Convert.ChangeType(value[i], typeof(T)))
                        min = value[i];
                }
                else
                    throw new ArgumentException();
                #endregion
            }

            return min;
        }

        /// <summary>
        /// To encrypt an string by using MD5 32-byte encryption
        /// </summary>
        /// <param name="value">The string want to encrypt</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string value)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }

        #region add tools for soon
        /// <summary>
        /// Check Text Decimal format
        /// </summary>
        /// <param name="textString">pass you vaildate string</param>
        /// <param name="Long">max length</param>
        /// <param name="dot">after dot max length</param>
        /// <returns></returns>
        public string CheckTextDecimal(string textString, int Long, int dot, double minValue)
        {


            if (IsNumeric(textString) == false)
            {
                return "Please enter a numeric value.";
            }
            else if (IsNumeric(textString) == true)
            {
                string[] value = textString.Split('.');
                if (value[0].Length == textString.Length)
                {
                    double max = 10;
                    for (int i = 0; i < Long; i++)
                    {
                        if (i == 0)
                        {
                            max = 10;
                        }
                        else
                        {
                            max *= max;
                        }
                    }
                    max = max - 1;
                    if (Convert.ToDouble(textString) > max)
                    {
                        return "you input is to large,Please between 0~" + max.ToString();
                    }
                    else
                    {
                        return "true";
                    }

                }
                else
                {
                    if ((value[0].Length + value[1].Length) > Long)
                    {
                        return "you input string is to long , please between 0~" + Long.ToString();
                    }
                    else
                    {
                        if (value[1].Length > dot)
                        {
                            return "you input decimal  is to long , please between 0~" + dot.ToString();
                        }
                        else
                        {
                            double max = 10;
                            for (int i = 0; i < Long; i++)
                            {
                                if (i == 0)
                                {
                                    max = 10;
                                }
                                else
                                {
                                    max *= max;
                                }
                            }
                            max = max - 1;
                            if (Convert.ToDouble(textString) > max)
                            {
                                return "you input is to large,Please between 0~" + max.ToString();
                            }
                            else if (Convert.ToDouble(textString) <= minValue)
                            {
                                return "Please enter a value greater than " + minValue.ToString();
                            }
                            else
                            {
                                return "true";
                            }

                        }

                    }
                }
            }

            return "true";
        }

        private bool IsNumeric(string sStr)
        {
            bool bReturnValue = true;
            if (sStr == null || sStr.Length == 0)
            {
                bReturnValue = false;
            }
            else
            {
                foreach (char c in sStr)
                {
                    if (!Char.IsNumber(c))
                    {
                        if (c != '.')
                        {
                            bReturnValue = false;
                            break;
                        }
                    }
                }
            }
            return bReturnValue;
        }
        /// <summary>
        /// check the date is exist
        /// </summary>
        /// <param name="year">year,pls format is yyyy</param>
        /// <param name="month">month,please is mm</param>
        /// <param name="day">day,please is day</param>
        /// <returns></returns>
        public bool CheckDataIsExist(int year, int month, int day)
        {
            if (month > 12 || day > 31)
            {
                return false;
            }
            else
            {
                if (month == 2 && day == 29)
                {
                    if (DateTime.IsLeapYear(year))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (month == 2 && day == 30)
                {
                    return false;
                }
                else
                {
                    if ((month == 2 || month == 4 || month == 6 || month == 9 || month == 11) && day == 31)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        /// <summary>
        /// User number to get the month， example input 13 retrun 1 ,15 return 3
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public int Month(int month)
        {
            if (month % 12 == 0)
            {
                return 12;
            }
            else
            {
                return month % 12;
            }

        }

        /// <summary>
        /// Calculated to increase the number of months to get an increase of the number of years
        /// </summary>
        /// <param name="month">Current month</param>
        /// <param name="add_month">Increase the number of months</param>
        /// <param name="year">Current Year</param>
        /// <returns>The final year of</returns>
        public int Year(int month, int add_month, int year)
        {
            int temp = (month + add_month) % 12;
            int temp1 = (month + add_month) / 12;
            if (temp == 0)
            {
                return year;
            }
            else if (temp == 0 && temp1 == 1)
            {
                return year;
            }
            else
                return (year + temp1);
        }



        #endregion

        /// <summary>
        /// Remove the special that potentially occur error to normal HTML code 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The string that involve HTML code</returns>
        public static string Encoding(string value)
        {
            string gt = "&#62;";
            string lt = "&#60;";
            string slash = "&#47;";
            string asterisk = "&#42;";
            string underscore = "&#95;";
            string colon = "&#58;";

            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("<", lt);
                value = value.Replace(">", gt);
                value = value.Replace("/", slash);
                value = value.Replace("*", asterisk);
                value = value.Replace("_", underscore);
                value = value.Replace(":", colon);
            }
            return value;
        }
        
        #region encypt
        public static string EncrytedString(string str)
        {
            byte[] _encryted = System.Text.Encoding.Unicode.GetBytes(str);
            string s = Convert.ToBase64String(_encryted);
            return s;
        }

        public static string DecrytedString(string str)
        {
            byte[] _decryted = Convert.FromBase64String(str);
            string s = System.Text.Encoding.Unicode.GetString(_decryted, 0, _decryted.Length);
            return s;
        }
        #endregion

        public static string CheckEmptyString(string _obj)
        {
            if (string.IsNullOrEmpty(_obj))
                return string.Empty;
            else
                return _obj;
        }

        public static double ToDouble(object value)
        {
            if (value == null || value.ToString() == "")
                return 0;
            else
                return Convert.ToDouble(value);
        }

        public static bool IsValidEmail(string strEmail)
        {
            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(strEmail);
        }

        public static string IsDBNull_string(object obj)
        {
            if (obj == DBNull.Value)
                return string.Empty;
            else if (obj == null)
                return string.Empty;
            else
                return obj.ToString();
        }

        public static string GeneratePassword(int intLength)
        {
            string PW = "";
            char c;
            for (int i = 0; i < intLength; i++)
            {
                Random RandNum = new Random();
                int MyRandomNumber;
                do
                {
                    do
                    {
                        MyRandomNumber = RandNum.Next(48, 122);
                    } while ((MyRandomNumber > 57 && MyRandomNumber < 65) || (MyRandomNumber > 90 && MyRandomNumber < 97));

                    c = (Char)MyRandomNumber;
                } while (PW.Contains(c.ToString()));
                PW = PW.Insert(PW.Length, c.ToString());
            }
            return PW;
        }

        public static byte[] ConvertToCSharpBytesFromJavaBytesString(string strJavaBytesStringSplitByBlankSpace)
        {

            if (string.IsNullOrEmpty(strJavaBytesStringSplitByBlankSpace))
                return null;

            byte[] byteRtn = null;

            string[] strJavaBytes = strJavaBytesStringSplitByBlankSpace.Split(' ');

            byteRtn = new byte[strJavaBytes.Length];

            int intTry;

            int i = 0;

            foreach (string str in strJavaBytes)
            {
                if (!int.TryParse(str, out intTry))
                {
                    throw new Exception("Convert From Java Bytes String Failed!");
                }
                else
                {
                    int temp;

                    temp = Convert.ToInt32(str);

                    temp = (temp < 0) ? (temp + 256) : temp;

                    byteRtn.SetValue((byte)temp, i);

                    i++;
                }
            }

            return byteRtn;

        }

        public static string ConvertToBase64String(string strValue)
        {
            string strRtn = null;

            if (strValue == null)
                return strRtn;

            byte[] byteRtn = null;

            byteRtn = System.Text.Encoding.UTF8.GetBytes(strValue);

            strRtn = Convert.ToBase64String(byteRtn);

            return strRtn;
        }

        public static string ConvertToBase64String(byte[] byteValue)
        {
            string strRtn = null;

            strRtn = Convert.ToBase64String(byteValue);

            return strRtn;
        }

        public static byte[] ConvertFromBase64String(string strBase64)
        {
            byte[] byteRtn = null;

            byteRtn = Convert.FromBase64String(strBase64);

            return byteRtn;
        }

        /// <summary>
        /// Encryption the plain text with AES and format the bytes as "X2"
        /// </summary>
        /// <param name="strPlainText">The plain text</param>
        /// <returns>Encrypted string</returns>
        public static string AESX2Encryption(string strPlainText)
        {
            if (string.IsNullOrEmpty(strPlainText))
                throw new ArgumentNullException("PlainText is null");

            string strRtn = string.Empty;
            byte[] byteEncrypt;
            SymmetricAlgorithm algorithm = System.Security.Cryptography.Aes.Create();
            algorithm.Mode = CipherMode.ECB;
            algorithm.Key = InternalGetCryptographyKey();//<add key="ChiperKey" value="8 7 6 5 4 3 2 1 16 15 14 13 12 11 10 9"/>
            algorithm.Padding = PaddingMode.PKCS7;

            ICryptoTransform ctf = algorithm.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, ctf, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(strPlainText);
                    }
                    byteEncrypt = ms.ToArray();
                }
            }

            for (int i = 0; i < byteEncrypt.Length; i++)
            {
                strRtn += byteEncrypt[i].ToString("X2");
            }

            return strRtn;
        }

        /// <summary>
        /// Decryption the encrypted string with AES which formatted as "X2"
        /// </summary>
        /// <param name="strEncryption">The encrypted string which formatted as "X2"</param>
        /// <returns>Decrypted string</returns>
        public static string AESX2Decryption(string strEncryption)
        {
            if (string.IsNullOrEmpty(strEncryption))
                throw new ArgumentNullException("strEncryption is null or empty");

            int intByteEncryptionLength = Convert.ToInt32(strEncryption.Length / 2);

            byte[] byteEncryption = new byte[intByteEncryptionLength];

            int i = 0;

            int j = 0;

            while (i <= strEncryption.Length - 2)
            {
                byteEncryption.SetValue(Convert.ToByte(strEncryption.Substring(i, 2), 16), j);
                i += 2;
                j += 1;
            }

            string strPlainText;
            SymmetricAlgorithm algorithm = System.Security.Cryptography.Aes.Create();
            algorithm.Mode = CipherMode.ECB;
            algorithm.Key = InternalGetCryptographyKey();
            algorithm.Padding = PaddingMode.PKCS7;

            ICryptoTransform ctf = algorithm.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream(byteEncryption))
            {
                using (CryptoStream cs = new CryptoStream(ms, ctf, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs, System.Text.Encoding.UTF8))
                    {
                        strPlainText = sr.ReadToEnd();
                    }
                }
            }

            return strPlainText;
        }

        /// <summary>
        /// Encryption the plain text with AES by key and return the encrypted string encoded with BASE64
        /// </summary>
        /// <param name="strPlainText">The plain text</param>
        /// <param name="byteKey">The key used for encryption</param>
        /// <returns>Encrypted string encoded with BASE64</returns>
        public static string AESBase64Encryption(string strPlainText, byte[] byteKey)
        {
            if (string.IsNullOrEmpty(strPlainText))
                throw new ArgumentNullException("PlainText is null");
            if (byteKey == null)
                throw new ArgumentNullException("byteKey is null");

            byte[] byteEncrypt;
            SymmetricAlgorithm algorithm = System.Security.Cryptography.Aes.Create();
            algorithm.Mode = CipherMode.ECB;
            algorithm.Key = byteKey;
            algorithm.Padding = PaddingMode.PKCS7;

            ICryptoTransform ctf = algorithm.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, ctf, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(strPlainText);
                    }
                    byteEncrypt = ms.ToArray();
                }
            }
            return ConvertToBase64String(byteEncrypt);
        }

        /// <summary>
        /// Decryption the encrypted string which encoded by BASE64 with AES by key
        /// </summary>
        /// <param name="strEncryption">The encrypted string</param>
        /// <param name="byteKey">The key used fro decryption</param>
        /// <returns>Decrypted string</returns>
        public static string AESBase64Decryption(string strEncryption, byte[] byteKey)
        {
            if (string.IsNullOrEmpty(strEncryption))
                throw new ArgumentNullException("strEncryption is null or empty");
            if (byteKey == null || byteKey.Length <= 0)
                throw new ArgumentNullException("byteKey is null");

            byte[] byteEncryption = ConvertFromBase64String(strEncryption);
            string strPlainText;
            SymmetricAlgorithm algorithm = System.Security.Cryptography.Aes.Create();
            algorithm.Mode = CipherMode.ECB;
            algorithm.Key = byteKey;
            algorithm.Padding = PaddingMode.PKCS7;

            ICryptoTransform ctf = algorithm.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream(byteEncryption))
            {
                using (CryptoStream cs = new CryptoStream(ms, ctf, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs, System.Text.Encoding.UTF8))
                    {
                        strPlainText = sr.ReadToEnd();
                    }
                }
            }

            return strPlainText;
        }

        /// <summary>
        /// Encryption the plain text with AES by key and return the encrypted string encoded with BASE64 and UrlEncode
        /// </summary>
        /// <param name="strPlainText">The plain text</param>
        /// <param name="byteKey">The key used for encryption</param>
        /// <returns>Encrypted string encoded with BASE64 and UrlEncode</returns>
        public static string AESBase64UrlEncodeEncryption(string strPlainText,byte[] byteKey)
        {
            if (string.IsNullOrEmpty(strPlainText))
                throw new ArgumentNullException("PlainText is null");
            if (byteKey == null)
                throw new ArgumentNullException("byteKey is null");
            
            return HttpUtility.UrlEncode(AESBase64Encryption(strPlainText,byteKey));
        }

        /// <summary>
        /// Decryption the encrypted string which encoded by BASE64 and UrlEncode with AES by key
        /// </summary>
        /// <param name="strEncryption">The encrypted string</param>
        /// <param name="byteKey">The key used fro decryption</param>
        /// <returns>Decrypted string</returns>
        public static string AESBase64UrlDecodeDecryption(string strEncryption, byte[] byteKey)
        {
            if (string.IsNullOrEmpty(strEncryption))
                throw new ArgumentNullException("strEncryption is null or empty");
            if (byteKey == null || byteKey.Length <= 0)
                throw new ArgumentNullException("byteKey is null");

            string strUrlDecode = HttpUtility.UrlDecode(strEncryption);

            return AESBase64Decryption(strUrlDecode, byteKey);
        }

        public static byte[] InternalGetCryptographyKey(string strAppSettingCryptographyKey="ChiperKey",char chrSplit=' ')
        {
            try
            {
                string strAppSettingCryptographyValue = ConfigurationManager.AppSettings[strAppSettingCryptographyKey];
                               
                byte[] byteRtn = null;

                if (string.IsNullOrEmpty(strAppSettingCryptographyValue))
                {
                    byteRtn = new byte[] { 8, 7, 6, 5, 4, 3, 2, 1, 16, 15, 14, 13, 12, 11, 10, 9 };
                }
                else
                {
                    string[] strBytes = strAppSettingCryptographyValue.Split(chrSplit);

                    byteRtn = new byte[strBytes.Length];

                    int intTry;

                    int i = 0;

                    foreach (string str in strBytes)
                    {
                        if (!int.TryParse(str, out intTry))
                        {
                            throw new Exception("Convert From Cryptography Bytes String Failed!");
                        }
                        else
                        {
                            int temp;

                            temp = Convert.ToInt32(str);

                            byteRtn.SetValue((byte)temp, i);

                            i++;
                        }
                    }
                }
                return byteRtn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FormatEmail(string email)
        {
            return FormatEmail(email, true);
        }

        public static string FormatEmail(string email, bool replaceAtMark)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(email))
            {
                return str;
            }
            string str2 = replaceAtMark ? email.Replace('@', '#') : email;
            if (email.IndexOfAny(new char[] { '@', '#' }) > -1)
            {
                return string.Format("<a herf=\"mailto:{0}\">{1}</a>", email.Replace('#', '@'), str2);
            }
            return email;
        }

        public static string StripAllTags(string stringToStrip)
        {
            return StripAllTags(stringToStrip, true);
        }

        public static string StripAllTags(string stringToStrip, bool enableMultiLine)
        {
            if (enableMultiLine)
            {
                stringToStrip = Regex.Replace(stringToStrip, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", "\n\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                stringToStrip = Regex.Replace(stringToStrip, @"<br(?:\s*)/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            else
            {
                stringToStrip = Regex.Replace(stringToStrip, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                stringToStrip = Regex.Replace(stringToStrip, @"<br(?:\s*)/>", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            stringToStrip = Regex.Replace(stringToStrip, "&[^;]*;", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            stringToStrip = StripHtmlXmlTags(stringToStrip);
            return stringToStrip;
        }

        public static string StripForPreview(string content)
        {
            return StripUBBTags(StripHtmlXmlTags(content.Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n").Replace("<p>", "\n").Replace("'", "&#39;")));
        }

        public static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string StripScriptTags(string content)
        {
            content = Regex.Replace(content, "<script((.|\n)*?)</script>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return Regex.Replace(content, "\"javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        public static string StripUBBTags(string content)
        {
            return Regex.Replace(content, @"\[[^\]]*?\]", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string TrimHtml(string sourceHtml, int charLimit)
        {
            if (string.IsNullOrEmpty(sourceHtml))
            {
                return string.Empty;
            }
            string stringTrim = StripUBBTags(StripAllTags(sourceHtml, false));
            if ((charLimit <= 0) || (charLimit >= stringTrim.Length))
            {
                return stringTrim;
            }
            return StringUtils.Trim(stringTrim, charLimit);
        }

        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16]; 
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }

    public static class StringUtils
    {
        public static string Base64_Decode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Base64_Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string CleanInvalidCharsForXML(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            StringBuilder builder = new StringBuilder();
            char[] chArray = input.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                int num2 = Convert.ToInt32(chArray[i]);
                if ((((num2 < 0) || (num2 > 8)) && ((num2 < 11) || (num2 > 12))) && ((num2 < 14) || (num2 > 0x1f)))
                {
                    builder.Append(chArray[i]);
                }
            }
            return builder.ToString();
        }

        public static string StripSQLInjection(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                string pattern = @"(\%27)|(\')|(\-\-)";
                string str2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";
                string str3 = @"\s+exec(\s|\+)+(s|x)p\w+";
                sql = Regex.Replace(sql, pattern, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, str2, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, str3, string.Empty, RegexOptions.IgnoreCase);
            }
            return sql;
        }

        public static string Trim(string stringTrim, int maxLength)
        {
            return Trim(stringTrim, maxLength, "...");
        }

        public static string Trim(string rawString, int maxLength, string appendString)
        {
            if (string.IsNullOrEmpty(rawString) || (rawString.Length <= maxLength))
            {
                return rawString;
            }
            if (Encoding.UTF8.GetBytes(rawString).Length <= (maxLength * 2))
            {
                return rawString;
            }
            int length = Encoding.UTF8.GetBytes(appendString).Length;
            StringBuilder builder = new StringBuilder();
            int num3 = 0;
            for (int i = 0; i < rawString.Length; i++)
            {
                char ch = rawString[i];
                builder.Append(ch);
                num3 += Encoding.Default.GetBytes(new char[] { ch }).Length;
                if (num3 >= ((maxLength * 2) - length))
                {
                    break;
                }
            }
            return (builder.ToString() + appendString);
        }

        public static string UnicodeEncode(string rawText)
        {
            if ((rawText == null) || (rawText == string.Empty))
            {
                return rawText;
            }
            string str = "";
            string str4 = rawText;
            for (int i = 0; i < str4.Length; i++)
            {
                int num = str4[i];
                string str2 = "";
                if (num > 0x7e)
                {
                    str = str + @"\u";
                    str2 = num.ToString("x");
                    for (int j = 0; j < (4 - str2.Length); j++)
                    {
                        str = str + "0";
                    }
                }
                else
                {
                    str2 = ((char)num).ToString();
                }
                str = str + str2;
            }
            return str;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Configuration;
using Castle.Components.DictionaryAdapter;

namespace Nop.Plugin.Company.Media.Tools
{
    public class Core
    {

        public static class IntegerToWritten
        {
            static string[] ones = new string[] { "", "اول", "دوم", "سوم", "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم" };
            static string[] teens = new string[] { "دهم", "یازدهم", "دوازدهم", "سیزدهم", "چهاردهم", "پانزدهم", "شانزدهم", "هفدهم", "هجدهم", "نانزدهم" };
            static string[] tens = new string[] { "بیستم", "سی ام", "چهال ام", "پنجاه ام", "شصت ام", "هفتاد ام", "هشتاد ام", "نود ام" };
            static string[] thousandsGroups = { "", " هزار", " میلیون", " بیلیون" };

            private static string FriendlyInteger(int n, string leftDigits, int thousands)
            {
                if (n == 0)
                {
                    return leftDigits;
                }

                string friendlyInt = leftDigits;

                if (friendlyInt.Length > 0)
                {
                    friendlyInt += " ";
                }

                if (n < 10)
                {
                    friendlyInt += ones[n];
                }
                else if (n < 20)
                {
                    friendlyInt += teens[n - 10];
                }
                else if (n < 100)
                {
                    friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
                }
                else if (n < 1000)
                {
                    friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Hundred"), 0);
                }
                else
                {
                    friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
                    if (n % 1000 == 0)
                    {
                        return friendlyInt;
                    }
                }

                return friendlyInt + thousandsGroups[thousands];
            }

            public static string IntegerToWrittens(byte? n)
            {
                if (n == 0)
                {
                    return "Zero";
                }
                else if (n < 0)
                {
                    //return "Negative " + IntegerToWrittens((int)n*-1);
                }

                return FriendlyInteger((int)n.Value, "", 0);
            }
        }

        public class Convertor
        {
            public static bool IsDate(string value)
            {
                DateTime i;
                return DateTime.TryParse(value, out i);
            }

            public static bool IsInt16(string value)
            {
                Int16 i;
                return Int16.TryParse(value, out i);
            }

            public static Int16 ToInt16(string value)
            {
                if (IsInt16(value))
                    return Convert.ToInt16(value);
                return 0;
            }

            public static int? ToInt(object value)
            {
                try
                {
                    return int.Parse(value.ToString());
                }
                catch (Exception ee)
                {
                    return null;
                }
            }

            public static long? ToLong(object value)
            {
                try
                {
                    return long.Parse(value.ToString());
                }
                catch (Exception ee)
                {
                    return null;
                }
            }

            public static decimal? ToDecimal(object value)
            {
                try
                {
                    return decimal.Parse(value.ToString());
                }
                catch (Exception ee)
                {
                    return null;
                }
            }

            public static DateTime? ToDateTime(object value)
            {
                try
                {
                    return DateTime.Parse(value.ToString());
                }
                catch (Exception ee)
                {
                    return null;
                }
            }

            public static bool? ToBoolean(object value)
            {
                try
                {
                    return bool.Parse(value.ToString());
                }
                catch (Exception ee)
                {
                    return null;
                }
            }

            public static string ToString(object value)
            {
                try
                {
                    return value.ToString();
                }
                catch (Exception ee)
                {
                    return string.Empty;
                }
            }


            public static string NormilizePersianString(string text)
            {
                if (String.IsNullOrEmpty(text))
                {
                    return "";
                }
                string newText = "";
                for (int i = 0; i < text.Length; i++)
                {
                    char temp = new char();
                    temp = text.ElementAt(i);
                    int x = temp;
                    if (x == 1603)
                    {
                        temp = 'ک';
                    }
                    else if (x == 1610)
                    {
                        temp = 'ی';
                    }
                    else if (x == 1776 || x == 1632)
                    {
                        temp = '0';
                    }
                    else if (x == 1777 || x == 1633)
                    {
                        temp = '1';
                    }
                    else if (x == 1778 || x == 1634)
                    {
                        temp = '2';
                    }
                    else if (x == 1779 || x == 1635)
                    {
                        temp = '3';
                    }
                    else if (x == 1780 || x == 1636)
                    {
                        temp = '4';
                    }

                    else if (x == 1781 || x == 1637)
                    {
                        temp = '5';
                    }
                    else if (x == 1782 || x == 1638)
                    {
                        temp = '6';
                    }
                    else if (x == 1783 || x == 1639)
                    {
                        temp = '7';
                    }
                    else if (x == 1784 || x == 1640)
                    {
                        temp = '8';

                    }

                    else if (x == 1785 || x == 1641)
                    {
                        temp = '9';
                    }
                    newText += temp;
                }
                return newText;
            }

            /// <summary>
            /// Space to '%20'
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static string RemoveSpaces(string input)
            {
                try
                {
                    string output = input;
                    while (output.IndexOf(' ') > 0)
                    {
                        int index = output.IndexOf(' ');
                        string temp = output.Substring(0, index);
                        temp += "%20";
                        output = temp + output.Substring(index + 1);
                    }

                    return output;
                }
                catch (Exception ee)
                {
                    return "";
                }
            }


            public static string ToSubString(object value, int len = 100)
            {
                try
                {
                    string str = value.ToString();

                    if (str.Length > len)
                        str = str.Remove(len);

                    return str;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }




            public static bool IsNumeric(string Number)
            {
                string newNumber = NormilizePersianString(Number);
                for (int i = 0; i < newNumber.Length; i++)
                {
                    if (newNumber.ElementAt(i) < 48 || newNumber.ElementAt(i) > 57)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class Date
        {
            public const string DisplayDateFormat = "yyyy/MM/dd";
            public const string DateTimeFormat = "";
            public const string TIME_FORMAT = "HH:mm";

            public static class ConvertDateTime
            {

                public static string ToShamsi(DateTime? datetime)
                {
                    try
                    {
                        if (datetime.HasValue)
                        {
                            string val = datetime.Value.ToString();
                            if (Convertor.IsDate(val))
                            {
                                DateTime MDate = new DateTime();
                                MDate = Convert.ToDateTime(val);
                                PersianCalendar Shamsi = new PersianCalendar();
                                string m = Shamsi.GetMonth(MDate).ToString();
                                if (m.Length < 2)
                                    m = m.Insert(0, "0");
                                string d = Shamsi.GetDayOfMonth(MDate).ToString();
                                if (d.Length < 2)
                                    d = d.Insert(0, "0");

                                return Shamsi.GetYear(MDate).ToString() + "/" + m + "/" + d + " " + MDate.ToString(" HH:mm");
                            }
                            return "0/0/0";
                        }
                        else
                        {
                            return "0/0/0";
                        }
                    }
                    catch (Exception ee)
                    {
                        return "0/0/0";
                    }
                }

                public static string ToShorShamsi(DateTime? datetime)
                {
                    try
                    {
                        if (datetime.HasValue)
                        {
                            string val = datetime.Value.ToString();
                            if (Convertor.IsDate(val))
                            {
                                DateTime MDate = new DateTime();
                                MDate = Convert.ToDateTime(val);
                                PersianCalendar Shamsi = new PersianCalendar();
                                string m = Shamsi.GetMonth(MDate).ToString();
                                if (m.Length < 2)
                                    m = m.Insert(0, "0");
                                string d = Shamsi.GetDayOfMonth(MDate).ToString();
                                if (d.Length < 2)
                                    d = d.Insert(0, "0");

                                return Shamsi.GetYear(MDate).ToString() + "/" + m + "/" + d;
                            }
                            return "0/0/0";
                        }
                        else
                        {
                            return "0/0/0";
                        }
                    }
                    catch (Exception ee)
                    {
                        return "0/0/0";
                    }
                }

                public static string ToMiladi(string value)
                {
                    if (IsShamsi(value))
                    {
                        PersianCalendar pc = new PersianCalendar();
                        int y = Convertor.ToInt16(value.Substring(0, 4));
                        int m = Convertor.ToInt16(value.Substring(5, value.IndexOf("/", 5) - 5));
                        int d = Convertor.ToInt16(value.Substring(value.LastIndexOf("/") + 1));
                        return pc.ToDateTime(y, m, d, 12, 12, 12, 12).ToShortDateString();
                    }
                    if (Convertor.IsDate(value))
                    {
                        DateTime MDate = new DateTime();
                        MDate = Convert.ToDateTime(value);
                        GregorianCalendar miladi = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
                        return miladi.GetYear(MDate).ToString() + "/" + miladi.GetMonth(MDate).ToString() + "/" + miladi.GetDayOfMonth(MDate);
                    }
                    return "";
                }

                public static DateTime ToMiladi(DateTime? shamsiDate)
                {
                    try
                    {
                        string value = shamsiDate.Value.ToString(Core.Date.DisplayDateFormat);
                        if (IsShamsi(value))
                        {
                            PersianCalendar pc = new PersianCalendar();
                            int y = Convertor.ToInt16(value.Substring(0, 4));
                            int m = Convertor.ToInt16(value.Substring(5, value.IndexOf("/", 5) - 5));
                            int d = Convertor.ToInt16(value.Substring(value.LastIndexOf("/") + 1));
                            return DateTime.Parse(pc.ToDateTime(y, m, d, 12, 12, 12, 12).ToShortDateString());
                        }
                        if (Convertor.IsDate(value))
                        {
                            DateTime MDate = new DateTime();
                            MDate = Convert.ToDateTime(value);
                            GregorianCalendar miladi = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
                            return DateTime.Parse(miladi.GetYear(MDate).ToString() + "/" + miladi.GetMonth(MDate).ToString() + "/" + miladi.GetDayOfMonth(MDate));
                        }
                        return new DateTime();
                    }
                    catch (Exception ee)
                    {
                        return new DateTime();
                    }
                }

                public static string ToTime(DateTime? time)
                {
                    try
                    {
                        return time.Value.ToString("HH:mm");
                    }
                    catch (Exception ee)
                    {
                        return "--:--";
                    }
                }

                public static bool IsShamsi(string value)
                {
                    return (value.Length > 4 && Convertor.IsInt16(value.Substring(0, 4)) && (value.Substring(0, 4).StartsWith("13") || value.Substring(0, 4).StartsWith("14")));
                }



                public static DateTime ConcatDatetime(DateTime? a, DateTime b)
                {
                    try
                    {
                        return a.Value.Add(b.TimeOfDay);
                    }
                    catch (Exception)
                    {
                        return new DateTime();
                    }
                }
            }

            public static string WeekDay(int day)
            {
                if (day == 1)
                {
                    return "شنبه";
                }
                else if (day == 2)
                {
                    return "یکشنبه";
                }
                else if (day == 3)
                {
                    return "دوشنبه";
                }
                else if (day == 4)
                {
                    return "سه شنبه";
                }
                else if (day == 5)
                {
                    return "چهارشنبه";
                }
                else if (day == 6)
                {
                    return "پنجشنبه";
                }
                else if (day == 7)
                {
                    return "جمعه";
                }
                else
                {
                    return "";
                }

            }

            public static DateTime GetCurrentDate()
            {
                try
                {
                    return DateTime.Now;
                }
                catch (Exception ee)
                {
                    return new DateTime();
                }
            }

            public static string GetStringCurrentTime()
            {
                try
                {
                    return GetCurrentDate().ToString(TIME_FORMAT);
                }
                catch (Exception ee)
                {
                    return "--:--";
                }
            }

            public static string GetStringShortShamsiCurrentData()
            {
                try
                {
                    return ConvertDateTime.ToShorShamsi(GetCurrentDate());
                }
                catch (Exception ee)
                {
                    return "0/0/0";
                }
            }

            public static string ConvertLengthTimeToLength(int Lenght)
            {
                if (Lenght != 0)
                {
                    int time = Lenght * 15;
                    int h = time / 60;
                    int m = time % 60;
                    if (h > 0)
                    {
                        return " به مدت " + h.ToString("00") + ":" + m.ToString("00") + " ساعت  ";
                    }
                    else
                    {
                        return " به مدت " + m.ToString("00") + " دقیقه  ";
                    }
                }
                return " به مدت 0 دقیقه  ";
            }
            public static string ConvertLengthTimeToTime(int Lenght)
            {
                if (Lenght != 0)
                {
                    int time = Lenght * 15;
                    int h = time / 60;
                    int m = time % 60;

                    return h.ToString("00") + ":" + m.ToString("00");
                }
                else
                {
                    return "00:00";
                }

            }


            public static long GetYearShamsi(DateTime? datetime)
            {
                try
                {
                    if (datetime.HasValue)
                    {
                        string val = datetime.Value.ToString();
                        if (Convertor.IsDate(val))
                        {
                            DateTime MDate = new DateTime();
                            MDate = Convert.ToDateTime(val);
                            PersianCalendar Shamsi = new PersianCalendar();

                            return Convert.ToInt64(Shamsi.GetYear(MDate).ToString());
                        }
                        return 0;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ee)
                {
                    return 0;
                }
            }

            public static int GetMonth(DateTime? datetime)
            {
                try
                {
                    if (datetime.HasValue)
                    {
                        string val = datetime.Value.ToString();
                        if (Convertor.IsDate(val))
                        {
                            DateTime MDate = new DateTime();
                            MDate = Convert.ToDateTime(val);
                            PersianCalendar Shamsi = new PersianCalendar();
                            string m = Shamsi.GetMonth(MDate).ToString();
                            if (m.Length < 2)
                                m = m.Insert(0, "0");

                            return Convert.ToInt32(m);
                        }
                        return 0;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ee)
                {
                    return 0;
                }
            }
            public static string GetNameMonth(DateTime? datetime)
            {
                try
                {
                    if (datetime.HasValue)
                    {
                        string val = datetime.Value.ToString();
                        if (Convertor.IsDate(val))
                        {
                            DateTime MDate = new DateTime();
                            MDate = Convert.ToDateTime(val);
                            PersianCalendar Shamsi = new PersianCalendar();
                            string m = Shamsi.GetMonth(MDate).ToString();
                            if (m.Length < 2)
                                m = m.Insert(0, "0");
                            string namemounth;
                            switch (Convert.ToByte(m))
                            {
                                case 1:
                                    namemounth = "فروردین";
                                    break;
                                case 2:
                                    namemounth = "اردیبهشت";
                                    break;
                                case 3:
                                    namemounth = "خرداد";
                                    break;
                                case 4:
                                    namemounth = "تیر";
                                    break;
                                case 5:
                                    namemounth = "مرداد";
                                    break;
                                case 6:
                                    namemounth = "شهریور";
                                    break;
                                case 7:
                                    namemounth = "مهر";
                                    break;
                                case 8:
                                    namemounth = "آبان";
                                    break;
                                case 9:
                                    namemounth = "آذر";
                                    break;
                                case 10:
                                    namemounth = "دی";
                                    break;
                                case 11:
                                    namemounth = "بهمن";
                                    break;
                                case 12:
                                    namemounth = "اسفند";
                                    break;
                                default:
                                    namemounth = "نامضخص";
                                    break;
                            }
                            return namemounth;
                        }
                        return "نامشخص";
                    }
                    else
                    {
                        return "نامشخص";
                    }
                }
                catch (Exception ee)
                {
                    return "نامشخص";
                }
            }
            public static string GetNameMonth(int month)
            {
                try
                {
                    string namemounth;
                    switch (Convert.ToByte(month))
                    {
                        case 1:
                            namemounth = "فروردین";
                            break;
                        case 2:
                            namemounth = "اردیبهشت";
                            break;
                        case 3:
                            namemounth = "خرداد";
                            break;
                        case 4:
                            namemounth = "تیر";
                            break;
                        case 5:
                            namemounth = "مرداد";
                            break;
                        case 6:
                            namemounth = "شهریور";
                            break;
                        case 7:
                            namemounth = "مهر";
                            break;
                        case 8:
                            namemounth = "آبان";
                            break;
                        case 9:
                            namemounth = "آذر";
                            break;
                        case 10:
                            namemounth = "دی";
                            break;
                        case 11:
                            namemounth = "بهمن";
                            break;
                        case 12:
                            namemounth = "اسفند";
                            break;
                        default:
                            namemounth = "نامضخص";
                            break;
                    }
                    return namemounth;


                }
                catch (Exception ee)
                {
                    return "نامشخص";
                }
            }
        }

        public class Security
        {
            /// <summary>
            ///  Hashes an text with MD5.  Suitable for use with Gravatar profile
            /// image urls
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string Hash_MD5(string text)
            {
                // Create a new instance of the MD5CryptoServiceProvider object.  
                MD5 md5Hasher = MD5.Create();
                // Convert the input string to a byte array and compute the hash.  
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
                // Create a new Stringbuilder to collect the bytes  
                // and create a string.  
                StringBuilder sBuilder = new StringBuilder();
                // Loop through each byte of the hashed data  
                // and format each one as a hexadecimal string.  
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();  // Return the hexadecimal string. 
            }

            public class Encryption
            {
                public const string EncKey = "(WEb;$5HDxC39u6)SKOef4`w/Ga7A6G7yuqBcDsqy6l1kgt;~gzG58o02n5H3XA58M6y29%7cOXYm'seQ's30fygB+Vk7judh*tYS38N16Jq88!K8Rs~bwEMAA6x85gt;*tY9j1n14Bylyp2M~gzG58o02n5HsQ,7(PP?fWBm.jEH]hjNv^Uzv8C=fW6sb?G!b4eU,v;tfFTn)@5#w?EpzeAgt6R}QNPHT9W`REKc*2_XA58M6y29%7cOb6)nLZ";
                public static string Encrypt(object pstrText)
                {
                    try
                    {

                        char[] pass = pstrText.ToString().ToCharArray();
                        string[] symb = { "q", "e", "a", "g", "h", "j" };
                        Random rnd = new Random();
                        string newpass = "";
                        for (int i = pass.Length - 1; i >= 0; i--)
                        {
                            newpass += ((int)pass[i]).ToString() + symb[rnd.Next(0, 5)];
                        }
                        return newpass;
                    }
                    catch (Exception ex)
                    {
                        return "0";
                    }
                }

                public static string Decrypt(object pstrText)
                {
                    try
                    {
                        string[] tokens = pstrText.ToString().Split('q', 'e', 'a', 'g', 'h', 'j');
                        string pass = "";
                        for (int i = tokens.Length - 2; i >= 0; i--)
                        {
                            pass += ((char)Convert.ToInt32(tokens[i])).ToString();
                        }
                        return pass;
                    }
                    catch (Exception ex)
                    {
                        return "0";
                    }
                }
            }

            public static string GenerateCode()
            {
                string words = "qwertyuiopasdfghjklzxcvbnm1234567890";
                string code = "";
                Random rnd = new Random();

                for (int i = 0; i < 6; i++)
                {
                    int UNchar = rnd.Next(0, 35);
                    code += words.ElementAt(UNchar);

                }
                return code;
            }
        }

        public class Gson
        {
            /// <summary>
            /// این تابع از فرمت تاریخ استاندارد بهره می برد
            /// "yyyy-MM-dd HH:mm:ss"
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static string ToJson(object obj)
            {
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" });
            }
        }

        public class Login
        {
            public static bool CheckLogin()
            {
                try
                {
                    return true;
                }
                catch (Exception ee)
                {
                    return false;
                }
            }
        }

        public class Month
        {
            public static List<List<string>> GetMonthList()
            {
                List<string> MonthList = new EditableList<string>();
                List<List<string>> AllList = new EditableList<List<string>>();
                MonthList.Add("فروردین");
                MonthList.Add("1");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("اردیبهشت");
                MonthList.Add("2");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("خرداد");
                MonthList.Add("3");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("تیر");
                MonthList.Add("4");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("مرداد");
                MonthList.Add("5");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("شهریور");
                MonthList.Add("6");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("مهر");
                MonthList.Add("7");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("آبان");
                MonthList.Add("8");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("آذر");
                MonthList.Add("9");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("دی");
                MonthList.Add("10");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("بهمن");
                MonthList.Add("11");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();
                MonthList.Add("اسفند");
                MonthList.Add("12");
                AllList.Add(MonthList);
                MonthList = new EditableList<string>();


                return AllList;
            }

            public static string GetMonthName(int id)
            {
                switch (id)
                {
                    case 1:
                        return "فروردین";
                        break;
                    case 2:
                        return "اردیبهشت";
                        break;
                    case 3:
                        return "خرداد";
                        break;
                    case 4:
                        return "تیر";
                        break;
                    case 5:
                        return "مرداد";
                        break;
                    case 6:
                        return "شهریور";
                        break;
                    case 7:
                        return "مهر";
                        break;
                    case 8:
                        return "آبان";
                        break;
                    case 9:
                        return "آذر";
                        break;
                    case 10:
                        return "دی";
                        break;
                    case 11:
                        return "بهمن";
                        break;
                    case 12:
                        return "اسفند";
                        break;
                }
                return "نامعتبر";
            }
            public static int GetMonthNumber(string id)
            {
                switch (id)
                {
                    case "فروردین":
                        return 1;
                        break;
                    case "اردیبهشت":
                        return 2;
                        break;
                    case "خرداد":
                        return 3;
                        break;
                    case "تیر":
                        return 4;
                        break;
                    case "مرداد":
                        return 5;
                        break;
                    case "شهریور":
                        return 6;
                        break;
                    case "مهر":
                        return 7;
                        break;
                    case "آبان":
                        return 8;
                        break;
                    case "آذر":
                        return 9;
                        break;
                    case "دی":
                        return 10;
                        break;
                    case "بهمن":
                        return 11;
                        break;
                    case "اسفند":
                        return 12;
                        break;
                }
                return 0;
            }
            public static int GetDecade(int day)
            {
                if (day <= 10)
                {
                    return 1;
                }
                else
                {
                    if (day <= 20)
                    {
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
        }

        public class Summery
        {
            public static string GetSummery(string Text, int index)
            {
                if (Text != null)
                {
                    if (Text.Length > index)
                    {
                        return Text.Substring(0, index - 1);
                    }
                    else
                    {
                        return Text;
                    }
                }

                return String.Empty;
            }
        }

        public class Like
        {
            public static string GetType(int ID)
            {
                if (ID == 1)
                {
                    return "پسندیدم";
                }
                else
                {
                    return "نمی پسندم";
                }
            }
        }

    }
}

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Kit
{
    public class RegexHelper
    {
        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        /// <returns></returns>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <returns></returns>
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证数字(double类型)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumber(string input)
        {
            //return IsMatch(input, @"^-?\d+$|^(-?\d+)(\.\d+)?$");

            if (double.TryParse(input, out double d))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInteger(string input)
        {
            //return IsMatch(input, @"^-?\d+$");
            if (int.TryParse(input, out int i))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证非负整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIntegerNotNagtive(string input)
        {
            //return IsMatch(input, @"^\d+$");

            int i = -1;
            if (int.TryParse(input, out i) && i >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIntegerPositive(string input)
        {
            //return IsMatch(input, @"^[0-9]*[1-9][0-9]*$");

            if (int.TryParse(input, out int i) && i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDecimal(string input)
        {
            return IsMatch(input, @"^([-+]?[1-9]\d*\.\d+|-?0\.\d*[1-9]\d*)$");
        }

        /// <summary>
        /// 验证只包含英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEnglishCharacter(string input)
        {
            return IsMatch(input, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// 验证只包含数字和英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIntegerAndEnglishCharacter(string input)
        {
            return IsMatch(input, @"^[0-9A-Za-z]+$");
        }

        /// <summary>
        /// 验证只包含汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChineseCharacter(string input)
        {
            return IsMatch(input, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 验证数字长度范围（数字前端的0计长度）
        /// [若要验证固定长度，可传入相同的两个长度数值]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lengthBegin">长度范围起始值（含）</param>
        /// <param name="lengthEnd">长度范围结束值（含）</param>
        /// <returns></returns>
        public static bool IsIntegerLength(string input, int lengthBegin, int lengthEnd)
        {
            //return IsMatch(input, @"^\d{" + lengthBegin + "," + lengthEnd + "}$");

            if (input.Length >= lengthBegin && input.Length <= lengthEnd)
            {
                if (int.TryParse(input, out int i))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证字符串包含内容
        /// </summary>
        /// <param name="input"></param>
        /// <param name="withEnglishCharacter">是否包含英文字母</param>
        /// <param name="withNumber">是否包含数字</param>
        /// <param name="withChineseCharacter">是否包含汉字</param>
        /// <returns></returns>
        public static bool IsStringInclude(string input, bool withEnglishCharacter, bool withNumber, bool withChineseCharacter)
        {
            if (!withEnglishCharacter && !withNumber && !withChineseCharacter)
            {
                return false;
            }
            var patternString = new StringBuilder();
            patternString.Append("^[");
            if (withEnglishCharacter)
            {
                patternString.Append("a-zA-Z");
            }
            if (withNumber)
            {
                patternString.Append("0-9");
            }
            if (withChineseCharacter)
            {
                patternString.Append(@"\u4E00-\u9FA5");
            }
            patternString.Append("]+$");
            return IsMatch(input, patternString.ToString());
        }

        /// <summary>
        /// 验证字符串长度范围
        /// [若要验证固定长度，可传入相同的两个长度数值]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lengthBegin">长度范围起始值（含）</param>
        /// <param name="lengthEnd">长度范围结束值（含）</param>
        /// <returns></returns>
        public static bool IsStringLength(string input, int lengthBegin, int lengthEnd)
        {
            //return IsMatch(input, @"^.{" + lengthBegin + "," + lengthEnd + "}$");

            if (input.Length >= lengthBegin && input.Length <= lengthEnd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证字符串长度范围（字符串内只包含数字和/或英文字母）
        /// [若要验证固定长度，可传入相同的两个长度数值]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lengthBegin">长度范围起始值（含）</param>
        /// <param name="lengthEnd">长度范围结束值（含）</param>
        /// <returns></returns>
        public static bool IsStringLengthOnlyNumberAndEnglishCharacter(string input, int lengthBegin, int lengthEnd)
        {
            return IsMatch(input, @"^[0-9a-zA-z]{" + lengthBegin + "," + lengthEnd + "}$");
        }

        /// <summary>
        /// 验证字符串长度范围
        /// </summary>
        /// <param name="input"></param>
        /// <param name="withEnglishCharacter">是否包含英文字母</param>
        /// <param name="withNumber">是否包含数字</param>
        /// <param name="withChineseCharacter">是否包含汉字</param>
        /// <param name="lengthBegin">长度范围起始值（含）</param>
        /// <param name="lengthEnd">长度范围结束值（含）</param>
        /// <returns></returns>
        public static bool IsStringLengthByInclude(string input, bool withEnglishCharacter, bool withNumber, bool withChineseCharacter, int lengthBegin, int lengthEnd)
        {
            if (!withEnglishCharacter && !withNumber && !withChineseCharacter)
            {
                return false;
            }
            var patternString = new StringBuilder();
            patternString.Append("^[");
            if (withEnglishCharacter)
            {
                patternString.Append("a-zA-Z");
            }
            if (withNumber)
            {
                patternString.Append("0-9");
            }
            if (withChineseCharacter)
            {
                patternString.Append(@"\u4E00-\u9FA5");
            }
            patternString.Append("]{" + lengthBegin + "," + lengthEnd + "}$");
            return IsMatch(input, patternString.ToString());
        }

        /// <summary>
        /// 验证字符串字节数长度范围
        /// [若要验证固定长度，可传入相同的两个长度数值；每个汉字为两个字节长度]
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lengthBegin">长度范围起始值（含）</param>
        /// <param name="lengthEnd">长度范围结束值（含）</param>
        /// <returns></returns>
        public static bool IsStringByteLength(string input, int lengthBegin, int lengthEnd)
        {
            //var byteLength = Regex.Replace(input, @"[^\x00-\xff]", "ok").Length;
            //if (byteLength >= lengthBegin && byteLength <= lengthEnd)
            //{
            //    return true;
            //}
            //return false;

            var byteLength = Encoding.Default.GetByteCount(input);
            if (byteLength >= lengthBegin && byteLength <= lengthEnd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDateTime(string input)
        {
            if (DateTime.TryParse(input, out DateTime dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证固定电话号码
        /// [3位或4位区号；区号可以用小括号括起来；区号可以省略；区号与本地号间可以用减号或空格隔开；可以有3位数的分机号，分机号前要加减号]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsTelePhoneNumber(string input)
        {
            return IsMatch(input, @"^(((0\d2|0\d{2})[- ]?)?\d{8}|((0\d3|0\d{3})[- ]?)?\d{7})(-\d{3})?$");
        }

        /// <summary>
        /// 验证手机号码
        /// [可匹配"(+86)013325656352"，括号可以省略，+号可以省略，(+86)可以省略，11位手机号前的0可以省略；11位手机号第二位数可以是3、4、5、8中的任意一个]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhoneNumber(string input)
        {
            return IsMatch(input, @"^((\+)?86|((\+)?86)?)0?1[3458]\d{9}$");
        }

        /// <summary>
        /// 验证电话号码（可以是固定电话号码或手机号码）
        /// [固定电话：[3位或4位区号；区号可以用小括号括起来；区号可以省略；区号与本地号间可以用减号或空格隔开；可以有3位数的分机号，分机号前要加减号]]
        /// [手机号码：[可匹配"(+86)013325656352"，括号可以省略，+号可以省略，(+86)可以省略，手机号前的0可以省略；手机号第二位数可以是3、4、5、8中的任意一个]]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string input)
        {
            return IsMatch(input, @"^((\+)?86|((\+)?86)?)0?1[3458]\d{9}$|^(((0\d2|0\d{2})[- ]?)?\d{8}|((0\d3|0\d{3})[- ]?)?\d{7})(-\d{3})?$");
        }

        /// <summary>
        /// 验证邮政编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsZipCode(string input)
        {
            //return IsMatch(input, @"^\d{6}$");

            if (input.Length != 6)
            {
                return false;
            }
            if (int.TryParse(input, out int i))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证电子邮箱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            return IsMatch(input, @"^([\w-\.]+)@([\w-\.]+)(\.[a-zA-Z0-9]+)$");
        }

        /// <summary>
        /// 验证网址（可以匹配IPv4地址但没对IPv4地址进行格式验证；IPv6暂时没做匹配）
        /// [允许省略"://"；可以添加端口号；允许层级；允许传参；域名中至少一个点号且此点号前要有内容]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsURL(string input)
        {
            return IsMatch(input, @"^([a-zA-Z]+://)?([\w-\.]+)(\.[a-zA-Z0-9]+)(:\d{0,5})?/?([\w-/]*)\.?([a-zA-Z]*)\??(([\w-]*=[\w%]*&?)*)$");
        }

        /// <summary>
        /// 验证IPv4地址
        /// [第一位和最后一位数字不能是0或255；允许用0补位]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPv4(string input)
        {
            //return IsMatch(input, @"^(25[0-4]|2[0-4]\d]|[01]?\d{2}|[1-9])\.(25[0-5]|2[0-4]\d]|[01]?\d?\d)\.(25[0-5]|2[0-4]\d]|[01]?\d?\d)\.(25[0-4]|2[0-4]\d]|[01]?\d{2}|[1-9])$");

            string[] IPs = input.Split('.');
            if (IPs.Length != 4)
            {
                return false;
            }
            for (int i = 0; i < IPs.Length; i++)
            {
                int n = -1;
                if (i == 0 || i == 3)
                {
                    if (int.TryParse(IPs[i], out n) && n > 0 && n < 255)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (int.TryParse(IPs[i], out n) && n >= 0 && n <= 255)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 验证IPv6地址
        /// [可用于匹配任何一个合法的IPv6地址]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIPv6(string input)
        {
            return IsMatch(input, @"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$");
        }

        public static bool IsIDCard(string input)
        {
            if (input.Length == 18)
            {
                return IsIDCard18(input);
            }
            else if (input.Length == 15)
            {
                return IsIDCard15(input);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证一代身份证号（15位数）
        /// [长度为15位的数字；匹配对应省份地址；生日能正确匹配]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIDCard15(string input)
        {
            if (!long.TryParse(input, out long l) || l.ToString().Length != 15)
            {
                return false;
            }
            var address = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91,";
            if (!address.Contains(input.Remove(2) + ","))
            {
                return false;
            }
            var birthdate = input.Substring(6, 6).Insert(4, "/").Insert(2, "/");
            if (!DateTime.TryParse(birthdate, out DateTime dt))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证二代身份证号（18位数，GB11643-1999标准）
        /// [长度为18位；前17位为数字，最后一位(校验码)可以为大小写x；匹配对应省份地址；生日能正确匹配；校验码能正确匹配]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIDCard18(string input)
        {
            long l = 0;
            if (!long.TryParse(input.Remove(17), out l) || l.ToString().Length != 17 || !long.TryParse(input.Replace('x', '0').Replace('X', '0'), out l))
            {
                return false;
            }
            var address = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91,";
            if (!address.Contains(input.Remove(2) + ","))
            {
                return false;
            }
            string birthdate = input.Substring(6, 8).Insert(6, "/").Insert(4, "/");
            if (!DateTime.TryParse(birthdate, out DateTime dt))
            {
                return false;
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = input.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != input.Substring(17, 1).ToLower())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 验证经度
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLongitude(string input)
        {
            //return IsMatch(input, @"^[-\+]?((1[0-7]\d{1}|0?\d{1,2})\.\d{1,5}|180\.0{1,5})$");

            if (float.TryParse(input, out float lon) && lon >= -180 && lon <= 180)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证纬度 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLatitude(string input)
        {
            //return IsMatch(input, @"^[-\+]?([0-8]?\d{1}\.\d{1,5}|90\.0{1,5})$");

            if (float.TryParse(input, out float lat) && lat >= -90 && lat <= 90)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Infrastructure.Extensions
{
   public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举说明
        /// </summary>
        public static string DisplayName(this Enum val)
        {
            return val.Display(DisplayProperty.Name) as string;
        }

        /// <summary>
        /// 获取枚举指定的显示内容
        /// </summary>
        public static object Display(this Enum val, DisplayProperty property)
        {
            var enumType = val.GetType();
            //if val is Flag enum, each item will connect with ","
            var str = val.ToString();

            if (enumType.GetAttribute<FlagsAttribute>() != null && str.Contains(","))
            {
                var array = str.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.Trim());

                var result = array.Aggregate("", (s, s1) =>
                {
                    var f = enumType.GetField(s1);

                    if (f != null)
                    {
                        var text = f.Display(property);
                        return s.IsNullOrEmpty() ? text.ToString() : $"{s},{text}";
                    }

                    return s;
                });

                return result.IsNullOrEmpty() ? null : result;
            }

            var field = enumType.GetField(str);
            if (field != null)
            {
                return field.Display(property);
            }

            return null;
        }

        /// <summary>
        /// 获取成员信息的Attribute
        /// </summary>
        public static TAttr GetAttribute<TAttr>(this MemberInfo member) where TAttr : Attribute
        {
            return Attribute.GetCustomAttribute(member, typeof(TAttr)) as TAttr;
        }

        /// <summary>
        /// 使用 string.Split 方法以指定的分隔符分隔字符串
        /// </summary>
        /// <param name="str">要分隔的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="options">分隔选项</param>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str?.Split(new[] { separator }, options);
        }

        /// <summary>
        /// 指示指定的字符串是 null 还是空字符串。
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 获取枚举指定的显示内容
        /// </summary>
        public static object Display(this MemberInfo memberInfo, DisplayProperty property)
        {
            if (memberInfo == null) return null;

            var display = memberInfo.GetAttribute<DisplayAttribute>();

            if (display != null)
            {
                switch (property)
                {
                    case DisplayProperty.Name:
                        return display.GetName();
                    case DisplayProperty.ShortName:
                        return display.GetShortName();
                    case DisplayProperty.GroupName:
                        return display.GetGroupName();
                    case DisplayProperty.Description:
                        return display.GetDescription();
                    case DisplayProperty.Order:
                        return display.GetOrder();
                    case DisplayProperty.Prompt:
                        return display.GetPrompt();
                }
            }

            return null;
        }

    }

}

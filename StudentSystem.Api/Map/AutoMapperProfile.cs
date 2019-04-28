using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace StudentSystem.Api.Map
{

    public class AutoMapperProfile : Profile
    {
        private static Dictionary<Type, Type> mapDics = new Dictionary<Type, Type>();
        public AutoMapperProfile()
        {
            foreach (var key in mapDics.Keys)
            {
                CreateMap(key, mapDics[key]);   //创建映射关系
            }
        }
        /// <summary>
        /// 添加映射
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="destinationType">目标类型</param>
        public static void AddMapping(Type sourceType, Type destinationType)
        {
            mapDics.Add(sourceType, destinationType);
        }
    }

    public class AutoMapperHelper
    {
        public static void CreateMap(Type type)
        {
            CreateMapping<AutoMapFromAttribute>(type);
            CreateMapping<AutoMapToAttribute>(type);
            CreateMapping<AutoMapAttribute>(type);
        }

        public static void Register()
        {
            Mapper.Initialize(x => x.AddProfile<AutoMapperProfile>());      //最后初始化注册一次
        }

        private static void CreateMapping<TAttribute>(Type type)
            where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            foreach (var autoMapAttr in type.GetCustomAttributes<TAttribute>())
            {
                if (autoMapAttr.TargetTypes == null || autoMapAttr.TargetTypes.Length == 0)
                {
                    continue;
                }

                foreach (var targetType in autoMapAttr.TargetTypes)
                {
                    if (autoMapAttr.Direciton.HasFlag(AutoMapDirection.To))
                    {
                        AutoMapperProfile.AddMapping(type, targetType);
                    }
                    if (autoMapAttr.Direciton.HasFlag(AutoMapDirection.From))
                    {
                        AutoMapperProfile.AddMapping(targetType, type);
                    }
                }
            }
        }
    }

}

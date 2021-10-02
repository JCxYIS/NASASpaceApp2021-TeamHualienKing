using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

public static class EnumHelper
{
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }

    public static Fatalness GetEventFatalNess(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field != null)
        {
            var attr = field.GetCustomAttributes(typeof(EventFatalnessAttribute), true).SingleOrDefault() as EventFatalnessAttribute;
            if (attr != null)
            {
                return attr.Value;
            }
        }
        throw new Exception("!!!");
        // return Convert.ToInt32(value);
    }
}


[AttributeUsage(AttributeTargets.Field)]
public class EventFatalnessAttribute : Attribute
{
    public EventFatalnessAttribute(Fatalness value)
    {
        Value = value;
    }

    /// <summary>
    /// 讀寫 Database 存入值
    /// </summary>
    public Fatalness Value { get; set; }
}

public enum Fatalness { Information, Warning, Danger, Fatal }
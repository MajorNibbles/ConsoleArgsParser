using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using ConsoleArgsParser.Attributes;

namespace ConsoleArgsParser
{
    public class ArgsParser
    {
        public static T Parse<T>(params string[] args)
        {
            //collect arg pairs
            var dict = new Dictionary<string, string>();

            for (int i = 0; i < args.Length -1; i++)
            {
                if(args[i].Contains("--"))
                    dict.Add(args[i].ToLower().Replace("--",""), args[i + 1]);
            }

            //Create new object
            var obj = Activator.CreateInstance<T>();

            //Populate each property of T
            var props = typeof(T).GetProperties();
            foreach (var p in props)
            {
                var name = p.Name;
                var optional = false;
                var prompt = true;

                var attribute = (ArgsOptionAttribute) p.GetCustomAttribute(typeof(ArgsOptionAttribute));
                if (attribute != null)
                {
                    //If custom attribute, use those values
                    name = attribute.Name ?? name;
                    optional = attribute.Optional;
                    prompt = attribute.PromptOnNull;
                }

                dict.TryGetValue(name.ToLower(), out var value);

                if (value != null)
                {
                    SetObjValue(ref obj, value, p);
                    continue;
                }

                if (optional)
                    continue;

                if(!prompt)
                    throw new NoNullAllowedException($"Unable to find args for non-optional property: {name}. Please enable PromptOnNull if you wish to allow user to enter value.");

                while (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"Please enter value for: {name}");
                    value = Console.ReadLine();
                }

                SetObjValue(ref obj, value, p);
            }

            return obj;
        }

        private static void SetObjValue<T>(ref T obj, string value, PropertyInfo p)
        {
            switch (p.PropertyType.Name)
            {
                case "Int32":
                    p.SetValue(obj, Convert.ToInt32(value));
                    break;
                case "Decimal":
                    p.SetValue(obj, Convert.ToDecimal(value));
                    break;
                case "Double":
                    p.SetValue(obj, Convert.ToDouble(value));
                    break;
                case "Single":
                    p.SetValue(obj, (float) Convert.ToDecimal(value));
                    break;
                case "Boolean":
                    p.SetValue(obj, Convert.ToBoolean(value));
                    break;
                default:
                    try
                    {
                        p.SetValue(obj, value);
                        break;
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Unable to set type {p.PropertyType.Name}. Please use: string, int, decimal, float or bool");
                    }
            }
        }
    }
}

using BaseModule.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 映台Dome
{
    public class SimpleFactory
    {
        public static T Create<T>() where T : test
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                $"ConfigFiles\\{typeof(T).Name}.json");

            string info = File.ReadAllText(path);
            T model = JsonHelper.JsonToObj<T>(info);
            return model;
        }
    }
}

using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IDEClient
{
    public class Validator
    {
        public static Regex Email = new Regex("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
    
        public static Regex Login = new Regex("^([\\w-\\.)(*&^%$#@!~]+){3,14}$");

        public static Regex Passwd = new Regex("^([\\w-\\.)(*&^%$#@!~]+){3,14}$");
    }
}

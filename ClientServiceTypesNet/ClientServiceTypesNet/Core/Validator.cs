using System.Text.RegularExpressions;

namespace IDEClient.Core
{
    public class Validator
    {
        public static Regex Email = new Regex("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
    
        public static Regex Login = new Regex("^([\\w-\\.)(*&^%$#@!~]+){3,14}$");

        public static Regex Passwd = new Regex("^([\\w-\\.)(*&^%$#@!~]+){3,14}$");
    }
}

using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    class TextPosition
    {
        [DataMember] 
        public int Line { get; set; }

        [DataMember] 
        public int Number { get; set; }

        public TextPosition(){ }

        public TextPosition(int line, int number)
        {
            Line = line;
            Number = number;
        }
    }
}

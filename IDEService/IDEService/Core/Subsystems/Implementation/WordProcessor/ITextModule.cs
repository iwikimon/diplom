using System;
using System.Collections.Generic;

namespace IDEService.Core
{
    interface ITextModule :IDisposable
    {
        void TypeWord(TextMessage msg);

        void TypeText(TextMessage msg);

        List<string> LoadFile(File file);

    }
}

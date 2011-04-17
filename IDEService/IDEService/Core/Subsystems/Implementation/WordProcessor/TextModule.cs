using System;
using System.Collections.Generic;

namespace IDEService.Core
{
    class TextModule :ITextModule
    {
        private ISubsystem _textSubsystem;

        public TextModule(ISubsystem wordProcessorSubsystem)
        {
            if (wordProcessorSubsystem.Type() != SubsystemType.WordProcessor)
                throw new ModuleLoadException("Неправилььный тип подсистемы");
            _textSubsystem = wordProcessorSubsystem;
        }

        public void TypeWord(TextMessage msg)
        {
            throw new NotImplementedException();
        }

        public void TypeText(TextMessage msg)
        {
            throw new NotImplementedException();
        }

        public List<string> LoadFile(File file)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}

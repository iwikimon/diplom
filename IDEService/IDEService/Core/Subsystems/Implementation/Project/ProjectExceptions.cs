using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    class ProjectExceptions : Exception
    {
        public ProjectExceptions(string errMsg) : base(errMsg){ }
    }

    class CreateProjectException : ProjectExceptions
    {
        public CreateProjectException(string errMsg) : base(errMsg){ }
    }

    class CreateProjectFolderException : ProjectExceptions
    {
        public CreateProjectFolderException(string errMsg) : base(errMsg){ }
    }
}

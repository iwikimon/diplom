using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    public class ProdjectExceptions : Exception
    {
        public ProdjectExceptions(string errMsg) : base(errMsg){ }
    }

    public class CreateProdjectException : ProdjectExceptions
    {
        public CreateProdjectException(string errMsg) : base(errMsg){ }
    }

    public class CreateProdjectFolderException : ProdjectExceptions
    {
        public CreateProdjectFolderException(string errMsg) : base(errMsg){ }
    }
}

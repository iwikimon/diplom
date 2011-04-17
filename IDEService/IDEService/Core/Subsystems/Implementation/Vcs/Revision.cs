using System.Collections.Generic;

namespace IDEService.Core
{
    class Revision
    {
        public File FileName { get; set; }

        public List<int> Revisions { get; set; }

        public Revision()
        {
        }

        public Revision(File fileName, List<int> revisions)
        {
            FileName = fileName;
            Revisions = revisions;
        }
    }
}

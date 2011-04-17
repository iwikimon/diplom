using System.Collections.Generic;

namespace IDEService.Core
{
    class Revision
    {
        public string FileName { get; set; }

        public List<int> Revisions { get; set; }

        public Revision()
        {
        }

        public Revision(string fileName, List<int> revisions)
        {
            FileName = fileName;
            Revisions = revisions;
        }
    }
}

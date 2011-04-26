using System;
using System.Collections.Generic;
using System.Windows.Controls;
using IDEService.Core;

namespace IDEClient.Core
{
   public enum ElemType
   {
       Folder,
       File
   }

    /// <summary>
    /// Представляет элементы в структуре проекта
    /// </summary>
    public class Elem
    {
        public string Name { get; set; }
        public ElemType Type { get; set; }
        public DBModelDtoBase Object { get; set; }
        public Image Icon { get; set; }
        public List<Elem> Children { get; set; }
        public FolderDto Folder { get; set; }

        public Elem()
        {
        }

        public Elem(string name,DBModelDtoBase obj ,ElemType type,ref ProjectStructure structure)
        {
            Name = name;
            Type = type;
            Object = obj;
            Children = new List<Elem>();
            if(Type ==  ElemType.Folder)
            {
                Folder = (FolderDto) obj;
                Children = Elem.GetFilesFromFolder(ref structure, (FolderDto)Object);
                var folders = Elem.GetFoldersFromFolder(ref structure, (FolderDto) Object);
                Children.AddRange(folders);
            }
        }


        public static Elem Build(ProjectStructure structure)
        {
            structure.Folders.Sort((x,y)=> x.Path.CompareTo(y.Path));
            var folder = structure.Folders[0];
            return new Elem(folder.Name, folder,ElemType.Folder,ref structure);
        }

        private static List<Elem> GetFilesFromFolder(ref ProjectStructure structure, FolderDto folder)
        {
            var result = new List<Elem>();   
            for(int i =0; i< structure.Files.Count; ++i)
            {
                if(structure.Files[i].Path == folder.Path+"\\"+folder.Name)
                {
                    var file = structure.Files[i];
                    structure.Files.RemoveAt(i);
                    result.Add(new Elem(file.Name, file, ElemType.File,ref structure));
                    --i;
                }
            }
            return result;
        }

        private static List<Elem> GetFoldersFromFolder(ref ProjectStructure structure, FolderDto folder)
        {
            var result = new List<Elem>();
            for (int i = 0; i < structure.Folders.Count; ++i)
            {
                if ((structure.Folders[i].Path == folder.Path + "\\" + folder.Name)&&(structure.Folders[i] != folder))
                {
                    var newFolder = structure.Folders[i];
                    structure.Folders.RemoveAt(i);
                    result.Add(new Elem(newFolder.Name, newFolder, ElemType.Folder, ref structure));
                    --i;
                }
            }
            return result;
        }
    }
}

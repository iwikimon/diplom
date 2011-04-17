using System;

using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Validation;
using Mindscape.LightSpeed.Linq;

namespace IDEService
{
  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class Chat : Entity<int>
  {
    #region Fields
  
    private System.DateTime _date;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _message;
    private int _projectId;
    private int _userId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Date entity attribute.</summary>
    public const string DateField = "Date";
    /// <summary>Identifies the Message entity attribute.</summary>
    public const string MessageField = "Message";
    /// <summary>Identifies the ProjectId entity attribute.</summary>
    public const string ProjectIdField = "ProjectId";
    /// <summary>Identifies the UserId entity attribute.</summary>
    public const string UserIdField = "UserId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Chats")]
    private readonly EntityHolder<Project> _project = new EntityHolder<Project>();
    [ReverseAssociation("Chats")]
    private readonly EntityHolder<User> _user = new EntityHolder<User>();


    #endregion
    
    #region Properties

    public Project Project
    {
      get { return Get(_project); }
      set { Set(_project, value); }
    }

    public User User
    {
      get { return Get(_user); }
      set { Set(_user, value); }
    }


    public System.DateTime Date
    {
      get { return Get(ref _date, "Date"); }
      set { Set(ref _date, value, "Date"); }
    }

    public string Message
    {
      get { return Get(ref _message, "Message"); }
      set { Set(ref _message, value, "Message"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Project" /> property.</summary>
    public int ProjectId
    {
      get { return Get(ref _projectId, "ProjectId"); }
      set { Set(ref _projectId, value, "ProjectId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="User" /> property.</summary>
    public int UserId
    {
      get { return Get(ref _userId, "UserId"); }
      set { Set(ref _userId, value, "UserId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class File : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _name;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _path;
    private int _projectId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Name entity attribute.</summary>
    public const string NameField = "Name";
    /// <summary>Identifies the Path entity attribute.</summary>
    public const string PathField = "Path";
    /// <summary>Identifies the ProjectId entity attribute.</summary>
    public const string ProjectIdField = "ProjectId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("File")]
    private readonly EntityCollection<Access> _accesses = new EntityCollection<Access>();
    [ReverseAssociation("Files")]
    private readonly EntityHolder<Project> _project = new EntityHolder<Project>();


    #endregion
    
    #region Properties

    public EntityCollection<Access> Accesses
    {
      get { return Get(_accesses); }
    }

    public Project Project
    {
      get { return Get(_project); }
      set { Set(_project, value); }
    }


    public string Name
    {
      get { return Get(ref _name, "Name"); }
      set { Set(ref _name, value, "Name"); }
    }

    public string Path
    {
      get { return Get(ref _path, "Path"); }
      set { Set(ref _path, value, "Path"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Project" /> property.</summary>
    public int ProjectId
    {
      get { return Get(ref _projectId, "ProjectId"); }
      set { Set(ref _projectId, value, "ProjectId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class Folder : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _name;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _path;
    private int _projectId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Name entity attribute.</summary>
    public const string NameField = "Name";
    /// <summary>Identifies the Path entity attribute.</summary>
    public const string PathField = "Path";
    /// <summary>Identifies the ProjectId entity attribute.</summary>
    public const string ProjectIdField = "ProjectId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Folder")]
    private readonly EntityCollection<Access> _accesses = new EntityCollection<Access>();
    [ReverseAssociation("Folders")]
    private readonly EntityHolder<Project> _project = new EntityHolder<Project>();


    #endregion
    
    #region Properties

    public EntityCollection<Access> Accesses
    {
      get { return Get(_accesses); }
    }

    public Project Project
    {
      get { return Get(_project); }
      set { Set(_project, value); }
    }


    public string Name
    {
      get { return Get(ref _name, "Name"); }
      set { Set(ref _name, value, "Name"); }
    }

    public string Path
    {
      get { return Get(ref _path, "Path"); }
      set { Set(ref _path, value, "Path"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Project" /> property.</summary>
    public int ProjectId
    {
      get { return Get(ref _projectId, "ProjectId"); }
      set { Set(ref _projectId, value, "ProjectId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class Project : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _name;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _sourcedir;
    private int _ownerId;
    private int _memberId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Name entity attribute.</summary>
    public const string NameField = "Name";
    /// <summary>Identifies the Sourcedir entity attribute.</summary>
    public const string SourcedirField = "Sourcedir";
    /// <summary>Identifies the OwnerId entity attribute.</summary>
    public const string OwnerIdField = "OwnerId";
    /// <summary>Identifies the MemberId entity attribute.</summary>
    public const string MemberIdField = "MemberId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Project")]
    private readonly EntityCollection<Chat> _chats = new EntityCollection<Chat>();
    [ReverseAssociation("Project")]
    private readonly EntityCollection<File> _files = new EntityCollection<File>();
    [ReverseAssociation("Project")]
    private readonly EntityCollection<Folder> _folders = new EntityCollection<Folder>();
    [ReverseAssociation("ProjectsOwner")]
    private readonly EntityHolder<User> _owner = new EntityHolder<User>();
    [ReverseAssociation("ProjectsMember")]
    private readonly EntityHolder<User> _member = new EntityHolder<User>();


    #endregion
    
    #region Properties

    public EntityCollection<Chat> Chats
    {
      get { return Get(_chats); }
    }

    public EntityCollection<File> Files
    {
      get { return Get(_files); }
    }

    public EntityCollection<Folder> Folders
    {
      get { return Get(_folders); }
    }

    public User Owner
    {
      get { return Get(_owner); }
      set { Set(_owner, value); }
    }

    public User Member
    {
      get { return Get(_member); }
      set { Set(_member, value); }
    }


    public string Name
    {
      get { return Get(ref _name, "Name"); }
      set { Set(ref _name, value, "Name"); }
    }

    public string Sourcedir
    {
      get { return Get(ref _sourcedir, "Sourcedir"); }
      set { Set(ref _sourcedir, value, "Sourcedir"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Owner" /> property.</summary>
    public int OwnerId
    {
      get { return Get(ref _ownerId, "OwnerId"); }
      set { Set(ref _ownerId, value, "OwnerId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Member" /> property.</summary>
    public int MemberId
    {
      get { return Get(ref _memberId, "MemberId"); }
      set { Set(ref _memberId, value, "MemberId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class Userinfo : Entity<int>
  {
    #region Fields
  
    private System.DateTime _registred;
    private System.DateTime _lastAccess;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _name;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _sname;
    [ValidatePresence]
    [ValidateEmailAddress]
    [ValidateLength(0, 65535)]
    private string _email;
    private int _userId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Registred entity attribute.</summary>
    public const string RegistredField = "Registred";
    /// <summary>Identifies the LastAccess entity attribute.</summary>
    public const string LastAccessField = "LastAccess";
    /// <summary>Identifies the Name entity attribute.</summary>
    public const string NameField = "Name";
    /// <summary>Identifies the Sname entity attribute.</summary>
    public const string SnameField = "Sname";
    /// <summary>Identifies the Email entity attribute.</summary>
    public const string EmailField = "Email";
    /// <summary>Identifies the UserId entity attribute.</summary>
    public const string UserIdField = "UserId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Userinfo")]
    private readonly EntityHolder<User> _user = new EntityHolder<User>();


    #endregion
    
    #region Properties

    public User User
    {
      get { return Get(_user); }
      set { Set(_user, value); }
    }


    public System.DateTime Registred
    {
      get { return Get(ref _registred, "Registred"); }
      set { Set(ref _registred, value, "Registred"); }
    }

    public System.DateTime LastAccess
    {
      get { return Get(ref _lastAccess, "LastAccess"); }
      set { Set(ref _lastAccess, value, "LastAccess"); }
    }

    public string Name
    {
      get { return Get(ref _name, "Name"); }
      set { Set(ref _name, value, "Name"); }
    }

    public string Sname
    {
      get { return Get(ref _sname, "Sname"); }
      set { Set(ref _sname, value, "Sname"); }
    }

    public string Email
    {
      get { return Get(ref _email, "Email"); }
      set { Set(ref _email, value, "Email"); }
    }

    public int UserId
    {
      get { return Get(ref _userId, "UserId"); }
      set { Set(ref _userId, value, "UserId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class Userlog : Entity<int>
  {
    #region Fields
  
    private System.DateTime _date;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _message;
    private int _userId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Date entity attribute.</summary>
    public const string DateField = "Date";
    /// <summary>Identifies the Message entity attribute.</summary>
    public const string MessageField = "Message";
    /// <summary>Identifies the UserId entity attribute.</summary>
    public const string UserIdField = "UserId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Userlogs")]
    private readonly EntityHolder<User> _user = new EntityHolder<User>();


    #endregion
    
    #region Properties

    public User User
    {
      get { return Get(_user); }
      set { Set(_user, value); }
    }


    public System.DateTime Date
    {
      get { return Get(ref _date, "Date"); }
      set { Set(ref _date, value, "Date"); }
    }

    public string Message
    {
      get { return Get(ref _message, "Message"); }
      set { Set(ref _message, value, "Message"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="User" /> property.</summary>
    public int UserId
    {
      get { return Get(ref _userId, "UserId"); }
      set { Set(ref _userId, value, "UserId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class User : Entity<int>
  {
    #region Fields
  
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _login;
    [ValidatePresence]
    [ValidateLength(0, 65535)]
    private string _password;
    private int _accessId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Login entity attribute.</summary>
    public const string LoginField = "Login";
    /// <summary>Identifies the Password entity attribute.</summary>
    public const string PasswordField = "Password";
    /// <summary>Identifies the AccessId entity attribute.</summary>
    public const string AccessIdField = "AccessId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Owner")]
    private readonly EntityCollection<Project> _projectsOwner = new EntityCollection<Project>();
    [ReverseAssociation("Member")]
    private readonly EntityCollection<Project> _projectsMember = new EntityCollection<Project>();
    [ReverseAssociation("User")]
    private readonly EntityCollection<Userlog> _userlogs = new EntityCollection<Userlog>();
    [ReverseAssociation("User")]
    private readonly EntityCollection<Chat> _chats = new EntityCollection<Chat>();
    [ReverseAssociation("User")]
    private readonly EntityHolder<Access> _access = new EntityHolder<Access>();
    [ReverseAssociation("User")]
    private readonly EntityHolder<Userinfo> _userinfo = new EntityHolder<Userinfo>();


    #endregion
    
    #region Properties

    public EntityCollection<Project> ProjectsOwner
    {
      get { return Get(_projectsOwner); }
    }

    public EntityCollection<Project> ProjectsMember
    {
      get { return Get(_projectsMember); }
    }

    public EntityCollection<Userlog> Userlogs
    {
      get { return Get(_userlogs); }
    }

    public EntityCollection<Chat> Chats
    {
      get { return Get(_chats); }
    }

    public Access Access
    {
      get { return Get(_access); }
      set { Set(_access, value); }
    }

    public Userinfo Userinfo
    {
      get { return Get(_userinfo); }
      set { Set(_userinfo, value); }
    }


    public string Login
    {
      get { return Get(ref _login, "Login"); }
      set { Set(ref _login, value, "Login"); }
    }

    public string Password
    {
      get { return Get(ref _password, "Password"); }
      set { Set(ref _password, value, "Password"); }
    }

    public int AccessId
    {
      get { return Get(ref _accessId, "AccessId"); }
      set { Set(ref _accessId, value, "AccessId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  public partial class Access : Entity<int>
  {
    #region Fields
  
    private string _rule;
    private int _fileId;
    private int _folderId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Rule entity attribute.</summary>
    public const string RuleField = "Rule";
    /// <summary>Identifies the FileId entity attribute.</summary>
    public const string FileIdField = "FileId";
    /// <summary>Identifies the FolderId entity attribute.</summary>
    public const string FolderIdField = "FolderId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Accesses")]
    private readonly EntityHolder<File> _file = new EntityHolder<File>();
    [ReverseAssociation("Accesses")]
    private readonly EntityHolder<Folder> _folder = new EntityHolder<Folder>();
    [ReverseAssociation("Access")]
    private readonly EntityHolder<User> _user = new EntityHolder<User>();


    #endregion
    
    #region Properties

    public File File
    {
      get { return Get(_file); }
      set { Set(_file, value); }
    }

    public Folder Folder
    {
      get { return Get(_folder); }
      set { Set(_folder, value); }
    }

    public User User
    {
      get { return Get(_user); }
      set { Set(_user, value); }
    }


    public string Rule
    {
      get { return Get(ref _rule, "Rule"); }
      set { Set(ref _rule, value, "Rule"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="File" /> property.</summary>
    public int FileId
    {
      get { return Get(ref _fileId, "FileId"); }
      set { Set(ref _fileId, value, "FileId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Folder" /> property.</summary>
    public int FolderId
    {
      get { return Get(ref _folderId, "FolderId"); }
      set { Set(ref _folderId, value, "FolderId"); }
    }

    #endregion
  }



  /// <summary>
  /// Provides a strong-typed unit of work for working with the DBModel model.
  /// </summary>
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  public partial class DBModelUnitOfWork : Mindscape.LightSpeed.DataServices.DataServiceUnitOfWork
  {

    public System.Linq.IQueryable<Chat> Chats
    {
      get { return this.Query<Chat>(); }
    }
    
    public System.Linq.IQueryable<File> Files
    {
      get { return this.Query<File>(); }
    }
    
    public System.Linq.IQueryable<Folder> Folders
    {
      get { return this.Query<Folder>(); }
    }
    
    public System.Linq.IQueryable<Project> Projects
    {
      get { return this.Query<Project>(); }
    }
    
    public System.Linq.IQueryable<Userinfo> Userinfos
    {
      get { return this.Query<Userinfo>(); }
    }
    
    public System.Linq.IQueryable<Userlog> Userlogs
    {
      get { return this.Query<Userlog>(); }
    }
    
    public System.Linq.IQueryable<User> Users
    {
      get { return this.Query<User>(); }
    }
    
    public System.Linq.IQueryable<Access> Accesses
    {
      get { return this.Query<Access>(); }
    }
    
  }

  namespace Contracts.Data
  {
    [System.Runtime.Serialization.DataContract(Name="DBModelDtoBase")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class DBModelDtoBase
    {
    }

    [System.Runtime.Serialization.DataContract(Name="Chat")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class ChatDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public System.DateTime Date { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Message { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int ProjectId { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int UserId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name="File")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class FileDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public string Name { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Path { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int ProjectId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name="Folder")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class FolderDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public string Name { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Path { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int ProjectId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name="Project")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class ProjectDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public string Name { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Sourcedir { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int OwnerId { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int MemberId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name="Userinfo")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class UserinfoDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public System.DateTime Registred { get; set; }
      [System.Runtime.Serialization.DataMember]
      public System.DateTime LastAccess { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Name { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Sname { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Email { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int UserId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name="Userlog")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class UserlogDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public System.DateTime Date { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Message { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int UserId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name="User")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class UserDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public string Login { get; set; }
      [System.Runtime.Serialization.DataMember]
      public string Password { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int AccessId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name="Access")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class AccessDto : DBModelDtoBase
    {
      [System.Runtime.Serialization.DataMember]
      public string Rule { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int FileId { get; set; }
      [System.Runtime.Serialization.DataMember]
      public int FolderId { get; set; }
    }


    
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public static partial class DBModelDtoExtensions
    {
      static partial void CopyDBModelDtoBase(Entity entity, DBModelDtoBase dto);
      static partial void CopyDBModelDtoBase(DBModelDtoBase dto, Entity entity);

      static partial void BeforeCopyChat(Chat entity, ChatDto dto);
      static partial void AfterCopyChat(Chat entity, ChatDto dto);
      static partial void BeforeCopyChat(ChatDto dto, Chat entity);
      static partial void AfterCopyChat(ChatDto dto, Chat entity);
      
      private static void CopyChat(Chat entity, ChatDto dto)
      {
        BeforeCopyChat(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Date = entity.Date;
        dto.Message = entity.Message;
        dto.ProjectId = entity.ProjectId;
        dto.UserId = entity.UserId;
        AfterCopyChat(entity, dto);
      }
      
      private static void CopyChat(ChatDto dto, Chat entity)
      {
        BeforeCopyChat(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Date = dto.Date;
        entity.Message = dto.Message;
        entity.ProjectId = dto.ProjectId;
        entity.UserId = dto.UserId;
        AfterCopyChat(dto, entity);
      }
      
      public static ChatDto AsDto(this Chat entity)
      {
        ChatDto dto = new ChatDto();
        CopyChat(entity, dto);
        return dto;
      }
      
      public static Chat CopyTo(this ChatDto source, Chat entity)
      {
        CopyChat(source, entity);
        return entity;
      }

      static partial void BeforeCopyFile(File entity, FileDto dto);
      static partial void AfterCopyFile(File entity, FileDto dto);
      static partial void BeforeCopyFile(FileDto dto, File entity);
      static partial void AfterCopyFile(FileDto dto, File entity);
      
      private static void CopyFile(File entity, FileDto dto)
      {
        BeforeCopyFile(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Name = entity.Name;
        dto.Path = entity.Path;
        dto.ProjectId = entity.ProjectId;
        AfterCopyFile(entity, dto);
      }
      
      private static void CopyFile(FileDto dto, File entity)
      {
        BeforeCopyFile(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Name = dto.Name;
        entity.Path = dto.Path;
        entity.ProjectId = dto.ProjectId;
        AfterCopyFile(dto, entity);
      }
      
      public static FileDto AsDto(this File entity)
      {
        FileDto dto = new FileDto();
        CopyFile(entity, dto);
        return dto;
      }
      
      public static File CopyTo(this FileDto source, File entity)
      {
        CopyFile(source, entity);
        return entity;
      }

      static partial void BeforeCopyFolder(Folder entity, FolderDto dto);
      static partial void AfterCopyFolder(Folder entity, FolderDto dto);
      static partial void BeforeCopyFolder(FolderDto dto, Folder entity);
      static partial void AfterCopyFolder(FolderDto dto, Folder entity);
      
      private static void CopyFolder(Folder entity, FolderDto dto)
      {
        BeforeCopyFolder(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Name = entity.Name;
        dto.Path = entity.Path;
        dto.ProjectId = entity.ProjectId;
        AfterCopyFolder(entity, dto);
      }
      
      private static void CopyFolder(FolderDto dto, Folder entity)
      {
        BeforeCopyFolder(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Name = dto.Name;
        entity.Path = dto.Path;
        entity.ProjectId = dto.ProjectId;
        AfterCopyFolder(dto, entity);
      }
      
      public static FolderDto AsDto(this Folder entity)
      {
        FolderDto dto = new FolderDto();
        CopyFolder(entity, dto);
        return dto;
      }
      
      public static Folder CopyTo(this FolderDto source, Folder entity)
      {
        CopyFolder(source, entity);
        return entity;
      }

      static partial void BeforeCopyProject(Project entity, ProjectDto dto);
      static partial void AfterCopyProject(Project entity, ProjectDto dto);
      static partial void BeforeCopyProject(ProjectDto dto, Project entity);
      static partial void AfterCopyProject(ProjectDto dto, Project entity);
      
      private static void CopyProject(Project entity, ProjectDto dto)
      {
        BeforeCopyProject(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Name = entity.Name;
        dto.Sourcedir = entity.Sourcedir;
        dto.OwnerId = entity.OwnerId;
        dto.MemberId = entity.MemberId;
        AfterCopyProject(entity, dto);
      }
      
      private static void CopyProject(ProjectDto dto, Project entity)
      {
        BeforeCopyProject(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Name = dto.Name;
        entity.Sourcedir = dto.Sourcedir;
        entity.OwnerId = dto.OwnerId;
        entity.MemberId = dto.MemberId;
        AfterCopyProject(dto, entity);
      }
      
      public static ProjectDto AsDto(this Project entity)
      {
        ProjectDto dto = new ProjectDto();
        CopyProject(entity, dto);
        return dto;
      }
      
      public static Project CopyTo(this ProjectDto source, Project entity)
      {
        CopyProject(source, entity);
        return entity;
      }

      static partial void BeforeCopyUserinfo(Userinfo entity, UserinfoDto dto);
      static partial void AfterCopyUserinfo(Userinfo entity, UserinfoDto dto);
      static partial void BeforeCopyUserinfo(UserinfoDto dto, Userinfo entity);
      static partial void AfterCopyUserinfo(UserinfoDto dto, Userinfo entity);
      
      private static void CopyUserinfo(Userinfo entity, UserinfoDto dto)
      {
        BeforeCopyUserinfo(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Registred = entity.Registred;
        dto.LastAccess = entity.LastAccess;
        dto.Name = entity.Name;
        dto.Sname = entity.Sname;
        dto.Email = entity.Email;
        dto.UserId = entity.UserId;
        AfterCopyUserinfo(entity, dto);
      }
      
      private static void CopyUserinfo(UserinfoDto dto, Userinfo entity)
      {
        BeforeCopyUserinfo(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Registred = dto.Registred;
        entity.LastAccess = dto.LastAccess;
        entity.Name = dto.Name;
        entity.Sname = dto.Sname;
        entity.Email = dto.Email;
        entity.UserId = dto.UserId;
        AfterCopyUserinfo(dto, entity);
      }
      
      public static UserinfoDto AsDto(this Userinfo entity)
      {
        UserinfoDto dto = new UserinfoDto();
        CopyUserinfo(entity, dto);
        return dto;
      }
      
      public static Userinfo CopyTo(this UserinfoDto source, Userinfo entity)
      {
        CopyUserinfo(source, entity);
        return entity;
      }

      static partial void BeforeCopyUserlog(Userlog entity, UserlogDto dto);
      static partial void AfterCopyUserlog(Userlog entity, UserlogDto dto);
      static partial void BeforeCopyUserlog(UserlogDto dto, Userlog entity);
      static partial void AfterCopyUserlog(UserlogDto dto, Userlog entity);
      
      private static void CopyUserlog(Userlog entity, UserlogDto dto)
      {
        BeforeCopyUserlog(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Date = entity.Date;
        dto.Message = entity.Message;
        dto.UserId = entity.UserId;
        AfterCopyUserlog(entity, dto);
      }
      
      private static void CopyUserlog(UserlogDto dto, Userlog entity)
      {
        BeforeCopyUserlog(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Date = dto.Date;
        entity.Message = dto.Message;
        entity.UserId = dto.UserId;
        AfterCopyUserlog(dto, entity);
      }
      
      public static UserlogDto AsDto(this Userlog entity)
      {
        UserlogDto dto = new UserlogDto();
        CopyUserlog(entity, dto);
        return dto;
      }
      
      public static Userlog CopyTo(this UserlogDto source, Userlog entity)
      {
        CopyUserlog(source, entity);
        return entity;
      }

      static partial void BeforeCopyUser(User entity, UserDto dto);
      static partial void AfterCopyUser(User entity, UserDto dto);
      static partial void BeforeCopyUser(UserDto dto, User entity);
      static partial void AfterCopyUser(UserDto dto, User entity);
      
      private static void CopyUser(User entity, UserDto dto)
      {
        BeforeCopyUser(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Login = entity.Login;
        dto.Password = entity.Password;
        dto.AccessId = entity.AccessId;
        AfterCopyUser(entity, dto);
      }
      
      private static void CopyUser(UserDto dto, User entity)
      {
        BeforeCopyUser(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Login = dto.Login;
        entity.Password = dto.Password;
        entity.AccessId = dto.AccessId;
        AfterCopyUser(dto, entity);
      }
      
      public static UserDto AsDto(this User entity)
      {
        UserDto dto = new UserDto();
        CopyUser(entity, dto);
        return dto;
      }
      
      public static User CopyTo(this UserDto source, User entity)
      {
        CopyUser(source, entity);
        return entity;
      }

      static partial void BeforeCopyAccess(Access entity, AccessDto dto);
      static partial void AfterCopyAccess(Access entity, AccessDto dto);
      static partial void BeforeCopyAccess(AccessDto dto, Access entity);
      static partial void AfterCopyAccess(AccessDto dto, Access entity);
      
      private static void CopyAccess(Access entity, AccessDto dto)
      {
        BeforeCopyAccess(entity, dto);
        CopyDBModelDtoBase(entity, dto);
        dto.Rule = entity.Rule;
        dto.FileId = entity.FileId;
        dto.FolderId = entity.FolderId;
        AfterCopyAccess(entity, dto);
      }
      
      private static void CopyAccess(AccessDto dto, Access entity)
      {
        BeforeCopyAccess(dto, entity);
        CopyDBModelDtoBase(dto, entity);
        entity.Rule = dto.Rule;
        entity.FileId = dto.FileId;
        entity.FolderId = dto.FolderId;
        AfterCopyAccess(dto, entity);
      }
      
      public static AccessDto AsDto(this Access entity)
      {
        AccessDto dto = new AccessDto();
        CopyAccess(entity, dto);
        return dto;
      }
      
      public static Access CopyTo(this AccessDto source, Access entity)
      {
        CopyAccess(source, entity);
        return entity;
      }


    }

  }
}

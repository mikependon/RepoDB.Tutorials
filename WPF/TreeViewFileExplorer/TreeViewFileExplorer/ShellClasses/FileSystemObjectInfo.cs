using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using TreeViewFileExplorer.Enums;

namespace TreeViewFileExplorer.ShellClasses
{
    public class FileSystemObjectInfo : BaseObject
    {
        public FileSystemObjectInfo(FileSystemInfo info)
        {
            if (this is DummyFileSystemObjectInfo) return;
            this.Children = new ObservableCollection<FileSystemObjectInfo>();
            this.FileSystemInfo = info;
            if (info is DirectoryInfo)
            {
                this.ImageSource = FolderManager.GetImageSource(info.FullName, ItemState.Close);
                this.AddDummy();
            }
            else if (info is FileInfo)
            {
                this.ImageSource = FileManager.GetImageSource(info.FullName);
            }
            this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FileSystemObjectInfo_PropertyChanged);
        }

        public FileSystemObjectInfo(DriveInfo drive)
            : this(drive.RootDirectory)
        {
        }

        #region Events

        public event EventHandler BeforeExpand;

        public event EventHandler AfterExpand;

        public event EventHandler BeforeExplore;

        public event EventHandler AfterExplore;

        private void RaiseBeforeExpand()
        {
            BeforeExpand?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseAfterExpand()
        {
            AfterExpand?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseBeforeExplore()
        {
            BeforeExplore?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseAfterExplore()
        {
            AfterExplore?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region EventHandlers

        void FileSystemObjectInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.FileSystemInfo is DirectoryInfo)
            {
                if (string.Equals(e.PropertyName, "IsExpanded", StringComparison.CurrentCultureIgnoreCase))
                {
                    RaiseBeforeExpand();
                    if (this.IsExpanded)
                    {
                        this.ImageSource = FolderManager.GetImageSource(this.FileSystemInfo.FullName, ItemState.Open);
                        if (this.HasDummy())
                        {
                            RaiseBeforeExplore();
                            this.RemoveDummy();
                            this.ExploreDirectories();
                            this.ExploreFiles();
                            RaiseAfterExplore();
                        }
                    }
                    else
                    {
                        this.ImageSource = FolderManager.GetImageSource(this.FileSystemInfo.FullName, ItemState.Close);
                    }
                    RaiseAfterExpand();
                }
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<FileSystemObjectInfo> Children
        {
            get { return base.GetValue<ObservableCollection<FileSystemObjectInfo>>("Children"); }
            private set { base.SetValue("Children", value); }
        }

        public ImageSource ImageSource
        {
            get { return base.GetValue<ImageSource>("ImageSource"); }
            private set { base.SetValue("ImageSource", value); }
        }

        public bool IsExpanded
        {
            get { return base.GetValue<bool>("IsExpanded"); }
            set { base.SetValue("IsExpanded", value); }
        }

        public FileSystemInfo FileSystemInfo
        {
            get { return base.GetValue<FileSystemInfo>("FileSystemInfo"); }
            private set { base.SetValue("FileSystemInfo", value); }
        }

        private DriveInfo Drive
        {
            get { return base.GetValue<DriveInfo>("Drive"); }
            set { base.SetValue("Drive", value); }
        }

        #endregion

        #region Methods

        private void AddDummy()
        {
            this.Children.Add(new DummyFileSystemObjectInfo());
        }

        private bool HasDummy()
        {
            return !object.ReferenceEquals(this.GetDummy(), null);
        }

        private DummyFileSystemObjectInfo GetDummy()
        {
            var list = this.Children.OfType<DummyFileSystemObjectInfo>().ToList();
            if (list.Count > 0) return list.First();
            return null;
        }

        private void RemoveDummy()
        {
            this.Children.Remove(this.GetDummy());
        }

        private void ExploreDirectories()
        {
            if (!object.ReferenceEquals(this.Drive, null))
            {
                if (!this.Drive.IsReady) return;
            }
            try
            {
                if (this.FileSystemInfo is DirectoryInfo)
                {
                    var directories = ((DirectoryInfo)this.FileSystemInfo).GetDirectories();
                    foreach (var directory in directories.OrderBy(d => d.Name))
                    {
                        if (!object.Equals((directory.Attributes & FileAttributes.System), FileAttributes.System) &&
                            !object.Equals((directory.Attributes & FileAttributes.Hidden), FileAttributes.Hidden))
                        {
                            var fso = new FileSystemObjectInfo(directory);
                            fso.BeforeExplore += Fso_BeforeExplore;
                            fso.AfterExplore += Fso_AfterExplore;
                            this.Children.Add(fso);
                        }
                    }
                }
            }
            catch
            {
                /*throw;*/
            }
        }

        private void Fso_AfterExplore(object sender, EventArgs e)
        {
            RaiseAfterExplore();
        }

        private void Fso_BeforeExplore(object sender, EventArgs e)
        {
            RaiseBeforeExplore();
        }

        private void ExploreFiles()
        {
            if (!object.ReferenceEquals(this.Drive, null))
            {
                if (!this.Drive.IsReady) return;
            }
            try
            {
                if (this.FileSystemInfo is DirectoryInfo)
                {
                    var files = ((DirectoryInfo)this.FileSystemInfo).GetFiles();
                    foreach (var file in files.OrderBy(d => d.Name))
                    {
                        if (!object.Equals((file.Attributes & FileAttributes.System), FileAttributes.System) &&
                            !object.Equals((file.Attributes & FileAttributes.Hidden), FileAttributes.Hidden))
                        {
                            this.Children.Add(new FileSystemObjectInfo(file));
                        }
                    }
                }
            }
            catch
            {
                /*throw;*/
            }
        }

        #endregion
    }
}

using System.IO;
using System.Windows;
using System.Windows.Input;
using TreeViewFileExplorer.ShellClasses;

namespace TreeViewFileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Get all drives
            var drives = DriveInfo.GetDrives();

            // Iterate each drive
            foreach (var drive in drives)
            {
                // Create an FSO
                var fileSystemObject = new FileSystemObjectInfo(drive);

                // Handle the events
                fileSystemObject.BeforeExplore += FileSystemObject_BeforeExplore;
                fileSystemObject.AfterExplore += FileSystemObject_AfterExplore;

                // Add the item
                this.treeView.Items.Add(fileSystemObject);
            }
        }

        private void FileSystemObject_AfterExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void FileSystemObject_BeforeExplore(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Wait;
        }
    }
}

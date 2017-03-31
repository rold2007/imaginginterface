namespace ImagingInterface.Controllers
{
    using ImagingInterface.Models;
    using ImagingInterface.Plugins;
    using Microsoft.Practices.ServiceLocation;

    public class FileOperationController
    {
        private FileOperationModel fileOperationModel;
        private ImageSourceManager imageSourceManager;

        public FileOperationController(FileOperationModel fileOperationModel, ImageSourceManager imageSourceManager)
        {
            this.fileOperationModel = fileOperationModel;
            this.imageSourceManager = imageSourceManager;
        }

        public void OpenFiles(string[] files)
        {
            if (files != null)
            {
                this.imageSourceManager.AddImageFiles(files);
            }
        }

        public void CloseFile(IImageSource imageSource)
        {
            if(imageSource != null)
            {
                this.imageSourceManager.RemoveImageSource(imageSource);
            }
        }

        public void CloseAllFiles()
        {
            ////if (this.CloseAllFiles != null)
            ////   {
            ////   this.CloseAllFiles(this, EventArgs.Empty);

            ////   GC.Collect();
            ////   }
        }

        public void RequestDragDropFile(string[] data)
        {
            this.OpenFiles(data);
        }
    }
}

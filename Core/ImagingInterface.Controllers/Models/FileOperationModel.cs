namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;

   public class FileOperationModel : IFileOperationModel
      {
      private IServiceLocator serviceLocator;

      public FileOperationModel(IServiceLocator serviceLocator)
         {
         this.serviceLocator = serviceLocator;
         }

      public IFileSource OpenFile(string file)
         {
         IFileSource fileSource = this.serviceLocator.GetInstance<IFileSource>();

         fileSource.SetImageSource(file);

         return fileSource;
         }

      public void CloseFile(IFileSource fileSourceController)
         {
         }
      }
   }

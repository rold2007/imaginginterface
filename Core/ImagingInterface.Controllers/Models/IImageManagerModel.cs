namespace ImagingInterface.Models.Interfaces
{
   public interface IImageManagerModel
   {
      int ActiveImageIndex
      {
         get;
         set;
      }

      int ImageCount
      {
         get;
      }

      int AddImage();

      void RemoveActiveImage();
   }
}

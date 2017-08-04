namespace ImagingInterface.Plugins
   {
   public interface IFileSource : IImageSource
   {
      string Filename
      {
         get;
      }

      bool LoadFile(string file);
   }
}

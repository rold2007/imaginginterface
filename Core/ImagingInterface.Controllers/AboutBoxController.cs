namespace ImagingInterface.Controllers
{
   using ImagingInterface.Models;

   public class AboutBoxController
   {
      public AboutBoxController(AboutBoxModel aboutBoxModel)
      {
         this.AboutBoxModel = aboutBoxModel;
      }

      public IAboutBoxModel AboutBoxModel
      {
         get;
         private set;
      }
   }
}

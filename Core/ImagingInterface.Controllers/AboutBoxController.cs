namespace ImagingInterface.Controllers
{
   using ImagingInterface.Models.Interfaces;

   public class AboutBoxController
   {
      private IAboutBoxModel aboutBoxModel;

      public AboutBoxController(IAboutBoxModel aboutBoxModel)
      {
         this.aboutBoxModel = aboutBoxModel;
      }

      public ProductInformations Product
      {
         get
         {
            ProductInformations productInformations = new ProductInformations()
            {
               ProductName = this.aboutBoxModel.ProductName,
               Version = this.aboutBoxModel.Version,
               Copyright = this.aboutBoxModel.Copyright,
               ProductDescription = this.aboutBoxModel.ProductDescription
            };

            return productInformations;
         }
      }

      public struct ProductInformations
      {
         public string ProductName;
         public string Version;
         public string Copyright;
         public string ProductDescription;
      }
   }
}

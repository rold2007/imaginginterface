namespace ImagingInterface.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public class AboutBoxView : IAboutBoxView
      {
      public IAboutBoxModel DisplayedModel
         {
         get;
         set;
         }

      public void Display(IAboutBoxModel aboutBoxModel)
         {
         this.DisplayedModel = aboutBoxModel;
         }
      }
   }

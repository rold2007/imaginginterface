﻿namespace ImagingInterface.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views;

   public class HelpOperationView : IHelpOperationView
      {
      public event EventHandler HelpAbout;

      public void TriggerHelpAbout()
         {
         this.HelpAbout(this, EventArgs.Empty);
         }
      }
   }

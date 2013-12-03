﻿namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IMainView
      {
      void AddImageManagerView(IImageManagerView imageManagerView);

      void AddPluginManagerView(IPluginManagerView pluginManagerView);
      }
   }

// <copyright file="ControllersBaseTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using SimpleInjector;

   public abstract class ControllersBaseTest
   {
      public ControllersBaseTest()
      {
         this.Container = new Container();
      }

      protected Container Container
      {
         get;
         private set;
      }
   }
}

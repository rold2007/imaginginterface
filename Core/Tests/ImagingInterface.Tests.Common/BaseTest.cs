namespace ImagingInterface.Tests.Common
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using NUnit.Framework;

   public abstract class BaseTest
      {
      private STASynchronizationContext staSynchronizationContext;

      [SetUp]
      protected virtual void SetUp()
         {
         this.staSynchronizationContext = new STASynchronizationContext();

         SynchronizationContext.SetSynchronizationContext(this.staSynchronizationContext);
         }

      [TearDown]
      protected void TearDown()
         {
         if (this.staSynchronizationContext != null)
            {
            this.staSynchronizationContext.Dispose();
            }

         this.staSynchronizationContext = null;

         SynchronizationContext.SetSynchronizationContext(null);
         }
      }
   }

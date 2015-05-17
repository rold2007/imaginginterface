namespace ImagingInterface.Tests.Common
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using NUnit.Framework;

   public abstract class BaseTest : IDisposable
      {
      private STASynchronizationContext staSynchronizationContext;

      ~BaseTest()
         {
         this.Dispose(false);
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      protected virtual void Dispose(bool disposing)
         {
         if (disposing)
            {
            this.ResetSynchronizationContext();
            }
         }

      [SetUp]
      protected virtual void SetUp()
         {
         this.staSynchronizationContext = new STASynchronizationContext();

         SynchronizationContext.SetSynchronizationContext(this.staSynchronizationContext);
         }

      [TearDown]
      protected void TearDown()
         {
         this.ResetSynchronizationContext();
         }

      private void ResetSynchronizationContext()
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

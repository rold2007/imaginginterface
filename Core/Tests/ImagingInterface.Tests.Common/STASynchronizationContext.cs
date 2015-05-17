namespace ImagingInterface.Tests.Common
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using NUnit.Framework;

   // Inspired from http://stackoverflow.com/a/11084309/263228
   public class STASynchronizationContext : SynchronizationContext, IDisposable
      {
      private readonly Control control;
      private readonly int mainThreadId;

      public STASynchronizationContext()
         {
         this.control = new Control();

         this.control.CreateControl();

         this.mainThreadId = Thread.CurrentThread.ManagedThreadId;

         if (Thread.CurrentThread.Name == null)
            {
            Thread.CurrentThread.Name = "AsynchronousTestRunner Main Thread";
            }
         }

      ~STASynchronizationContext()
         {
         this.Dispose(false);
         }

      public override void Post(SendOrPostCallback d, object state)
         {
         this.control.BeginInvoke(d, new object[] { state });
         }

      public override void Send(SendOrPostCallback d, object state)
         {
         this.control.Invoke(d, new object[] { state });
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      protected virtual void Dispose(bool disposing)
         {
         Assert.AreEqual(this.mainThreadId, Thread.CurrentThread.ManagedThreadId);

         if (disposing)
            {
            if (this.control != null)
               {
               this.control.Dispose();
               }
            }
         }
      }
   }

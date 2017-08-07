// <copyright file="BaseTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Tests.Common
{
   using System;
   using System.Threading;
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

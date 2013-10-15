namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;
   using SimpleInjector;

   public abstract class ControllersBaseTests
      {
      private SimpleInjectorServiceLocatorAdapter simpleInjectorServiceLocatorAdapter;

      public ControllersBaseTests()
         {
         ServiceLocator.SetLocatorProvider(this.GetServiceLocator);
         }

      protected Container Container
         {
         get;
         private set;
         }

      [SetUp]
      protected void Bootstrap()
         {
         this.Container = new Container();

         this.simpleInjectorServiceLocatorAdapter = new SimpleInjectorServiceLocatorAdapter(Container);
         }

      private IServiceLocator GetServiceLocator()
         {
         return this.simpleInjectorServiceLocatorAdapter;
         }
      }
   }

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
      protected Container container;
      private SimpleInjectorServiceLocatorAdapter simpleInjectorServiceLocatorAdapter;

      public ControllersBaseTests()
         {
         ServiceLocator.SetLocatorProvider(this.GetServiceLocator);
         }

      [SetUp]
      protected void Bootstrap()
         {
         this.container = new Container();

         this.simpleInjectorServiceLocatorAdapter = new SimpleInjectorServiceLocatorAdapter(container);
         }

      private IServiceLocator GetServiceLocator()
         {
         return this.simpleInjectorServiceLocatorAdapter;
         }
      }
   }

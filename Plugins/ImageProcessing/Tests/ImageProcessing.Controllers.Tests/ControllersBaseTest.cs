namespace ImageProcessing.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ImageProcessing.Controllers;
    using ImageProcessing.Models;
    using ImageProcessing.ObjectDetection;
    using ImagingInterface.Controllers;
    using ImagingInterface.Models;
    using ImagingInterface.Plugins;
    using ImagingInterface.Tests.Common;
    using ImagingInterface.Tests.Common.Mocks;
    using NUnit.Framework;
    using SimpleInjector;

    public abstract class ControllersBaseTest : BaseTest
    {
        public Container Container
        {
            get;
            private set;
        }

        [SetUp]
        protected override void SetUp()
        {
            base.SetUp();

            this.Bootstrap();
        }

        protected void Bootstrap()
        {
            this.Container = new Container();

            // This is needed for some tests to register a mock class
            this.Container.Options.AllowOverridingRegistrations = true;

            // Service
            this.Container.RegisterSingleton<IServiceLocator, SimpleInjectorServiceLocatorAdapter>();

            // Views
            ////this.Container.RegisterSingleton<IMainView, MainView>();
            ////this.Container.RegisterSingleton<IImageManagerView, ImageManagerView>();
            ////this.Container.Register<IInvertView, InvertView>();
            ////this.Container.Register<IRotateView, RotateView>();
            ////this.Container.Register<ICudafyView, CudafyView>();
            ////this.Container.Register<IImageView, ImageView>();
            ////this.Container.RegisterSingleton<ITaggerView, TaggerView>();
            ////this.Container.RegisterSingleton<IObjectDetectionView, ObjectDetectionView>();
            ////this.Container.Register<IObjectDetectionManagerView, ObjectDetectionManagerView>();

            // Controllers
            this.Container.Register<MainController>();
            this.Container.Register<ImageManagerController>();
            this.Container.Register<InvertController>();
            this.Container.Register<RotateController>();
            this.Container.Register<CudafyController>();
            this.Container.Register<ImageController>();
            ////this.Container.Register<IImageSourceController, ImageSourceController>();
            ////this.Container.Register<IFileSourceController, FileSourceController>();
            ////this.Container.Register<IMemorySourceController, MemorySourceController>();
            this.Container.Register<TaggerController>();
            this.Container.Register<ObjectDetectionController>();
            this.Container.Register<ObjectDetectionManagerController>();

            // Models
            this.Container.Register<IInvertModel, InvertModel>();
            this.Container.Register<IRotateModel, RotateModel>();
            this.Container.Register<ICudafyModel, CudafyModel>();
            this.Container.Register<IImageModel, ImageModel>();
            ////this.Container.Register<IFileSourceModel, FileSourceModel>();
            ////this.Container.Register<IMemorySourceModel, MemorySourceModel>();
            this.Container.RegisterSingleton<ITaggerModel, TaggerModel>();
            this.Container.RegisterSingleton<IObjectDetectionModel, ObjectDetectionModel>();
            this.Container.Register<IObjectDetectionManagerModel, ObjectDetectionManagerModel>();

            // Processing
            this.Container.RegisterSingleton<IObjectDetector, ObjectDetector>();
            this.Container.RegisterSingleton<ITagger, Tagger>();
        }
    }
}

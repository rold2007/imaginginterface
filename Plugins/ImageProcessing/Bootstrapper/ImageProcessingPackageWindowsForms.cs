namespace ImageProcessing.Bootstrapper
{
    using ImageProcessing.Controllers;
    using ImageProcessing.Models;
    using ImageProcessing.ObjectDetection;
    using ImageProcessing.Views;
    using ImagingInterface.BootStrapper;
    using ImagingInterface.Plugins;
    using SimpleInjector;
    using SimpleInjector.Diagnostics;

    public class ImageProcessingPackageWindowsForms : IPackageWindowsForms
    {
        public void RegisterServices(Container container)
        {
            // Factories
            container.RegisterSingleton<IFileSourceFactory, FileSourceFactory>();

            // Models
            container.Register<RotateModel>();
            container.Register<InvertModel>();
            container.Register<CudafyModel>();
            container.Register<TaggerModel>();
            container.Register<ObjectDetectionManagerModel>();
            container.Register<ObjectDetectionModel>();

            // Controllers
            container.Register<RotateController>();
            container.Register<InvertController>();
            container.Register<IFileSource, ImageProcessorFileSource>();
            container.Register<CudafyController>();
            container.Register<IMemorySource, MemorySource>();
            container.Register<TaggerController>();
            container.Register<ObjectDetectionManagerController>();
            container.Register<ObjectDetectionController>();

            // Views
            container.Register<RotateView>();
            container.Register<InvertView>();
            container.Register<CudafyView>();
            container.Register<TaggerView>();
            container.Register<ObjectDetectionManagerView>();
            container.Register<ObjectDetectionView>();

            // ObjectDetection
            container.Register<IObjectDetector, ObjectDetector>();
            container.Register<ITagger, Tagger>();
        }

        public void SuppressDiagnosticWarning(Container container)
        {
            ////container.GetRegistration(typeof(ICudafyController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(CudafyController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(CudafyView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(InvertView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(ObjectDetectionView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(IObjectDetector)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(ObjectDetector)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(ObjectDetectionManagerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(TaggerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
            container.GetRegistration(typeof(RotateView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
        }
    }
}

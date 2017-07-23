namespace ImagingInterface.Plugins
{
    public interface IPluginView : IRawPluginView
   {
        string DisplayName
        {
            get;
        }

        bool Active
        {
            get;
        }

        void Hide();

        void Close();
    }
}

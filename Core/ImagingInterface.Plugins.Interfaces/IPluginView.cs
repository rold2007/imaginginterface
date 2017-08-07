// <copyright file="IPluginView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

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

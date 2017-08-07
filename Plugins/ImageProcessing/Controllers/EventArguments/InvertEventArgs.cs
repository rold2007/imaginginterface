// <copyright file="InvertEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.EventArguments
{
   using System;

   public class InvertEventArgs : EventArgs
      {
      public InvertEventArgs(bool invert)
         {
         this.Invert = invert;
         }

      public bool Invert
         {
         get;
         private set;
         }
      }
   }

// <copyright file="ProductInformations.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Models
{
   using System;

   public struct ProductInformations : IEquatable<ProductInformations>
   {
      public string ProductName
      {
         get;
         set;
      }

      public string Version
      {
         get;
         set;
      }

      public string Copyright
      {
         get;
         set;
      }

      public string ProductDescription
      {
         get;
         set;
      }

      public static bool operator ==(ProductInformations productInformations1, ProductInformations productInformations2)
      {
         return productInformations1.Equals(productInformations2);
      }

      public static bool operator !=(ProductInformations productInformations1, ProductInformations productInformations2)
      {
         return !productInformations1.Equals(productInformations2);
      }

      public override int GetHashCode()
      {
         return this.ProductName.GetHashCode();
      }

      public override bool Equals(object obj)
      {
         if (!(obj is ProductInformations))
         {
            return false;
         }

         return this.Equals((ProductInformations)obj);
      }

      public bool Equals(ProductInformations other)
      {
         return (this.ProductName == other.ProductName) &&
            (this.Version == other.Version) &&
            (this.Copyright == other.Copyright) &&
            (this.ProductDescription == other.ProductDescription);
      }
   }
}

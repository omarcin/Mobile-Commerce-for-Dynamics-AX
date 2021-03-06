﻿using mAxCommerce.WindowsPhone.Resources;

namespace mAxCommerce.WindowsPhone
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static readonly AppResources localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return localizedResources; } }
    }
}
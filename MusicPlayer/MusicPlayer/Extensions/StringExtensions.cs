using System;

/*
Copyright 2016 Oliver Klesing

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

Author: Oliver Klesing
Date: 4/27/16
*/
namespace MusicPlayer.Extensions
{
    public static class StringExtensions
    {
        /* This method is a convenience method that adds functionality to a string.
         * The equals method in the string class does not allow for comparisons 
         * that ignore the case. The index of method does allow for such comparisons.
         * If items are not equal, IndexOf returns a -1.
         */
        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
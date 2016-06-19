using System;
using System.Collections.Generic;

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
    /* This class contains custom extensions to the IList interface.
     * These extensions can be used by any class implementing the 
     * IList interface. These methods provide additional functionality
     * to collections used throughout the application. 
     */
    public static class ListExtensions
    {
        private static Random random = new Random();

        /* The shuffle method is implemented using the  
         * Fisher-Yates shuffle. (https://en.wikipedia.org/wiki/Fisher–Yates_shuffle)
         * This method swaps each item with a random item in the list.
         */
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> list)
        {
            foreach (T item in list)
            {
                collection.Add(item);
            }
        }

        public static void RemoveAll<T>(this IList<T> collection, Func<T, bool> condition)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (condition(collection[i]))
                {
                    collection.RemoveAt(i);
                }
            }
        }
    }
}
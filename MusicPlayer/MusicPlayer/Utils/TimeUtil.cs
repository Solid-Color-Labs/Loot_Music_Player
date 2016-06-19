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
namespace MusicPlayer.Utils
{
    /* This class is a utility class 
     * for dealing with timespan objects.
     */
    public static class TimeUtil
    {
        // To store a timespan object, it is converted to a long.
        public static long ConvertTimeToTicks(TimeSpan time)
        {
            return time.Ticks;
        }

        public static TimeSpan ConvertTicksToTimeSpan(long ticks)
        {
            return TimeSpan.FromTicks(ticks);
        }

        public static string FormatTimeSpan(TimeSpan time)
        {
            return time.ToString(@"m\:ss");
        }

        public static string FormatTimeSpan(long ticks)
        {
            TimeSpan time = ConvertTicksToTimeSpan(ticks);
            return time.ToString(@"m\:ss");
        }

        public static string FormatTimeSpan(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"mm\:ss");
        }
    }
}
   M S B A M L   `   `   `  ����F  BMusicPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MusicPlayer.ConvertersMusicPlayer.Converters  5      +    %MusicPlayer.View.CurrentlyPlayingView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2c$clr-namespace:MusicPlayer.Converters     5      $	��300��5   3     � DataContext  5      �� *CurrentlyPlayingViewModel+   Locator �� Source# [   � 	Resources 5      8   2MusicPlayer.Converters.PositiveToNegativeConverter  PositiveToNegativeConverter��%   &	         5   
   6(   5      .��5!      ��  �� Orientation$ Vertical=�6   .+�5"   
   �� -   5         ��Name$ 
AlbumImage��5"      $	��240��5#      $
��Auto��6   $	��Top=�5%      $��Uniform=�5&      ��5$      �� *
AlbumImage+5&      �� 5'   
   -   5         ��Name$ ArtistLabel��5'      $��30��5(      $
��Auto��6   $
��Left=�5)      $��Center=�5*      $
��Left=�5+      $��15,0,0,0q�5,      ���	#FFEEEEEE$��16(�5.      $��Arial)�6   ��5'   %   �� *TrackArtist+z�5/      T� 50      -   5        	Translate��50   %    � Triggers 52      :� 53       :� RoutedEvent$ Label.MouseEnter��6   .[�54      �� .��55      ��  g� AutoReverse . .��56      W� 	 W� From$	 0X�57      
 �� 
TargetName$
 	Translate��59       �� TargetProperty$ X�5:       g� Duration$ 0:0:8D�5;      	 W� To 58      ��  �� ElementName$ ArtistLabel�� �� Path$ ActualWidth� �� 	Converter# [ 5;      5<      5=      5>      5@      �� 5A   
   -   5        $ 	SongLabel��5A      $��30��5B      $
��Auto��6   $
��Left=�5C      $��Center=�5D      $��15,0,0,0q�5E      ���	#FFEEEEEE$��16(�5G      $��Arial)�6   $
��Left=�61   ��5A   #   �� *
TrackTitle+z�5H      T� 5I      -   5        TranslateTransform��5I   %    5K      :� 5L      $ Label.MouseEnter��6   .[�5M      �� .��5N      ��  . .��5O      W� $	 0X�5P      $
 TranslateTransform��5R      $ X�5S      $ 0:0:8D�5T       5Q      �� $ 	SongLabel��$ ActualWidth�# [ 5T      5U      5V      5W      5Y      5Z      5[      
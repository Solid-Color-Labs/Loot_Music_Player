   M S B A M L   `   `   `  ����  MusicPlayer#    MusicPlayer.View.PlaylistView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      f bGalaSoft.MvvmLight.Platform, Version=5.2.0.37226, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      $
��Auto��5
      $
��Auto��6     � DataContext  5      �� *PlaylistViewModel+   Locator �� Source# [  .��5       �  � RowDefinitions 5!   
   �� 5"      $
��265* �6   �� 5#      $��60 �6   .+�5&   
   �� -   5         ��Name$ PlaylistListView��5&      $
��Left=�5'      $	��242��5(      $��Disabled=�5)      $��0q�5.      $��0,25,0,0q�5/      $��Arial)�50      $��16(�6&   ��5*      ��  �� Path$ ObservablePlaylists� �� SelectedIndex 5+      �� *SelectedPlaylistIndex+ �� Mode$ TwoWay=� �� SelectedItem 5,      �� *SelectedPlaylistName+$ OneWayToSource=�
 � Tag 5-      �� 	 �� RelativeSource	 �� 
 �� Mode$
 FindAncestor=� �� AncestorType" >�.  (System.Windows.Interactivity.Interaction   Triggers 51      /  )System.Windows.Interactivity.EventTrigger  52         	EventName$ SelectionChanged��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 53      /  )GalaSoft.MvvmLight.Command.EventToCommand     Command 6-   �� *SetCurrentPlaylistCommand+54       � ContextMenu 56      �� 57      .@�58      m� $��Delete��6    m� Command 59      �� *DeletePlaylistCommand+5:      5;       �� View 5=      �� 5>      .P�5?      �� $��	PLAYLISTS��6%    �� Width$	 250��68   5@      5B      � 5D   
   -   5        $��PlaylistPopup��5I      $	��Top=�5D       � 	StaysOpen .  $��0��5G      $��0��5H       � PlacementTarget 5E      ��  �� ElementName$ AddPlayListButton��.��5J      �� -   5         ��Name$ PlaylistTextBox��5J      $
��Left=�5L      $��Bottom=�5M      $��27��5N      $	��240��6"    �� TextWrapping$
 Wrap=�5O      ���#FFFFFF$��16(�5Q      
���   �
������ $��0q�5T      ��5K      �� *PlaylistTextBoxText+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . .��5U   4    J� InputBindings 5V      �� 5W      
 �� Key$ Enter��6!    �� Command 6-   �� *CreatePlaylistCommand+.��5X   )   5Y      5Z      �� 5\   
   -   5         ��Name$ AddPlayListButton��5\      $��0��5]      $��60��5^      $
��Auto��5_      $��Center=�5`        SquareButtonStyle#���  5b      $��	0,265,0,0q�6=   $��0��6P   $��2��6]   .��5c      �� $��Center=�6    �� Orientation$ 
Horizontal=�66   $	��242��6O   $��50��6[   .+�5d      �� $��/Resources/plus_icon.png��6   $��5,0,0,0q�6:   $��46��6K   �� 5e      $��5,0,0,0q�6   $	��180��5f      $��50��5g      $��18(�5h      $��New Playlist��5i      $
��Left=�5j      $��Center=�5k      $��Bottom=�5l      $
��Left=�5m      5n      5o      �� 5q   
   -   5        $ AllTracksButton��5q      $��0��5r      $��Songs��5s      $
��Left=�5u      $	��Top=�5v      $	��242��6)   $��0q�5w      $
��Left=�5x      ��5t      �� *SetAllTracksCommand+5x      5y      5z      
   M S B A M L   `   `   `  ����F  BMusicPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MusicPlayer.ViewMusicPlayer.View  5   	       MusicPlayer.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MusicPlayer.View     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   f bGalaSoft.MvvmLight.Platform, Version=5.2.0.37226, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5	   	     � Title$
  Loot��5   	    � WindowStartupLocation$ CenterScreen=�5   	      AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   5   	   #��Z��5   	   #��Z��6B   ���	#FF282828
������� � Topmost .  � DataContext 5   	   �� *MainWindowViewModel+  Locator �� Source# [ .  (System.Windows.Interactivity.Interaction   Triggers 5)      /  )System.Windows.Interactivity.EventTrigger  5*   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5+      /  )GalaSoft.MvvmLight.Command.EventToCommand  	   Command	 6%   �� *LoadCommand+5,      .��5/      � 
 � ColumnDefinitions
 50   
   �� 51      $
��137* �6   �� 52      $
��713* �6    � RowDefinitions 54   
   �� 55      $	��35* �6   �� 56      $
��777* �6   �� 57      $	��73* �6   .+�5:   
      MusicPlayer.View.Header  $��	0,0,168,0q�6   $��25��6*   $
��Auto��66   $	��Top=�6C   $   MusicPlayer.View.SearchBoxView  5<   
   $��1��6   $��0��6.   $��501,0,0,392q�6;   $��2��6P   $
��Auto��6a   #   MusicPlayer.View.PlaylistView  5>   
   $��	0,0,3,322q�6   $��0��60   $��1��6@   +	   %MusicPlayer.View.CurrentlyPlayingView	  5@   
   $��	0,438,0,0q�6%   $��1��68   $��0��6E   $
��Auto��6U   $
��Left=�6c   $	��274��6~   '
   !MusicPlayer.View.MusicControlView
  5B   
   $��2��6!   $��0��6.   $��0,0,3,0q�6>   '   !MusicPlayer.View.TrackControlView  5D   
   $��0,0,0,0q�6!   $��2��62   $��1��6?   !   MusicPlayer.View.TracksView  5F   
   $��1��6   $��1��6+   $��2,0,0,10q�68   5H      5I      
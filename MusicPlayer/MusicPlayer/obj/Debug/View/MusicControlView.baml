   M S B A M L   `   `   `  ����  MusicPlayer'    !MusicPlayer.View.MusicControlView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      f bGalaSoft.MvvmLight.Platform, Version=5.2.0.37226, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5        � DataContext  5
      �� *MusicControlViewModel+   Locator �� Source# [  .��5      �� $
��Left=�6    �� Orientation$ 
Horizontal=�5       $	��200��5!      .+�5#   
   �� -   5        $��PreviousButton��5#      $
��Left=�5$      $��60��5&      $��0q�5'      
������ ��5%      �� *PreviousTrackCommand+.��5*      �� 
���   �$��62��6%   $��50��61    J� OpacityMask 5+      !� 5,         appbar_control_skip_backward !� Visual# [ 6"    j� Stretch$ Uniform=�6Y   5.      5/      d� 50   
   -   5         d�Name$ PlayPauseToggle��50      $	��Top=�51      $��Center=�6&   {�. $��80��54      $
��Auto��6   
������  d� 	IsChecked 52      �� *	IsChecked+ �� Mode$ TwoWay=�.  (System.Windows.Interactivity.Interaction	   Triggers	 56      /  )System.Windows.Interactivity.EventTrigger  57      
   	EventName$
 Checked��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 58      /  )GalaSoft.MvvmLight.Command.EventToCommand     Command 6-   �� *PlayTrackCommand+59        5:      $
 	Unchecked��6!   . 5;         6-   �� *PauseTrackCommand+5<        5>      $
 Click��6!   . 5?         6-   �� *PlayTrackWhenStoppedCommand+5@       � 	Resources 5B        PlayIcon  	PauseIcon��%y   &	       &	 2     �� 5C      $��/Resources/play_icon.png��6)   �� 5D      $��/Resources/pause_icon.png��6*   5E      ��5F      �� 5G       �� 
TargetType" d�6    �� Triggers 5H      P� 5I       P� Property �@d�	IsChecked P� Value$
 TrueY�67   . �5J      ��  �� Property � �� �� Value# [ 68   5K      P� 5L       �@d�	IsChecked$ FalseY�67   . �5M      ��  � ��# [ 68   5N      5P      5R      �� 5T   
   -   5        $��
NextButton��5T      $��60��5V      $
��Auto��6   $��0q�5W      
������ ��5U      �� *NextTrackCommand+.��5Y      �� 
���   �$��62��6%   $��50��61    5Z      !� 5[        appbar_control_skip_forward# [ 6"   $ Uniform=�6X   5]      5^      5`      5a      
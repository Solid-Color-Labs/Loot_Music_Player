   M S B A M L   `   `   `  ����  MusicPlayer'    !MusicPlayer.View.TrackControlView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      f bGalaSoft.MvvmLight.Platform, Version=5.2.0.37226, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5        � DataContext  5
      �� *TrackControlViewModel+   Locator �� Source# [  .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5    
   �� -   5         ��Name$ 	ZeroLabel��5       $��Center=�5"      $��Center=�6+   ���	#FFEEEEEE$��30��5$      $��60��6   $
��0,21q�5%      $��Arial)�5&      ��5!      �� *CurrentFormattedPosition+5&      �� 5'   
   -   5         ��Name$ TrackProgress��5'      $��Center=�5(   
    � Minimum$ 0Q�5)   
    �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�5,   
    �� IsMoveToPointEnabled . $��18��5.   
   $	��896��6   $��Center=�5/   
   $
��0,27q�6'   	 � Maximum	 5)      �� *TrackDuration+
 � Value
 5*   
   �� *CurrentPosition+ �� Mode$ TwoWay=� �� Delay$ 1��.  (System.Windows.Interactivity.Interaction   Triggers 50      /  )System.Windows.Interactivity.EventTrigger  51         	EventName$ PreviewMouseUp��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 52      /  )GalaSoft.MvvmLight.Command.EventToCommand     Command 6-   �� *TrackPositionCommand+53      55      �� 56   
   -   5        $ DurationLabel��56      $��Center=�58      ���	#FFEEEEEE$��60��5:      $��30��6   $��
10,21,0,21q�5;      $��Center=�5<      $��Arial)�5=      $��Center=�6   ��57      �� *TrackFormattedDuration+5=      d� 5>   
   -   5         d�Name$ RepeatToggle��5>      $��Center=�5?   
   $��Center=�6%   
������ $��59��5A   
   $��50��6   $
��0,11q�6!    � 	Resources 5B        
RepeatIcon  RepeatSelectedIcon��%�   &	       &	 4     �� 5C      $ ��/Resources/repeat_icon.png��6+   �� 5D      $)��#/Resources/repeat_selected_icon.png��63   5E      ��5F      �� 5G       �� 
TargetType" d�6   .�5H      ��  �� Property � �� �� Value 5I      �� 5J       �� 
TargetType" d�6.   .R�5K   "   �� 
������ ._�5L   &   �� 5M   #   5N      5P       �� Triggers 5Q      P� 5R       P� Property �@d�	IsChecked P� Value$
 TrueY�67   . �5S      ��  � ��# [ 68   �� 5T       � �� 68   �� *RepeatOnCommand+5U      P� 5V       �@d�	IsChecked$ FalseY�67   . �5W      ��  � ��# [ 68   �� 5X       � �� 68   �� *RepeatOffCommand+5Y      5[      5]      d� 5^   
   -   5        $ ShuffleToggle��5^      $��Center=�6.   
������ $��57��5`   
   $��50��6    5a        ShuffleIcon  ShuffleSelectedIcon��%�   &	       &	 5     �� 5b      $!��/Resources/shuffle_icon.png��6,   �� 5c      $*��$/Resources/shuffle_selected_icon.png��64   5d      ��5e      �� 5f      " d�6   .�5g      ��  � �� 5h      �� 5i      " d�6.   .R�5j   "   �� 
������ ._�5k   &   �� 5l   #   5m      5o       5p      P� 5q       �@d�	IsChecked$
 TrueY�67   . �5r      ��  � ��# [ 68   �� 5s       � �� 68   �� *ShuffleOnCommand+5t      P� 5u       �@d�	IsChecked$ FalseY�67   . �5v      ��  � ��# [ 68   �� 5w       � �� 68   �� *ShuffleOffCommand+5x      5z      5|      5}      5~      
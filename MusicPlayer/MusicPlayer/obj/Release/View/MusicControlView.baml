   M S B A M L   `   `   `  ����   MusicPlayer'    !MusicPlayer.View.MusicControlView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � DataContext  �� *MusicControlViewModel+   Locator �� Source# [  .���� $
��Left=� �� Orientation$ 
Horizontal=�$	��200��.+��� -   $��PreviousButton��$
��Left=�$��60��$��0q�
������ ���� *PreviousTrackCommand+.���� 
���   �$��62��$��50�� J� OpacityMask !�    appbar_control_skip_backward !� Visual# [  j� Stretch$ Uniform=�d� -    d�Name$ PlayPauseToggle��$	��Top=�$��Center=�$��80��$
��Auto��
������ ���� *PlayTrackWhenStoppedCommand+ d� 	IsChecked �� *	IsChecked+ �� Mode$ TwoWay=�	 � 	Resources	   PlayIcon  	PauseIcon��%]   &	       &	 $     �� $��/Resources/play_icon.png���� $��/Resources/pause_icon.png������ 
 �� 
TargetType"
 d� �� Triggers P�  P� Property �@d�	IsChecked P� Value$
 TrueY�. ���  �� Property � �� �� Value# [ ��  � �� �� *PlayTrackCommand+P�  �@d�	IsChecked$ FalseY�. ���  � ��# [ ��  � �� �� *PauseTrackCommand+�� -   $��
NextButton��$��60��$
��Auto��$��0q�
������ ���� *NextTrackCommand+.���� 
���   �$��62��$��50�� !�   appbar_control_skip_forward# [ $ Uniform=�
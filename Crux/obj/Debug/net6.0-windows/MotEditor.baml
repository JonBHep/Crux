   M S B A M L   `   `   `  ����  Crux    Crux.MotEditor   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � Title$  Password editor��5   	   $��Consolas)�6!   $	��800��67   $
��1200��6D    � WindowStartupLocation$ CenterScreen=�6Q    � ShowInTaskbar$ False��5	       .��5
      �  � RowDefinitions 5   
   �� 5      $��32 �6   �� 5      $��* �6   �� 5      $��32 �6   �� 5      $��3* �6   �� 5      $��32 �6   �� 5      $��32 �6   �� 5      $��32 �6   �� 5      $��32 �6   �� 5      $��32 �6   �� 5      $��38 �6   .+�5   
   ]� .+�5      �� $
��Left=�6   $	��4 0q�6.   $��Center=�6;   Titles6^   �� 5      -   5         ��Name$ AddTitleButton��5      $��Right=�6-   $	��6 0q�6D   $��Center=�6Q   $��60��6l   $��23��6w   .��6�   Add6�   �� 5      -   5         ��Name$ TextboxTitle��5      $��4q�6,   $��4q�67   $��Center=�6C   $��Lucida Console)�6^   6z   5      ]� 5   
   $��1��6   .+�5      �� -   5        $ RemoveTitleButton��5      $��Right=�60   $	��6 0q�6G   $��Center=�6T   $��60��6o   $��23��6z   .��6�   Remove6�   �� 5      -   5         ��Name$ TitlesListBox��5      $��4q�6-   6l   5      �� 5    
   $��2��6   $	��4 0q�6!   $��Center=�6.   
Data items6U   �� 5!   
   -   5        $ PasswordListBox��5!      $��3��6+   $��4q�68   6y   �� 5"   
   $��4��6    �� Orientation$ 
Horizontal=�6"   .+�5#      �� -   5        $ ButtonMoveUp��5#      $��4q�6+   $��4q�66   $��Center=�6B   $��80��6]   $��Move up��6�   �� 5$      -   5        $ ButtonDelete��5$      $��4q�6+   $��4q�66   $��Center=�6B   $��80��6]   $��Delete��6�   �� 5%      -	   5        $ 
ButtonEdit��5%      $��4q�6)   $��4q�64   $��Center=�6@   $��80��6[   $
��Edit��6f   6u   5&      ]� 5'   
   $��5��6   .+�5(      �� $
��Left=�6   $��4q�6.    �� Padding$ 4q�69   $��Center=�6E   $��60��6`   �� 6l   $�Caption��6p   6�   �� 5)      -
   5        $��Right=�5)      $��4q�6,   $��4q�67   $��Center=�6C   $"��Insert Hotmail email address��6z   $|�False��6�   �� 5*      -   5        	 ��Name$	 ComboboxKey��5*      $��4q�6,   $��4q�67   $��Center=�6C   
 �� 
IsEditable$

 True��6^   $��Lucida Console)�6p   6�   5+      ]� 5,   
   $��6��6   .+�5-      �� $
��Left=�6   $��4q�6.   $ 4q�69   $��Center=�6E   $��60��6`   �� 6l   $�Value��6p   6�   �� 5.      -   5        $��Right=�5.      $��4q�6,   $��4q�67   $��Center=�6C   $��Generate a password��6�   $|�False��6�   �� 5/      -   5        $ TextboxValue��5/      $��4q�6,   $��4q�67   $��Center=�6C   $��Center=�6^   $��Lucida Console)�6�   6�   50      ]� 52   
   $��7��6   $��False��6!   .+�53      �� -   5         ��Name$ LinkCheckBox��53       d� IsThreeState$ False��6-   $
��Left=�6B   $��72 6 0 6q�6X   $��Center=�6j   $��Center=�6�   $��Weblink��6�   �� 54      $��8 0 0 0q�6   
����=H�$��Center=�6D   �� 6`   $[�UTick 'Weblink' for a web address which can be launched when viewing the Password file��6d   6�   55      �� 57   
   -   5        $ 	ButtonAdd��57      $��8��6$   $��72 0 0 0q�61   $��4q�6C   $
��Left=�6O   $��Center=�6j   $��80��6�    �� 	IsDefault$
 True��6�   $	��Add��6�   ]� 59   
   $��9��6   $��False��6!   .+�5:      �� -   5        $ 
SaveButton��5:      $��Right=�6)      ButtonClose#��[  6@   $��4 4 4 8q�6e   $��Center=�6v   6�   �� 5;      -   5        $��Right=�5;      $��4 4 4 8q�6,   $��4q�6=   $��Center=�6I   $��80��6d   $��Cancel��6�   �� 5<      -   5        $ CheckboxFavourite��5<      $ False��62   $��Right=�6G   $��0 0 8 0q�6^   $��Center=�6o   $��Center=�6�   $��	Favourite��6�   5=      5>      5@      
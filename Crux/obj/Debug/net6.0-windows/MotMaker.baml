   M S B A M L   `   `   `  ����  Crux    Crux.MotMaker   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � Title$  Password generator��5   	   $	��280��6$   $	��600��61    � 
ResizeMode$ NoResize=�6=    � WindowStartupLocation$ CenterScreen=�6S    � ShowInTaskbar$ False��6�    � SizeToContent$ Height=�6�   .��5      �  � RowDefinitions 5	   
   �� 5
      $��10 �6   6*   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��24 �6   �� 5      $��32 �6   �� 5      �� 5      �� 5      $��10 �6   6*   .+�5   
   �� $��1��6   $��8 0 0 0q�6!   	Include6;   �� 5   
   -   5         ��Name$ UpperCaseCheckBox��5       $��2��6   $	��8 0q�6;   $��Center=�6H   $��Center=�6c    d� IsThreeState$ False��6�    d� 	IsChecked$
 TrueY�6�   $��Upper case letters��6�   �� 5   
   -   5        $ LowerCaseCheckBox��5      $��3��6.   $	��8 0q�6;   $��Center=�6H   $��Lower case letters��6c   $ False��6�   $
 TrueY�6�   �� 5   
   -   5        $ NumberCheckBox��5      $��4��6+   $	��8 0q�68   $��Center=�6E   $ False��6`   $
 TrueY�6u   $��Numbers��6�   �� 5   
   -   5        $ SymbolCheckBox��5      $��5��6+   $	��8 0q�68   $��Center=�6E   $ False��6`   $
 TrueY�6u   $��Symbols��6�   �� 5   
   $��6��6   	 �� Orientation$	 
Horizontal=�6"   .+�5      �� -   5        
 ��Name$
 
NumTextBox��5      $	��8 0q�6*   $��50��67   $��0��6B   $��Center=�6K   6f   �� 5      -   5        $��6,6,2,6q�5      $��˄��6&   $��20��62   6=   �� 5      -   5        $��2,6,0,6q�5      $��˅��6'   $��20��63   6>   5      �� 5   
   -	   5        $��7��5      $	��8 4q�6   $
��12 4q�6+   $��80��6:   $��Center=�6E   $
��Left=�6`   $��Generate��6�   ]� 5    
   $��8��6   $��False��6!   .+�5!      �� -
   5         ��Name$ PasswordTextBlock��5!      $	��8 4q�63    �� Padding$	 8 5q�6@   $��Lucida Console)�6N   $��14(�6j   $
��Bold"�6x   
���  ��
�������$��Center=�6�   $	��144��6�   �� 6�   $�Password��6�   6�   �� 5"      -   5         ��Name$ 
CopyButton��5"      $	��8 0q�6)   $
��12 4q�66   $��Center=�6E   $��80��6`   $
��Copy��6�   �� 5#      -   5        $ 	UseButton��5#      $	��8 0q�6(   $
��12 4q�65   $��Center=�6D   $
��Bold"�6_   $��80��6q   $	��Use��6�   �� 5$      $��Right=�6   $	��8 0q�6,   $
��12 4q�69   $��80��6H   $��Center=�6S    �� IsCancel$
 True��6n   $��Cancel��6~   5%      5&      5'      
   M S B A M L   `   `   `  ����  Crux    Crux.CardsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � Title$  Credit and debit cards��5   	   $	��450��6(   $	��800��65   
������� � ShowInTaskbar$ False��6T    � WindowStartupLocation$ CenterScreen=�6j    � 
ResizeMode$ NoResize=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      $��38 �6   6*   �� 5      $��38 �6   6*   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      6%   �� 5      $��80 �6#   60   .+�5      �� -   5         ��Name$ CardsListBox��5      $��4q�6,   6j   ]� 5      $��1��6   $��False��6(   .+�5      �� -   5        $��4q�5      $��28��6$   $	��Top=�60   $
��Bold"�6E   .��6q   Close6x   �� 5      -   5        $��4q�5      $��28��6$   $��Bottom=�60   .��6`   New...6h   5      5      � 5   
   $��1��6    5      �� 5      6%   �� 5      $��80 �6#   60   �� 5      $��80 �6#   60   .+�5!      �� -   5         ��Name$ CardCaptionBlock��5!      $��Center=�62   $��6q�6M   
���E��$��Lucida Console)�6q   $��14(�6�   6�   �� 5"      -   5         ��Name$ 
EditButton��5"      $��1��6)   $��4q�69   $��28��6D   $��Center=�6P   	 � ToolTip$	 Edit card details��6k   .��6�   Edit6�   �� 5#      -   5        $ DeleteButton��5#      $��2��6+   $��4q�6;   $��28��6F   $��Center=�6R   .��6�   Delete6�   5$      � 5%   
   $��2��6    5&      �� 5'      $
��Auto �6#   �� 5(      �� 5)      �� 5*      .+�5,      �� $��Center=�6   $	��6 0q�63   COPY6F   �� 5-      -   5        $ CopyNameButton��5-      $��1��6-   $	 Copy name on card��6=   $��Center=�6Y   $��Center=�6t   $
��12 3q�6�   $	��100��6�   $��26��6�   
�������
���E��.��6  
Cardholder6  �� 5.      -	   5        $ CopyNumberButton��5.      $��2��6/   $	 Copy card number��6?   $��Center=�6Z   $��Center=�6u   $
��12 3q�6�   $	��100��6�   $��26��6�   
�������
���E��.��6  Card number6  �� 5/      -
   5        $ CopyCcvButton��5/      $��3��6,   $"	 Copy card verification value��6<   $��Center=�6c   $��Center=�6~   $
��12 3q�6�   $	��100��6�   $��26��6�   
�������
���E��.��6  CVV6  50      52      53      
   M S B A M L   `   `   `  ����  Crux    Crux.DatedBalancesWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � ShowInTaskbar$  False��5   2   $	��200��6_   $	��400��6o   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $	��300 �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   
�������$��1��6D   .+�5      �� $��80��6   $��12 0 0 0q�6#    �� Padding$	 4 2q�65   $��Center=�6C   
���� ��Date6~   �� 5      $��80��6   $	 4 2q�6#   $��Center=�61   
���E�� �� TextAlignment$ Right=�6e   Pounds6�   �� 5      $��80��6   $	 4 2q�6#   $��Center=�61   
���/kU�$ Right=�6h   Euros6�   5      ]� 5   
   $��0��6   $��2��6!   $��0��65   $��False��6E   .+�5      �� -   5        $��Right=�5         ButtonClose#��[  6,   $��0 0 8 0q�6k   �� 5      -   5         ��Name$ ListLimitedTick��5       d� IsThreeState$ False��60   $��Center=�6�   $��6 0 0 0q�6�    d� 	IsChecked$
 TrueY�6�   .��6�   List last 2 years6�   5      �� 5   
   -   5        	 ��Name$	 BalancesListBox��5      $��2��6+   $��0��68   $��4 4 2 4q�6H   6[   �� 5   
   -   5        
 ��Name$
 CanvasGraph��5      $��1��6&   $��1��66   $��2��6C   $��0 0 4 4q�6T   
�������6~   5      5      
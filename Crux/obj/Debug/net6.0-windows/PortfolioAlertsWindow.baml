   M S B A M L   `   `   `  ����  Crux     Crux.PortfolioAlertsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � Title$&   Alerts for accounts and services��5   	    � WindowStartupLocation$ CenterScreen=�62    � WindowState$ 	Maximized=�6W    � 
ResizeMode$ NoResize=�6o    � ShowInTaskbar$ False��6�    � 	Resources 5       �� 
TargetType �� Triggers �� Binding	 �� Value
 �� Property �� Value   DatatemplateDueDate �� TextAlignment  DatatemplateDay  DatatemplateType  DatatemplateItem  DatatemplateAlert  DatatemplateAmount  DatatemplateAway��%�  '��       &	  �     &	 �    &	     &	 �    &	 	    &	 �    &	     �� 5
   
   " ��6    5      �� 5       6   �� *	Overdue+	 True6:   .a�5      �� 
 � ��
 �  ��63   5      �� 5       6   �� *	Overdue+		 False6:   .a�5      �� 
 � ��
 �p�63   5      5      �� 5   
   .R�5      �� $��Arial)�6   $��12(�6+   $��SemiBold"�69   $
 Left=�6O   ��6d   �� *	DueDate+5      �� 5   
   .R�5      �� $��Arial)�6   $��12(�6+   $��SemiBold"�69   $
 Left=�6O   ��6d   �� *Day+5      �� 5   
   .R�5      �� $��Arial)�6   $��12(�6+   $��SemiBold"�69   $
 Left=�6O   ��6d   �� *Type+5      �� 5!   
   .R�5"      �� $��Arial)�6   $��12(�6+   $��SemiBold"�69   $
 Left=�6O   ��6d   �� *Item+5#      �� 5%   
   .R�5&      �� $��Arial)�6   $��12(�6+   $��SemiBold"�69   $
 Left=�6O   ��6d   �� *Alert+5'      �� 5)   
   .R�5*      �� $��Arial)�6   $��12(�6+   $��SemiBold"�69   $
 Left=�6O   ��6d   �� *Amount+5+      �� 5-   
   .R�5.      �� $��Arial)�6   $��12(�6+   $��SemiBold"�69   $
 Left=�6O   ��6d   �� *Away+5/      51      .��52      � .+�53   
   �� -   5         ��Name$ ListviewAlerts��53      $��6q�6+    �� View 54      �� 55      .P�56      �� $��Due date��6%    �� CellTemplate# [  67   �� 57      $	��Day��6%   # [ 62   �� 58      $
��Type��6%   # [ 63   �� 59      $
��Item��6%   # [ 63   �� 5:      $��Alert��6%   # [ 64   �� 5;      $��Amount��6%   # [ 65   �� 5<      $
��Away��6%   # [ 63   5=       �� 
GroupStyle 5?      (  "System.Windows.Controls.GroupStyle  5@         ContainerStyle 5A      �� 5B      " ��6    .�5C      �� 
 � �� 5D   "   �� 5E   &   .R�5F   *   9�  9� 
IsExpanded$
 True��63   ��5G   .   �� 5H   2    �� Orientation$ 
Horizontal=�6=   .+�5I   6   �� $
��Bold"�6V   
��� d �$��16(�6   $��Arial)�6�   $��Center=�6�   ��6@   �� *Name+6�   �� 5J   6   $��14(�6[   
�������$��Italic$�6}   $��10,0,0,0q�6�   $��Center=�6�   ��6@   �� *	ItemCount+6�   �� 5K   6   $�� item(s)��6@   $��14(�6P   
�������$��Italic$�6r   $��Center=�6�   5L   3   .��5N   .   �� 5O   +   5P   '   5R      5S      5U      5X      5Y      5Z      
   M S B A M L   `   `   `  ����   Crux     Crux.PortfolioAlertsWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$&   Alerts for accounts and services�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 
ResizeMode$ NoResize=� � ShowInTaskbar$ False�� � 	Resources  �� 
TargetType �� Triggers �� Binding	 �� Value
 �� Property �� Value   DatatemplateDueDate �� TextAlignment  DatatemplateDay  DatatemplateType  DatatemplateItem  DatatemplateAlert  DatatemplateAmount  DatatemplateAway��%  '��       &	  �     &	 �     &	 )    &	 w    &	 �    &	     &	 d    �� " �� ��  �� *	Overdue+	 True.a��� 
 � ��
 �  ����  �� *	Overdue+		 False.a��� 
 � ��
 �p��� .R��� $��Arial)�$��12(�$��SemiBold"�$
 Left=����� *	DueDate+�� .R��� $��Arial)�$��12(�$��SemiBold"�$
 Left=����� *Day+�� .R��� $��Arial)�$��12(�$��SemiBold"�$
 Left=����� *Type+�� .R��� $��Arial)�$��12(�$��SemiBold"�$
 Left=����� *Item+�� .R��� $��Arial)�$��12(�$��SemiBold"�$
 Left=����� *Alert+�� .R��� $��Arial)�$��12(�$��SemiBold"�$
 Left=����� *Amount+�� .R��� $��Arial)�$��12(�$��SemiBold"�$
 Left=����� *Away+.��� .+��� -    ��Name$ ListviewAlerts��$��6q� �� View �� .P��� $��Due date�� �� CellTemplate# [  �� $	��Day��# [ �� $
��Type��# [ �� $
��Item��# [ �� $��Alert��# [ �� $��Amount��# [ �� $
��Away��# [  �� 
GroupStyle (  "System.Windows.Controls.GroupStyle     ContainerStyle �� " ��.��� 
 � �� �� .R�9�  9� 
IsExpanded$
 True������  �� Orientation$ 
Horizontal=�.+��� $
��Bold"�
��� d �$��16(�$��Arial)�$��Center=����� *Name+�� $��14(�
�������$��Italic$�$��10,0,0,0q�$��Center=����� *	ItemCount+�� $�� item(s)��$��14(�
�������$��Italic$�$��Center=�.���� 
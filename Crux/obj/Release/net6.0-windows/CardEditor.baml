   M S B A M L   `   `   `  ����   Crux    Crux.CardEditor   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  Credit or debit card��$	��450��$	��800��
�������
���E�� � ShowInTaskbar$ False�� � WindowStartupLocation$ CenterScreen=� � Cursor$  �� � 
ResizeMode$ NoResize=� � 	Resources  �� 
TargetType �� Property �� Value��%L   '��       �� " ��.���  � ��$ Center=���  � ��$ 6 0 6 0q�.��� 	 � ColumnDefinitions	 �� $
��Auto ��� �� $
��Auto ��� 
 � RowDefinitions
 �� �� �� �� �� �� �� .+��� $��0��	Caption�� -    ��Name$ 
CaptionBox��$��0��$��1��$��Center=�$��0 0 6 0q��� $��1��$��0��Name on card�� -   $ NameBox��$��1��$��1��$��Center=�$��0 0 6 0q��� $��2��$��0��Card number�� -   $ 	NumberBox��$��2��$��1��$��Center=�$��0 0 6 0q��� $��3��$��0��
Valid from�� $��3��$��1�� �� Orientation$ 
Horizontal=�$��Center=�.+��� $��0 0 2 0q�MM�� -   $ MonthFromBox��$��40��$
��Left=��� $��12 0 2 0q�YYYY�� -   $ YearFromBox��$��80��$
��Left=��� $��4��$��0��
Valid to�� $��4��$��1��$ 
Horizontal=�$��Center=�.+��� $��0 0 2 0q�MM�� -   $ 
MonthToBox��$��40��$
��Left=��� $��12 0 2 0q�YYYY�� -   $ 	YearToBox��$��80��$
��Left=��� $��0��$��2��CVV�� -	   $ CvvBox��$��0��$��3��$	��100��$��Center=�$
��Left=��� $��1��$��2��PIN�� -
   $ PinBox��$��1��$��3��$	��100��$��Center=�$
��Left=��� $��2��$��2��Type�� $��2��$��3��$��Center=�.+��� $ 
Horizontal=�.+�	� -    	�Name$ 
DebitRadio��$��0 0 12 8q�
���E�� d� IsThreeState$ False��.��Debit	� -   $ CreditRadio��$��0 0 0 8q�
���E��$ False��.��Credit�� $ 
Horizontal=�.+��� -    ��Name$ CreditLimitCaption��$��0 0 4 0q�Credit limit £�� -   $ CreditLimitBox��$	��100���� $��3��$��2��Verification message�� -   $ VMessageBox��$��3��$��3��$��0 0 6 0q�$��Center=��� $��4��$��2��Verification password�� -   $ VWordBox��$��4��$��3��$��0 0 6 0q�$��Center=��� $��5��$��0��Notes�� -   $ NotesBox��$��5��$��1��$��3��$��0 0 6 0q� �� TextWrapping$
 Wrap=�]� $��6��$��3��$��False��.+��� -   $��0 0 8 0q�$��80��$��32��$��Medium"�$��Right=�.��OK�� $��0 0 8 0q�$��80��$��32�� �� IsCancel$
 True��$��Right=�.��Cancel
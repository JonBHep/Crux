   M S B A M L   `   `   `  ����   Crux    Crux.CardsWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  Credit and debit cards��$	��450��$	��800��
������� � ShowInTaskbar$ False�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� �� $��38 ��� $��38 �.+�� $��0�� � ColumnDefinitions �� �� $��80 �.+��� -    ��Name$ CardsListBox��$��4q�]� $��1��$��False��.+��� -   $��4q�$��28��$	��Top=�$
��Bold"�.��Close�� -   $��4q�$��28��$��Bottom=�.��New...� $��1�� �� �� $��80 ��� $��80 �.+��� -    ��Name$ CardCaptionBlock��$��Center=�$��6q�
���E��$��Lucida Console)�$��14(��� -    ��Name$ 
EditButton��$��1��$��4q�$��28��$��Center=�	 � ToolTip$	 Edit card details��.��Edit�� -   $ DeleteButton��$��2��$��4q�$��28��$��Center=�.��Delete� $��2�� �� $
��Auto ��� �� �� .+��� $��Center=�$	��6 0q�COPY�� -   $ CopyNameButton��$��1��$	 Copy name on card��$��Center=�$��Center=�$
��12 3q�$	��100��$��26��
�������
���E��.��
Cardholder�� -	   $ CopyNumberButton��$��2��$	 Copy card number��$��Center=�$��Center=�$
��12 3q�$	��100��$��26��
�������
���E��.��Card number�� -
   $ CopyCcvButton��$��3��$"	 Copy card verification value��$��Center=�$��Center=�$
��12 3q�$	��100��$��26��
�������
���E��.��CVV
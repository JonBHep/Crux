   M S B A M L   `   `   `  ����   Crux    Crux.MotMaker   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  Password generator��$	��280��$	��600�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar$ False�� � SizeToContent$ Height=�.���  � RowDefinitions �� $��10 ��� $��24 ��� $��24 ��� $��24 ��� $��24 ��� $��24 ��� $��32 ��� �� �� $��10 �.+��� $��1��$��8 0 0 0q�	Include�� -    ��Name$ UpperCaseCheckBox��$��2��$	��8 0q�$��Center=�$��Center=� d� IsThreeState$ False�� d� 	IsChecked$
 TrueY�$��Upper case letters���� -   $ LowerCaseCheckBox��$��3��$	��8 0q�$��Center=�$��Lower case letters��$ False��$
 TrueY��� -   $ NumberCheckBox��$��4��$	��8 0q�$��Center=�$ False��$
 TrueY�$��Numbers���� -   $ SymbolCheckBox��$��5��$	��8 0q�$��Center=�$ False��$
 TrueY�$��Symbols���� $��6��	 �� Orientation$	 
Horizontal=�.+��� -   
 ��Name$
 
NumTextBox��$	��8 0q�$��50��$��0��$��Center=��� -   $��6,6,2,6q�$��˄��$��20���� -   $��2,6,0,6q�$��˅��$��20���� -	   $��7��$	��8 4q�$
��12 4q�$��80��$��Center=�$
��Left=�$��Generate��]� $��8��$��False��.+��� -
    ��Name$ PasswordTextBlock��$	��8 4q� �� Padding$	 8 5q�$��Lucida Console)�$��14(�$
��Bold"�
���  ��
�������$��Center=�$	��144���� $�Password���� -    ��Name$ 
CopyButton��$	��8 0q�$
��12 4q�$��Center=�$��80��$
��Copy���� -   $ 	UseButton��$	��8 0q�$
��12 4q�$��Center=�$
��Bold"�$��80��$	��Use���� $��Right=�$	��8 0q�$
��12 4q�$��80��$��Center=� �� IsCancel$
 True��$��Cancel��
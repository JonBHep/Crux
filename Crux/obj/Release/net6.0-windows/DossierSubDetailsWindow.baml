   M S B A M L   `   `   `  ����   Crux"    Crux.DossierSubDetailsWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � 
ResizeMode$  NoResize=� � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar$ False��.���  � ColumnDefinitions �� $
��Auto ��� $	��400 ��� $
��Auto � � RowDefinitions �� �� $
��Auto ��� .+��� -    ��Name$ TextBlockLabelOne��$��Center=�$��8 12 8 6q��� $"�Reference caption/Alert date���� -   $ TextBlockLabelTwo��$��1��$��0��$��Center=�$	��8 6q��� $�Reference text/Alert text���� -   $ TextBlockLabelThree��$��2��$��0��$��Center=�$��8 6 8 12q��� $+�%Reference highlight/Alert show amount���� -    ��Name$ TextBoxReferenceCaption��$��0��$��1��$��Center=�
����  �(  "System.Windows.Controls.DatePicker  -     Name$ DatepickerAlertDate��$��0��$��1��$��Center=�
����  ��� -   $  TextBoxAlertReferenceValue��$��1��$��1��$��Center=�$��Lucida Console)�
����  ��� -   	 ��Name$/	 )CheckboxReferenceHighlightAlertShowAmount��$��1��$��2��$��Center=�
����  ��� -	   $��0��$��2��   ButtonClose#��[  $��8 0 8 0q�$��Center=��� -
   $��2��$��1��$��80��$��8 0 8 0q�$��Center=�
 �� IsCancel$

 True��$��Cancel��
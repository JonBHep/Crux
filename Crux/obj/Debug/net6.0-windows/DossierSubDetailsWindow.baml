   M S B A M L   `   `   `  ����  Crux"    Crux.DossierSubDetailsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � 
ResizeMode$  NoResize=�5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�6>    � ShowInTaskbar$ False��6�   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $	��400 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   �� 5      .+�5   
   �� -   5         ��Name$ TextBlockLabelOne��5      $��Center=�6/   $��8 12 8 6q�6J   �� 6]   $"�Reference caption/Alert date��6a   6�   �� 5   
   -   5        $ TextBlockLabelTwo��5      $��1��6/   $��0��6<   $��Center=�6L   $	��8 6q�6g   �� 6u   $�Reference text/Alert text��6y   6�   �� 5   
   -   5        $ TextBlockLabelThree��5      $��2��61   $��0��6>   $��Center=�6N   $��8 6 8 12q�6i   �� 6|   $+�%Reference highlight/Alert show amount��6�   6�   �� 5   
   -   5         ��Name$ TextBoxReferenceCaption��5      $��0��63   $��1��6@   $��Center=�6P   
����  �6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DatepickerAlertDate��5      $��0��62   $��1��6?   $��Center=�6O   
����  �6|   �� 5   
   -   5        $  TextBoxAlertReferenceValue��5      $��1��66   $��1��6F   $��Center=�6S   $��Lucida Console)�6n   
����  �6�   �� 5   
   -   5        	 ��Name$/	 )CheckboxReferenceHighlightAlertShowAmount��5      $��1��6F   $��2��6V   $��Center=�6c   
����  �6�   �� 5   
   -	   5        $��0��5      $��2��6      ButtonClose#��[  6.   $��8 0 8 0q�6S   $��Center=�6d   6   �� 5   
   -
   5        $��2��5      $��1��6!   $��80��6.   $��8 0 8 0q�69   $��Center=�6J   
 �� IsCancel$

 True��6e   $��Cancel��6�   5      5      
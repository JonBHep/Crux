   M S B A M L   `   `   `  ����   Crux    Crux.GiftsWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$E  ?Gifts made (excluding gifts to charities and political parties)��$	��700��$
��1000�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � ShowInTaskbar$ False�� � 	Resources  �� 
TargetType �� Property �� Value   DateTemplate  AmountTemplate �� TextAlignment  DetailTemplate  AgeTemplate  YearDatesTemplate  YearCountTemplate  YearGrossTemplate  YearNetTemplate��%>  '��       &	  (     &	 o     &	 �     &	 �     &	 &    &	 P    &	 z    &	 �    �� " ��.���  � ��$ Stretch=��� .R��� $��80��$��Right=����� *Tint+���� *GDate+�� .R��� $��80��$ Right=����� *Tint+���� *	GAmount+�� .R��� ���� *Tint+���� *	GDetail+�� .R��� $��80������ *Tint+���� *GAge+�� .R��� $��80������ *YDates+�� .R��� $��80������ *YCount+�� .R��� $��80��$ Right=����� *YGross+�� .R��� $��80��$ Right=����� *YNet+.��� 	 � ColumnDefinitions	 �� �� .+��� -   
 ��Name$
 GiftsListView��$��0��$��Lucida Console)� �� View �� .P��� $
��Date�� �� CellTemplate# [  �� $	��Age��# [ �� $��Amount��# [ �� $��Detail��# [ � $��1�� � RowDefinitions �� $
��Auto ��� �� $
��Auto �.+��� .+�]� .+�(  "System.Windows.Controls.DatePicker  -     Name$ GiftDatePicker��$��8 0 8 0q�$��Center=��� $��Center=�Amount�� -    ��Name$ GiftAmountBox��$��80��$��4 0 8 0q�$��Center=��� $��Center=�	Details�� -   $��Center=�$��Right=�$��6q�$	��0 4q� � ToolTip$ Add as a new gift��$	��Add��$��60���� -   $ GiftDetailBox��$��Center=�$��4 0 0 0q�$��Gift to Tom��]� $��False��.+��� -    ��Name$ ButtonDelete��$��6q�$��Right=�$	��0 4q�$ Delete the selected gift��$��Delete��$��60��${�False���� -   $ 
ButtonEdit��$��6q�$��Right=�$	��0 4q�$ Amend the selected gift��$
��Edit��$��60��${�False��� $��1��	 �� �� ��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� .+��� $��8q�
���E��$
��Bold"�	SUMMARY�� $��8 0 4 0q�$��1��$��0��
���E��$��Lucida Console)�All time total�� -	    ��Name$ AllTimeCountTextBlock��$��1��$��1��$��Lucida Console)�X�� -
   $ AllTimeTotalTextBlock��$��1��$��2��$��Lucida Console)�X�� $��2��$��0��$��8 0 4 0q�$��Center=�$��Lucida Console)�
���E��Less than 7 years ago�� -   $ SevenYearCountTextBlock��$��2��$��1��$��Lucida Console)�X�� -   $ SevenYearTotalTextBlock��$��2��$��2��$��Lucida Console)�X�� $��3��$��2��$��0��$��	8 12 4 12q�$��Center=�$��Lucida Console)�
���E��/-Total non-exempt potentially chargeable gifts�� -   $$ PotentiallyChargeableTextBlock��$��3��$��2��$
��0 12q�$��Lucida Console)�X�� $��4��$��3��$��0��$��8q�
���E��$
��Bold"�TAX YEAR SUMMARIES�� -   $ AssumptionTextBlock��$��5��$��3��$��0��$��8q�
���E��$
��Wrap=�!Assumes annual exemption of ...�� -   $
 YearsListView��$��3��$��0��$��6��$��Lucida Console)� �� .P��� # [ $
��Year���� $��Gifts��# [ �� $��Gross��# [ �� $��Less exemption��# [ �� -   $ ButtonClose��$��2��$��Right=�$��6q�  ButtonClose#��[ 
   M S B A M L   `   `   `  ����  Crux    Crux.GiftsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � Title$E  ?Gifts made (excluding gifts to charities and political parties)��5   	   $	��700��6Q   $
��1000��6^    � WindowStartupLocation$ CenterScreen=�6k    � 
ResizeMode$ NoResize=�6�    � ShowInTaskbar$ False��6�    � 	Resources 5       �� 
TargetType �� Property �� Value   DateTemplate  AmountTemplate �� TextAlignment  DetailTemplate  AgeTemplate  YearDatesTemplate  YearCountTemplate  YearGrossTemplate  YearNetTemplate��%�  '��       &	  M     &	 �     &	 ;    &	 �    &	 �    &	 G    &	 �    &	 �    �� 5	   
   " ��6   .�5
      ��  � ��$ Stretch=�6;   5      �� 5   
   .R�5      �� $��80��6   $��Right=�6&   ��6B   �� *Tint+��6^   �� *GDate+5      �� 5   
   .R�5      �� $��80��6   $ Right=�6&   ��6<   �� *Tint+��6X   �� *	GAmount+5      �� 5   
   .R�5      �� ��6   �� *Tint+��64   �� *	GDetail+5      �� 5   
   .R�5      �� $��80��6   ��6&   �� *Tint+��6B   �� *GAge+5      �� 5   
   .R�5      �� $��80��6   ��6&   �� *YDates+5      �� 5   
   .R�5      �� $��80��6   ��6&   �� *YCount+5      �� 5   
   .R�5       �� $��80��6   $ Right=�6'   ��6=   �� *YGross+5!      �� 5"   
   .R�5#      �� $��80��6   $ Right=�6&   ��6<   �� *YNet+5$      5%      .��5&      � 	 � ColumnDefinitions	 5'   
   �� 5(      �� 5)      .+�5+   
   �� -   5        
 ��Name$
 GiftsListView��5+      $��0��6*   $��Lucida Console)�6l    �� View 5,      �� 5-      .P�5.      �� $
��Date��6%    �� CellTemplate# [  63   �� 5/      $	��Age��6%   # [ 62   �� 50      $��Amount��6%   # [ 65   �� 51      $��Detail��6%   # [ 65   52      54      � 55   
   $��1��6    � RowDefinitions 56      �� 57      $
��Auto �6    60   �� 58      6"   �� 59      $
��Auto �6    60   .+�5;      �� .+�5<      ]� .+�5=      (  "System.Windows.Controls.DatePicker  -   5          Name$ GiftDatePicker��5=   !   $��8 0 8 0q�69   $��Center=�6J   6g   �� 5>      $��Center=�6    Amount6C   �� 5?      -   5         ��Name$ GiftAmountBox��5?      $��80��65   $��4 0 8 0q�6@   $��Center=�6Q   6n   �� 5@      $��Center=�6    	Details6D   �� 5B      -   5        $��Center=�5B      $��Right=�68   $��6q�6O   $	��0 4q�6Z    � ToolTip$ Add as a new gift��6h   $	��Add��6�   $��60��6�   6�   �� 5C      -   5        $ GiftDetailBox��5C      $��Center=�65   $��4 0 0 0q�6P   $��Gift to Tom��6a   6v   5D      ]� 5E      $��False��6   .+�5F      �� -   5         ��Name$ ButtonDelete��5F      $��6q�63   $��Right=�6>   $	��0 4q�6U   $ Delete the selected gift��6c   $��Delete��6�   $��60��6�   ${�False��6�   �� 5G      -   5        $ 
ButtonEdit��5G      $��6q�61   $��Right=�6<   $	��0 4q�6S   $ Amend the selected gift��6a   $
��Edit��6�   $��60��6�   ${�False��6�   5H      5I      � 5J      $��1��6   	 5K      �� 5L      6)   �� 5M      6)   �� 5N      6)    5P      �� 5Q      $
��Auto �6$   64   �� 5R      $
��Auto �6$   64   �� 5S      $
��Auto �6$   64   �� 5T      $
��Auto �6$   64   �� 5U      $
��Auto �6$   64   �� 5V      $
��Auto �6$   64   �� 5W      6&   .+�5Z      �� $��8q�6   
���E��$
��Bold"�6@   	SUMMARY6[   �� 5\      $��8 0 4 0q�6   $��1��6-   $��0��6:   
���E��$��Lucida Console)�6c   All time total6�   �� 5]      -	   5         ��Name$ AllTimeCountTextBlock��5]      $��1��6;   $��1��6H   $��Lucida Console)�6X   X6w   �� 5^      -
   5        $ AllTimeTotalTextBlock��5^      $��1��6;   $��2��6H   $��Lucida Console)�6X   X6w   �� 5`      $��2��6   $��0��6)   $��8 0 4 0q�69   $��Center=�6J   $��Lucida Console)�6e   
���E��Less than 7 years ago6�   �� 5a      -   5        $ SevenYearCountTextBlock��5a      $��2��6=   $��1��6J   $��Lucida Console)�6Z   X6z   �� 5b      -   5        $ SevenYearTotalTextBlock��5b      $��2��6=   $��2��6J   $��Lucida Console)�6Z   X6z   �� 5d      $��3��6   $��2��6)   $��0��6=   $��	8 12 4 12q�6M   $��Center=�6`   $��Lucida Console)�6{   
���E��/-Total non-exempt potentially chargeable gifts6�   �� 5e      -   5        $$ PotentiallyChargeableTextBlock��5e      $��3��6D   $��2��6Q   $
��0 12q�6a   $��Lucida Console)�6o   X6�   �� 5g      $��4��6   $��3��6)   $��0��6=   $��8q�6M   
���E��$
��Bold"�6q   TAX YEAR SUMMARIES6�   �� 5h      -   5        $ AssumptionTextBlock��5h      $��5��69   $��3��6F   $��0��6Z   $��8q�6j   
���E��$
��Wrap=�6�   !Assumes annual exemption of ...6�   �� 5j      -   5        $
 YearsListView��5j      $��3��62   $��0��6F   $��6��6V   $��Lucida Console)�6�    5k      �� 5l      .P�5m      �� # [ 6-   $
��Year��6_   �� 5n      $��Gifts��6-   # [ 6<   �� 5o      $��Gross��6-   # [ 6<   �� 5p      $��Less exemption��6-   # [ 6E   5q      5s      5t      �� 5u      -   5        $ ButtonClose��5u      $��2��6*   $��Right=�67   $��6q�6S     ButtonClose#��[ 6^   6�   5v      5x      5y      
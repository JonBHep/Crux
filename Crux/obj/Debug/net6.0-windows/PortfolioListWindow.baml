   M S B A M L   `   `   `  ����  Crux    Crux.PortfolioListWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   -   5          � Title$  Portfolio list��5   	   $	��700��6    $
��1000��6-    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6�    � ShowInTaskbar$ False��6�    � 	Resources 5       �� 
TargetType �� Property �� Value   DatatemplateDossierTitle ��Name	 �� TextAlignment
 �� Triggers �� Value �� Binding �� 
TargetName  DatatemplateProvider  DatatemplateAlerts  InDocumentTemplate d� 	IsChecked  ObsolescenceTemplate  DatatemplateOnline  DatatemplateAmount  DatatemplateAmountDate��%	  '��       &	  M     &	 �    &	 �    &	 �    &	 [    &	 �    &	 /    &	 �    �� 5
   
   " ��6   .�5      ��  � ��$ Stretch=�6;   5      �� 5   
   .R�5      �� $ textblockTitle��6   $��Arial)�60   $��12(�6C   $��SemiBold"�6Q   $
	 Left=�6g   
���E����6�   �� *DossierName+
 5      �� 5      $
 True��6;    6   �� *
Obsolete+.a�5      �� $ textblockTitle��6    � ��
 �  ��6O   5      5      �� 5   
   .R�5      �� $ textblockProvider��6   $��Arial)�63   $��12(�6F   $
	 Left=�6T   
��� d ���6�   �� *
Provider+
 5      �� 5      $
 True��6;    6   �� *
Obsolete+.a�5      �� $ textblockProvider��6    � ��
 �  ��6R   5      5      �� 5!   
   .R�5"      �� $ textblockAlerts��6   $��12(�61   $��SemiBold"�6?   $	 Center=�6U   
���E����6�   �� *Alerts+
 5#      �� 5$      $
 True��6?    6   �� *AlertOverdue+.a�5%      �� $ textblockAlerts��6    � ��
 �  ��6P   5&      5(      �� 5*   
   .R�5+      �� $��Center=�6   ${�False��6U    64   �� *
InDocument+6U   5,      �� 5.   
   .R�5/      �� $��Center=�6   ${�False��6S    64   �� *
Obsolete+6S   50      �� 52   
   .R�53      �� $��Center=�6   ${�False��6Q    64   �� *Online+6Q   54      �� 56   
   .R�57      �� $ textblockAmount��6   $��Right=�61   
���   �$��Medium"�6x   ��6M   �� *Amount+6x   
 58      �� 59      $
 True��6A    6   �� *BankBalanceOld+.a�5:      �� $ textblockAmount��6    � ��
 �  ��6P   5;      �� 5<      $
 True��6C    6   �� *BankBalanceToday+.a�5=      �� $ textblockAmount��6    � ��
 � � �6P   5>      5@      �� 5B   
   .R�5C      �� $ textblockAmountDate��6   
���   �$��Center=�6d   $��Medium"�6�   ��65   �� *
AmountDate+6�   
 5D      �� 5E      $
 True��6A    6   �� *BankBalanceOld+.a�5F      �� $ textblockAmountDate��6    � ��
 �  ��6T   5G      �� 5H      $
 True��6C    6   �� *BankBalanceToday+.a�5I      �� $ textblockAmountDate��6    � ��
 � � �6T   5J      5L      5N      .��5O      �  � RowDefinitions 5P   
   �� 5Q      �� 5R      $��34 �6   �� 5S      $��34 �6   .+�5U   
   �� -   5         ��Name$ ListviewDossiers��5U       �� View 5V      �� 5W      .P�5X      �� -   5        ColumnDossier�� �� CellTemplate# [  5X   <   $��Account��6u   �� 5Y      -   5        ColumnProvider��# [ 5Y   =   �� 5Z      -   5        ColumnAmount��$��Amount��5Z   ;   # [ 6K   �� 5[      -   5        ColumnAmountDate��$��
AmountDate��5[   ?   # [ 6S   �� 5\      -   5        ColumnAlerts��$��Alerts��5\   ;   # [ 6K   �� 5]      -   5        ColumnOnline��$��Online��5]   ;   # [ 6K   �� 5^      -	   5        ColumnDocument��$��Document��5^   =   # [ 6O   �� 5_      -
   5        ColumnObsolete��$��Obsolete��5_   =   # [ 6O   5`       �� 
GroupStyle 5b      (  "System.Windows.Controls.GroupStyle  5c         ContainerStyle 5d      �� 5e      " ��6    .�5f      ��  � �� 5g   "   �� 5h   &   .R�5i   *   9�  9� 
IsExpanded$
 True��63   ��5j   .   �� 5k   2    �� Orientation$ 
Horizontal=�6=   .+�5l   6   �� $
��Bold"�6V   
��� d �$��14(�6   $��Arial)�6�   $��Center=�6�   ��6@   �� *Name+6�   �� 5m   6   $��14(�6[   
�������$��Italic$�6}   $��10,0,0,0q�6�   $��Center=�6�   ��6@   �� *	ItemCount+6�   �� 5n   6   $�� item(s)��6@   $��14(�6P   
�������$��Italic$�6r   $��Center=�6�   5o   3   .��5q   .   �� 5r   +   5s   '   5u      5v      5x      5z      �� 5{   
   $ 
Horizontal=�6   $��1��6.   .+�5|      �� -   5        $  TextblockDossierTotalCount��5|      $��Center=�6<   $��8q�6W   �� 6c   $�Count��6g   6w   �� 5}      -   5        $ TextblockDossierLiveCount��5}      $��Center=�6;   $��8q�6V   
��� d ��� 6y   $�Count��6}   6�   �� 5~      -   5        $# TextblockDossierObsoleteCount��5~      $��Center=�6?   $	��8 0q�6Z   
���  ���� 6|   $�Obsolete��6�   6�   �� 5      -   5        $ TextblockTotalPounds��5      $��Center=�66   $	��8 0q�6Q   �� 6_   $�Total��6c   6s   �� 5�      -   5        $ TextblockTotalEuros��5�      $��Center=�65   $	��8 0q�6P   �� 6^   $�Total��6b   6r   �� 5�      $��Center=�6   $	��8 0q�63   
���  ��$
��Bold"�6Q   �� 6d   $2�,Double-click an item to view or edit details��6h   6�   �� 5�      $��Center=�6   $	��8 0q�63   �� 6A   $-�'Right-click the list to access the menu��6E   6w   5�      ]� 5�   
   $��False��6   $��2��6*   .+�5�      �� -   5         ��Name$ ButtonClose��5�      $��4q�6*     ButtonClose#��[ 65   $��Center=�6Z    � ToolTip$1 +Return to Accounts and Services main screen��6u   .��6�   Return6�   �� 5�      -   5        $ BalancesButton��5�      $- 'View a list and chart of dated balances��6-   $��Right=�6_   $��Center=�6v   $��72��6�   $��27��6�   $��0 0 8 0q�6�   .��6�   
Timeline6�   �� 5�      -   5        $ UpdateBalancesButton��5�      $' !Enter up to date account balances��63   $��Right=�6_   $��Center=�6v   $	��128��6�   $��27��6�   $��0 0 8 0q�6�   .��6�   Update balances6�   5�      5�      5�      
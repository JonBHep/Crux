   M S B A M L   `   `   `  ����   Crux    Crux.PortfolioListWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  Portfolio list��$	��700��$
��1000�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � ShowInTaskbar$ False�� � 	Resources  �� 
TargetType �� Property �� Value   DatatemplateDossierTitle ��Name	 �� TextAlignment
 �� Triggers �� Value �� Binding �� 
TargetName  DatatemplateProvider  DatatemplateAlerts  InDocumentTemplate d� 	IsChecked  ObsolescenceTemplate  DatatemplateOnline  DatatemplateAmount  DatatemplateAmountDate��%�  '��       &	  (     &	 �     &	 �    &	 �    &	 �    &	 �    &	 4    &	 O    �� " ��.���  � ��$ Stretch=��� .R��� $ textblockTitle��$��Arial)�$��12(�$��SemiBold"�$
	 Left=�
���E������ *DossierName+
 �� $
 True�� �� *
Obsolete+.a��� $ textblockTitle�� � ��
 �  ���� .R��� $ textblockProvider��$��Arial)�$��12(�$
	 Left=�
��� d ����� *
Provider+
 �� $
 True�� �� *
Obsolete+.a��� $ textblockProvider�� � ��
 �  ���� .R��� $ textblockAlerts��$��12(�$��SemiBold"�$	 Center=�
���E������ *Alerts+
 �� $
 True�� �� *AlertOverdue+.a��� $ textblockAlerts�� � ��
 �  ���� .R��� $��Center=�${�False�� �� *
InDocument+�� .R��� $��Center=�${�False�� �� *
Obsolete+�� .R��� $��Center=�${�False�� �� *Online+�� .R��� $ textblockAmount��$��Right=�
���   �$��Medium"����� *Amount+
 �� $
 True�� �� *BankBalanceOld+.a��� $ textblockAmount�� � ��
 �  ���� $
 True�� �� *BankBalanceToday+.a��� $ textblockAmount�� � ��
 � � ��� .R��� $ textblockAmountDate��
���   �$��Center=�$��Medium"����� *
AmountDate+
 �� $
 True�� �� *BankBalanceOld+.a��� $ textblockAmountDate�� � ��
 �  ���� $
 True�� �� *BankBalanceToday+.a��� $ textblockAmountDate�� � ��
 � � �.���  � RowDefinitions �� �� $��34 ��� $��34 �.+��� -    ��Name$ ListviewDossiers�� �� View �� .P��� -   ColumnDossier�� �� CellTemplate# [  $��Account���� -   ColumnProvider��# [ �� -   ColumnAmount��$��Amount��# [ �� -   ColumnAmountDate��$��
AmountDate��# [ �� -   ColumnAlerts��$��Alerts��# [ �� -   ColumnOnline��$��Online��# [ �� -	   ColumnDocument��$��Document��# [ �� -
   ColumnObsolete��$��Obsolete��# [  �� 
GroupStyle (  "System.Windows.Controls.GroupStyle     ContainerStyle �� " ��.���  � �� �� .R�9�  9� 
IsExpanded$
 True������  �� Orientation$ 
Horizontal=�.+��� $
��Bold"�
��� d �$��14(�$��Arial)�$��Center=����� *Name+�� $��14(�
�������$��Italic$�$��10,0,0,0q�$��Center=����� *	ItemCount+�� $�� item(s)��$��14(�
�������$��Italic$�$��Center=�.���� �� $ 
Horizontal=�$��1��.+��� -   $  TextblockDossierTotalCount��$��Center=�$��8q��� $�Count���� -   $ TextblockDossierLiveCount��$��Center=�$��8q�
��� d ��� $�Count���� -   $# TextblockDossierObsoleteCount��$��Center=�$	��8 0q�
���  ���� $�Obsolete���� -   $ TextblockTotalPounds��$��Center=�$	��8 0q��� $�Total���� -   $ TextblockTotalEuros��$��Center=�$	��8 0q��� $�Total���� $��Center=�$	��8 0q�
���  ��$
��Bold"��� $2�,Double-click an item to view or edit details���� $��Center=�$	��8 0q��� $-�'Right-click the list to access the menu��]� $��False��$��2��.+��� -    ��Name$ ButtonClose��$��4q�  ButtonClose#��[ $��Center=� � ToolTip$1 +Return to Accounts and Services main screen��.��Return�� -   $ BalancesButton��$- 'View a list and chart of dated balances��$��Right=�$��Center=�$��72��$��27��$��0 0 8 0q�.��
Timeline�� -   $ UpdateBalancesButton��$' !Enter up to date account balances��$��Right=�$��Center=�$	��128��$��27��$��0 0 8 0q�.��Update balances
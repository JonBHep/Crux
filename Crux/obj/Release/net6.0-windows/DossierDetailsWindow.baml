   M S B A M L   `   `   `  ����   Crux    Crux.DossierDetailsWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  Portfolio details��$	��800��$
��1200�� � WindowStartupLocation$ CenterScreen=� � WindowState$ Normal=� � ShowInTaskbar$ False�� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � 	Resources  �� 
TargetType �� Property	 �� Value   DatatemplateAlertDate
 ��Name �� TextAlignment �� Triggers �� Value �� Binding �� Path �� 
TargetName  DatatemplateAlertCaption  DatatemplateAlertAmount��%�  '��       '�� q     '�� �     &	  �     &	 �    &	     �� " ��.���  �@��CornerRadius$	 6����  � ��
	 �"�"���  � ��$	 2q���  � ��$	 6q��� " ��.���  � ��$	 Stretch=��� " ��.���  � ��$	 Light"���  � ��$		 4 0q���  � ��$	 80���� .R��� $
 	textblock��$��Arial)�$��12(�$��SemiBold"�$
 Left=�
���"�"����� *DateForDisplay+ �� $
 True�� �� $ Overdue�.a��� $ 	textblock�� � ��
	 �  ���� .R��� $��Arial)�$��12(�$
 Left=�
���"�"����� *	Caption+�� .R��� $��Arial)�$��12(�$
 Left=�
���"�"����� *Amount+.���� 
�������$��0q�._�� $��8q� � ColumnDefinitions �� �� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� .+��� 
�������._�]� .+�]� $	��Top=�.+��� -   $
 TextblockTitle��$
��Left=�$��Center=�$��8q� �� Padding$	 8 3q�$��SemiBold"��� $�	... title���� -    ��Name$ ComboboxGroupName�� �� 
IsEditable$
 True��$
��Left=�$��Center=�$	��200�� � ToolTip$ Group��
���"�"�$��SemiBold"��� $
��Left=�$��Center=�$��SemiBold"�$��0q�$ 4q��� $�:���� -    ��Name$ TextboxTitleSpecifics��$��Center=�$��3q�$��0 0 8 0q�$
 Name��
���"�"�$��SemiBold"�]� .+��� $
��Left=�$��Center=�$��8q�$	 8 3q�$��SemiBold"��� $�Provider organisation���� -   $! TextboxProviderOrganisation��$��Center=�$��3q�$��0 0 8 0q�
���"�"�$��SemiBold"��� $��0��$��1��
�������$	��Top=�._�]� $��False��.+��� -    ��Name$ ButtonOk��$��Right=�  ButtonClose#��[ $��Center=�$��0 8 8 8q��� -   $ ButtonCancel��$��Right=�$��80��$��Center=�$��0 8 8 8q�$	��8 4q�$��Cancel���� -   $ 
ButtonCopy��$( "Copy acccount details to clipboard��$��Right=�$��80��$��Center=�$��8 8 8 8q�$	��8 4q�$
��Copy���� $��2��$��2��$��0��
�������._�]� $��False��.+��� -	   $
 TextblockAmount��$��Center=�$	��8 0q�$	 8 3q�$��SemiBold"��� $�Amount���� -
   $
 TextblockSymbol��$��Center=�$��8 0 0 0q�$	 8 3q�$��SemiBold"�
���"�"��� $�£���� -   $ TextboxAmount��$��Center=�$��3q�$��0 0 8 0q�$	��120��
���"�"�$��SemiBold"��� -    ��Name$ CheckboxEuros��$��Center=�$��Center=�$��8 0 8 0q�$��SemiBold"�$��Euros���� -   $
 TextblockDateCaption��$��Center=�$	��8 0q�$	 8 3q�$��SemiBold"��� $�[date caption]���� -   $ CheckboxEnableDate��$	��8 0q�$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ DatepickerSumDate��$	��200��$��Center=�
���"�"�${�False���� -   $ ButtonToday��$��3q�$��60��$��Center=�$��8q�$��Today��� $��3��$��2��$��0�� �� $
��Auto ��� .+��� 
�������._�]� .+��� -   $ CheckboxOption��$	��Top=�$��Center=�$��8q�$��[option]���� -   $ CheckboxDocument��$	��Top=�$	��Top=�$��Center=�$��8q�$"��Include in exported document���� -   $ CheckboxObsolete��$	��Top=�$	��Top=�$��Center=�$��8q�$��Obsolete [...]���� $��1��
�������._�]� $��6q�.+��� $	��Top=� �� Orientation$ 
Horizontal=�$��Stretch=�.+��� $��Center=�$	��8 0q�$��SemiBold"��� $�Alerts���� -   $ ButtonAlertAdd��$��Center=�$��60��$��3q�$	��3 8q�.��Add�� -   $ ButtonAlertEdit��$��Center=�$��60��$��3q�$��3q�${�False��.��Edit�� -   $ ButtonAlertDelete��$��Center=�$��60��$��3q�$��3q�${�False��.��Delete�� -    ��Name$ ListviewAlerts��$��4q�$	��100�� �� View ��  �� AllowsColumnReorder$ False��.P���  �� CellTemplate# [  .��Date�� # [ .��Alert�� # [ .��Amount�� $��4��$��2��$��0��
�������._�]� $��6q�.+��� $	��Top=�$ 
Horizontal=�$��Stretch=�.+��� $��Center=�$	��8 0q�$��SemiBold"��� $�
References���� -   $ ButtonReferenceAdd��$��Center=�$��60��$��3q�$	��3 8q�.��Add�� -   $ ButtonReferenceEdit��$��Center=�$��60��$��3q�$��3q�${�False��.��Edit�� -   $ ButtonReferenceDelete��$��Center=�$��60��$��3q�$��3q�${�False��.��Delete�� -   $ ButtonReferencePromote��$��Center=�$��60��$��3q�$��3q�${�False��.��	Promote�� -     ��Name$  ListboxReferences��$��4q�
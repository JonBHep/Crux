   M S B A M L   `   `   `  ����   Crux    Crux.MotEditor   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  Password editor��$��Consolas)�$	��800��$
��1200�� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar$ False��.���  � RowDefinitions �� $��32 ��� $��* ��� $��32 ��� $��3* ��� $��32 ��� $��32 ��� $��32 ��� $��32 ��� $��32 ��� $��38 �.+�]� .+��� $
��Left=�$	��4 0q�$��Center=�Titles�� -    ��Name$ AddTitleButton��$��Right=�$	��6 0q�$��Center=�$��60��$��23��.��Add�� -    ��Name$ TextboxTitle��$��4q�$��4q�$��Center=�$��Lucida Console)�]� $��1��.+��� -   $ RemoveTitleButton��$��Right=�$	��6 0q�$��Center=�$��60��$��23��.��Remove�� -    ��Name$ TitlesListBox��$��4q��� $��2��$	��4 0q�$��Center=�
Data items�� -   $ PasswordListBox��$��3��$��4q��� $��4�� �� Orientation$ 
Horizontal=�.+��� -   $ ButtonMoveUp��$��4q�$��4q�$��Center=�$��80��$��Move up���� -   $ ButtonDelete��$��4q�$��4q�$��Center=�$��80��$��Delete���� -	   $ 
ButtonEdit��$��4q�$��4q�$��Center=�$��80��$
��Edit��]� $��5��.+��� $
��Left=�$��4q� �� Padding$ 4q�$��Center=�$��60���� $�Caption���� -
   $��Right=�$��4q�$��4q�$��Center=�$"��Insert Hotmail email address��$|�False���� -   	 ��Name$	 ComboboxKey��$��4q�$��4q�$��Center=�
 �� 
IsEditable$

 True��$��Lucida Console)�]� $��6��.+��� $
��Left=�$��4q�$ 4q�$��Center=�$��60���� $�Value���� -   $��Right=�$��4q�$��4q�$��Center=�$��Generate a password��$|�False���� -   $ TextboxValue��$��4q�$��4q�$��Center=�$��Center=�$��Lucida Console)�]� $��7��$��False��.+��� -    ��Name$ LinkCheckBox�� d� IsThreeState$ False��$
��Left=�$��72 6 0 6q�$��Center=�$��Center=�$��Weblink���� $��8 0 0 0q�
����=H�$��Center=��� $[�UTick 'Weblink' for a web address which can be launched when viewing the Password file���� -   $ 	ButtonAdd��$��8��$��72 0 0 0q�$��4q�$
��Left=�$��Center=�$��80�� �� 	IsDefault$
 True��$	��Add��]� $��9��$��False��.+��� -   $ 
SaveButton��$��Right=�   ButtonClose#��[  $��4 4 4 8q�$��Center=��� -   $��Right=�$��4 4 4 8q�$��4q�$��Center=�$��80��$��Cancel���� -   $ CheckboxFavourite��$ False��$��Right=�$��0 0 8 0q�$��Center=�$��Center=�$��	Favourite��
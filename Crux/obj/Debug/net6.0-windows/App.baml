   M S B A M L   `   `   `  ����?  ;Crux, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CruxCrux  5          Crux.App   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      $localclr-namespace:Crux     5        �� 	Resources  5         ButtonClose �� 
TargetType �� Property �� Value �� EndPoint �� 
StartPoint � Color � Offset �� 
TargetType	 � 	Resources  Key
 g� 	BeginTime �� 
TargetName �� TargetProperty O� KeyTime O� Value ��Name �� RadiusX �� RadiusY J� Opacity J� RenderTransformOrigin ��Name J� SnapsToDevicePixels �� Triggers P� Property P� Value N� ExitActions  StoryboardMouseLeave N� EnterActions  StoryboardMouseEnter��%�  &	        7  7  �� 5      " ��6%   .�5      ��  � ��$ 88��6&   63   �� 5	       � ��$ 24��6'   64   �� 5
       � ��$
 Bold"�6+   6:   �� 5       � �� 5      �� 5      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5      � $	 Red��6'   $ 0Q�63   � 5      $ Moccasin��6'   $ 1Q�68   5      5      �� 5       � �� 5      �� 5      " ��6&   	 5      ���� 5      StoryboardMouseEnter .��5   "   U� 
 00:00:006@   $ 
rectangleR��6U   $ (UIElement.Opacity)�6x   .\�5   &   �� $ 00:00:00��6;   $	 0.6Q�6N   �� 5   &   $ 00:00:00.1000000��6;   $ 1Q�6V   5   #   U� 5   "   
 00:00:006@   $ contentPresenter��6U   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6~   .\�5   &   �� $ 00:00:00��6;   $ 1Q�6N   �� 5   &   $ 00:00:00.1000000��6;   $	 1.1Q�6V   5    #   U� 5!   "   
 00:00:006@   $ 
rectangleR��6U   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6x   .\�5"   &   �� $ 00:00:00��6;   $ 1Q�6N   �� 5#   &   $ 00:00:00.1000000��6;   $
 1.05Q�6V   5$   #   U� 5%   "   
 00:00:006@   $ 
rectangleR��6U   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6x   .\�5&   &   �� $ 00:00:00��6;   $ 1Q�6N   �� 5'   &   $ 00:00:00.1000000��6;   $
 1.05Q�6V   5(   #   5)      �� 5+      StoryboardMouseLeave .��5,   "   U� 
 00:00:006@   $ 
rectangleR��6U   $ (UIElement.Opacity)�6x   .\�5-   &   �� $ 00:00:00��6;   $ 1Q�6N   �� 5.   &   $ 00:00:01.1000000��6;   $	 0.6Q�6V   5/   #   U� 50   "   
 00:00:006@   $ contentPresenter��6U   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6~   .\�51   &   �� $ 00:00:00��6;   $	 1.1Q�6N   �� 52   &   $ 00:00:01.1000000��6;   $ 1Q�6V   53   #   U� 54   "   
 00:00:006@   $ 
rectangleR��6U   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6x   .\�55   &   �� $ 00:00:00��6;   $
 1.05Q�6N   �� 56   &   $ 00:00:01.1000000��6;   $ 1Q�6V   57   #   U� 58   "   
 00:00:006@   $ 
rectangleR��6U   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6x   .\�59   &   �� $ 00:00:00��6;   $
 1.05Q�6N   �� 5:   &   $ 00:00:01.1000000��6;   $ 1Q�6V   5;   #   5<      5=      .R�5>      � .+�5?      �� $ 
rectangleR��6(   $��60��6<   $��24��6J   
�������
���**��$ 2��6u   $ 2��6�   $	 0.6Q�6�   $ 0.5,0.5&�6�   z�5@   "   V� 5A   &   .~�5B   *   �� �� 5C   *   �� 5D   *   T� 5E   *   5F   '   5H      �� 5I      $ contentPresenter��6/   $��Center=�6K   #��z��6h   # z 6�   $
��True��6�   $��CLOSE��6�   $ 0.5,0.5&�6  z�5J   "   V� 5K   &   .~�5L   *   �� �� 5M   *   �� 5N   *   T� 5O   *   5P   '   5R      5S       5T      P� 5U       �@J�	IsFocused$
 True��6;   P� 5V       �@��IsDefaulted$
 True��6=   P� 5W       �@J�IsMouseOver$
 True��6=    5X   "   �� 5Y   &   8��  66    5[   "   �� 5\   &   8�� 66   5^      P� 5_       � ��$
 True��6;   P� 5`       � {�$ False��6;   . �5a   "   ��  �@J�Opacity$
 0.40Q�6<   6K   5b      5d      5g      5h      5i      5j      
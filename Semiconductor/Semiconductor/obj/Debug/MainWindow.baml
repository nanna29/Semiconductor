   M S B A M L   `   `   `  ����H  DSemiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SemiconductorSemiconductor  5   	       Semiconductor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Semiconductor     5   	     � Title$	  WMS��5   	   $	��750��6   $
��1300��6"      
background#��[  5	   	    � 	Resources 5      0   *Semiconductor.BooleanToVisibilityConverter  VisibilityConverter"   Semiconductor.ImageConverter  ImageConverter��%:   &	       &	        5   
   6-     5   
   6   5      .��5      � $��0,0,0,0q�6   .+�5   
   �� -   5         ��Name$	 txt��5      $��Right=�69   $��38��6U   $��
0,24,812,0q�6a    �� TextWrapping$
 Wrap=�6u   $	��Top=�6�   $	��308��6�   ��6   �� *
PathText+6�   �� 5   
   $��파일 선택��6   $��Right=�6�   $��38��6�   $��0,24,1143,0q�6�   $	��Top=�6�   $	��114��6�   ��6!   �� *DisplayPathCommand+��6H   ��  �� ElementName$	 txt�� �� Path$
 Text���5      �� 5       �� 
TargetType" ��6"   .R�5      � .+�5      A� ���	#AA000000$��10,10,0,-10q�63   A� 5      #��z��6"   $��0,0,10,0q�6F   (� 5      $��
5,10,15,10q�6"   .��5      �� 5      5      5      5      �� 5#   
   -   5         ��Name$ VisibleCheck��5#      $��이미지만 보기��6)   $��Right=�6;   $��38��6W   $��
0,24,676,0q�6c   $	��Top=�6w   $	��110��6�   �� 5$   
   -   5        $��image��5$       J� Opacity$ 1Q�66   	 J� RenderTransformOrigin$	 0.5,0.5&�6B   }�. $��Right=�6v   $	��646��5%      ��5$      �� *WBmp2+��5&      �� $ VisibleCheck��$ 	IsChecked�
 �� 	Converter#
 [ � 5)   
   $��0,0,0,0q�6   y�6   �� $ VisibleCheck��$ 	IsChecked�#
 [ .+�5,      �� $��Right=�6   $��19��64   $��0,230,558,0q�6@   $
��Wrap=�6U   $��Die Info��6i   $	��Top=�6y   $��62��6�   $
��Bold"�6�   &   System.Windows.Controls.DataGrid  5-      -   5          Name$ DieGrid��5-      $��
0,254,34,0q�6(   $
��Auto=�50      $
��Auto=�51         AutoGenerateColumns .     CanUserAddRows .  $��Right=�53   *   $	��586��6F   $	��176��6R   $	��Top=�6_   ��5.      �� *
DieLists+   Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Header$ XSampleTestPlan��6M   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *XSampleTestPlan+6M     56      $ YSampleTestPlan��6M    6)   �� *YSampleTestPlan+6M     57      $ XSampleCenterLocation��6S    6)   �� *XSampleCenterLocation+6S     58      $ YSampleCenterLocation��6S    6)   �� *YSampleCenterLocation+6S     59      $ 	XDiePitch��6G    6)   �� *	XDiePitch+6G     5:      $ 	YDiePitch��6G    6)   �� *	YDiePitch+6G   5<      �� 5@      $��Right=�6   $��20��64   $��0,472,558,0q�6@   $
��Wrap=�6U   $��Defect Info��6i   $	��Top=�6|   $��62��6�   $
��Bold"�6�     5A      $	��176��6   $��
0,497,34,0q�6$   $	��Top=�68   $
��Auto=�5D      $
��Auto=�5E       .   .  $��Right=�5G   *   $	��586��6F   ��5B      �� *DefectLists+ �� SelectedItem 5C      �� *SelectedDefect+ 5H        5I      $ DEFECTID��6F    6)   �� *
DEFECTID+6F     5J      $
 XREL��6B    6)   �� *XREL+6B     5K      $
 YREL��6B    6)   �� *YREL+6B     5L      $ XINDEX��6D    6)   �� *XINDEX+6D     5M      $ YINDEX��6D    6)   �� *YINDEX+6D   5O      �� 5R      
���   �$	��0.5q�6)   $��Right=�6?   $	��138��6[   $��
0,24,384,0q�6h   $	��Top=�6|   $	��236��6�   
������� � DataContext 6�   �� *SelectedDefect+._�5S      �� $��10q�6   .+�5T      �� $��Defect details��6    $
��Bold"�5U      $��16(�5V      $��Center=�5W      $��10q�5X      �� 5Y      $��실제 Defect X좌표��6    �� 5Z      ��6   �� *BL_X+�� 5[      $��실제 Defect Y좌표��6    �� 5\      ��6   �� *BL_Y+5]      5^      � 5a      -   5         �Name$
 grid��5a      $	��138��6!   $��	0,24,34,0q�6.   $	��Top=�6A   $��Right=�6Y   $	��293��6u    � ColumnDefinitions 5b      �� 5c      �� 5d       � RowDefinitions 5f      �� 5g      �� 5h      �� 5i      .+�5k      �� $��0��6   $��0��6'   .��67   	WaferID :6B   �� 5l      $��0��6   $��1��6'   ��67   �� *	WaferID+�� 5n      $��1��6   $��0��6'   .��67   FileTimestamp :6H   �� 5o      $ WrapWithOverflow=�69   $��	146,0,0,0q�6Y   $��2��6l   $��1��6�   ��6   �� *FileTimestamp+6�   �� 5q      $��2��6   $��0��6'   .��67   ResultTimestamp :6J   �� 5r      $��2��6   $��1��6'   $ WrapWithOverflow=�6X   ��67   �� *ResultTimestamp+6X   5s      5u      5x      5y      
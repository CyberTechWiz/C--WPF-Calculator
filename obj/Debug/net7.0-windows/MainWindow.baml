   M S B A M L   `   `   `  ����C  ?Calc 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  Калькулятор��5   	   $	��560��6   $	��340��6*   $	��340��5	   	   $	��500��6    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   $8��2pack://application:,,,/media/fonts/#Cera Pro Light)�5   	    � AllowsTransparency$
 True��5   	    � Icon$ 	/calc.png��6#   ��5      C� 5   
    C� ResourceKey$ BackgroundImage��6   .��5      � .+�5   
   �� -   5        
������ $��30��5   *   $	��Top=�66   ._�5      ]� $��False��6   .+�5      �� -   5         ��Name$ 
MenuButton��5         TitleBarButtonStyle#���   5      $��14(�5        TitleBarForeground#���  5      $��0q�5      $
��Left=�5      $��8,-6,5,0q�5!      $��27��6+   ��5"      �� 5#        GearIcon#���  6    $��47��6D   $��47��6O    �� BitmapScalingMode$ HighQuality=�5$       5&      �� 5)      $��Right=�6    �� Orientation$ 
Horizontal=�64   .+�5*      �� -   5        $ MinimizeButton��5*      $	��─��65   $��20(�6A   #���   5+      $��0,-4,6,0q�6K   $��20��5,   :   �� 5-      -   5        $ MaximizeButton��5-      $	��□��65   $��17(�6A   #���   5.      $��0,0,6,0q�6K   $��22��5/   :   �� 50      -   5        $ CloseButton��50      $��×��62   $��26(�6>   #���   51      $��0,-2,2,0q�6K   $��22��52   7   	 J� RenderTransformOrigin$	 
0.72,0.531&�6B   53      �� 56      $��Калькулятор��6   #���  57      $��19(�58      $��Center=�59      $��6,0,10,0q�5:      $
��Left=�5;      5<      5=      � 5@   
   $��0,30,0,0q�6   .+�5B         WpfApp1.BasicCalculator  -   5        
  Name$
 BasicCalculator��5B   $   "   WpfApp1.ScientificCalculator  5E      -   5          Name$ ScientificCalculator��5E   )   5F      � 5K   
   -   5         �Name$ 	MenuPopup��5K      $��Bottom=�6V    � 	StaysOpen$ False��6i    � AllowsTransparency$
 True��6{    � PlacementTarget 6#   ��  �� ElementName$ 
MenuButton��.��5L      ��  . *ContextMenuButtonStyleHoverBackgroundBrush#���  6   $	��150��6c   .+�5M      �� -	   5        $ ChangeThemeButton��5M      $��Change Theme��64   $��5q�6f    " ContextMenuTitleBarButtonStyle#���  6q   �� 5N      -
   5        $��About��5N      $��5q�6=   #���  6H   5O      5P      � 5S   
   -   5        $ 
ThemePopup��5S      $��Right=�6^   $ False��6p   $
 True��6�    6$   �� $ ChangeThemeButton��.��5T      ��  �� Padding$ 10q�6   ._�5U      �� #���  6   .+�5V      �� -   5        $��Light��5V      #���  6F   $��5q�6   �� 5W      -   5        $
��Dark��5W      #���  6D   $��5q�6}   �� 5X      -   5        $��Dark Lavender��5X      #���  6U   $��5q�6�   �� 5Y      -   5        $��Violet Sunrise��5Y      #���  6W   $��5q�6�   �� 5Z      -   5        $��Northern Lights��5Z      #���  6Y   $��5q�6�   �� 5[      -   5        $��University1��5[      #���  6R   $��5q�6�   �� 5\      -   5        $��University2��5\      #���  6R   $��5q�6�   5]      5^      5_      5`      5a      
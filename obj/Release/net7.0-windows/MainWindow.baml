   M S B A M L   `   `   `  ���� C  ?Calc 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1       � Title$  Калькулятор��$	��560��$	��340��$	��340��$	��500�� � WindowStyle$
 None=�$8��2pack://application:,,,/media/fonts/#Cera Pro Light)� � AllowsTransparency$
 True�� � Icon$ 	/calc.png����C�  C� ResourceKey$ BackgroundImage��.��� .+��� -   
������ $��30��$	��Top=�._�]� $��False��.+��� -    ��Name$ 
MenuButton��   TitleBarButtonStyle#���   $��14(�  TitleBarForeground#���  $��0q�$
��Left=�$��8,-6,5,0q�$��27������   GearIcon#���  $��47��$��47�� �� BitmapScalingMode$ HighQuality=��� $��Right=� �� Orientation$ 
Horizontal=�.+��� -   $ MinimizeButton��$	��─��$��20(�#���   $��0,-4,6,0q�$��20���� -   $ MaximizeButton��$	��□��$��17(�#���   $��0,0,6,0q�$��22���� -   $ CloseButton��$��×��$��26(�#���   $��0,-2,2,0q�$��22�� J� RenderTransformOrigin$ 
0.72,0.531&��� $��Калькулятор��#���  $��19(�$��Center=�$��6,0,10,0q�$
��Left=�� $��0,30,0,0q�.+�   WpfApp1.BasicCalculator  -   	  Name$	 BasicCalculator��"   WpfApp1.ScientificCalculator  -   
  Name$
 ScientificCalculator��� -    �Name$ 	MenuPopup��$��Bottom=� � 	StaysOpen$ False�� � AllowsTransparency$
 True�� � PlacementTarget ��  �� ElementName$ 
MenuButton��.����  . *ContextMenuButtonStyleHoverBackgroundBrush#���  $	��150��.+��� -	   $ ChangeThemeButton��$��Change Theme��$��5q� " ContextMenuTitleBarButtonStyle#���  �� -
   $��About��$��5q�#���  � -   $ 
ThemePopup��$��Right=�$ False��$
 True�� �� $ ChangeThemeButton��.����  �� Padding$ 10q�._��� #���  .+��� -   $��Light��#���  $��5q��� -   $
��Dark��#���  $��5q��� -   $��Dark Lavender��#���  $��5q��� -   $��Violet Sunrise��#���  $��5q��� -   $��Northern Lights��#���  $��5q�
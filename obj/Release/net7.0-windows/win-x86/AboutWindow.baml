   M S B A M L   `   `   `  ���� C  ?Calc 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.AboutWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1       � Title$  About��$	��550��$	��650�� � WindowStartupLocation$ CenterScreen=�$8��2pack://application:,,,/media/fonts/#Cera Pro Light)� � 
ResizeMode$ NoResize=� � AllowsTransparency$
 True�� � WindowStyle$
 None=�
������ .��� .+���    AboutWindowBackground#���    �� CornerRadius$ 10��$��Center=�$��Center=� �� Padding$ 40q�$	��480�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�	   ShadowDepth$	 0Q�
   Color$
 Black��._��� $��Center=�$��Center=�.+��� $��/Media/Images/calc.png��$��64��$��64��$��0,0,0,10q��� $��Author: CyberTechWiz��$��26(�$
��Bold"�$��0,0,0,0q�  DisplayBoxForeground#���  �� $��18(�$
��Bold"�$��0,0,0,10q��� -   $%��https://github.com/CyberTechWiz?� �� TextDecorations$
 None{�.�����  �� 
TargetType" ��.���  �� Property � ��  HyperlinkColor �� Value# �  ��  �@��TextDecorations$
 None{� �� Triggers P�  P� Property �@��IsMouseOver P� Value$
 True��. ���  � ��  HyperlinkColor2# �  .�!https://github.com/CyberTechWiz�� $��Instructions:��$��24(�$
��Bold"�$��0,10,0,5q�#���  �� $6��01. Overflow. If you need to count large numbers,��$��22(�#���  �� $6��0multiply one of them by "1,0" or add ",0" to the��$��22(�#���  �� $��end of the number.��$��22(�#���  �� $"��2. Esc- Clear the screen (C)��$��22(�#���  �� -   $��Закрыть��$	��100��$��0,20,0,0q�  EqualFlatButtonStyle#���  $��16(�
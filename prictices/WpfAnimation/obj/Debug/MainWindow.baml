   M S B A M L   `   `   `  ����G  CWpfAnimation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WpfAnimationWpfAnimation  5   	       WpfAnimation.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WpfAnimation     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	         sb W� From	 W� To g� Duration �� 
TargetName �� TargetProperty �� From	 �� To  rotatesb	 g� AutoReverse
 g� RepeatBehavior��%(  &	        &	 p    �� 5
   
   .��5      W� $	 120X�6   $	 300X�6)   $ 0:0:2D�62   $ _button��5      $ Width�5      5      W� 5      $ 40X�6   $	 150X�6(   $ 0:0:2D�61   $ _button��5      $ Height�5      5      �� 5      $ WhiteX�6   $ BlackX�6*   $ 0:0:2D�65   $	 _sb��5      $ Color�5      5      5      �� 5   
   	 . $
 Forever��69   .��5      W� $ 0X�6   $	 360X�6'   $ 0:0:2D�60   $ _rotate��5      $ Angle�5      5      5      5        � Triggers 5!      :� 5"   
    :� RoutedEvent$ Button.Click��6    :� 
SourceName$ _button��62   .[�5#      �� #��[ 6   5$      .��5&      � .+�5'   
   �� .+�5(      �� -   5        $��_button��5(      $��40��6$   $	��120��60   $��Button��6<   $��Center=�6M   $��Center=�6h   6�   �� 5)      -   5        $��_text��5)      
����·�$��123456��6:   
�������6]   �� 5*      $��123456��6   
���������5+      �� 5,       �� Color$ Green��6&   -   5        _sb��5,   C   5.      5/      � 50   
   $	��100��6   $	��100��6    J� RenderTransformOrigin$ 0.5,0.5&�6(   z�52      �� 54      -   5        _rotate�� �� Angle$ 45Q�54   3   6A   .+�56      �� $��12��6   $��12��6     �� CornerRadius$ 6��6,   
��� ���$��Bottom=�6Q   6n   57      58      59      